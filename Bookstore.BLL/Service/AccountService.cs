using Bookstore.BLL.Authorization;
using Bookstore.BLL.Exceptions;
using Bookstore.BLL.Interface;
using Bookstore.DAL.Entities;
using Bookstore.DAL.Interface;
using Bookstore.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.BLL.Service
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<IAccountRepository> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public AccountService(ILogger<IAccountRepository> logger,
                              IAccountRepository accountRepository,
                              IPasswordHasher<User> passwordHasher,
                              AuthenticationSettings authenticationSettings,
                              IAuthorizationService authorizationService,
                              IUserContextService userContextService)
        {
            _logger = logger;
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public async Task RegisterUser(RegisterUserDTO dto)
        {
            var newUser = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                RoleId = dto.RoleId
            };

            if (dto.Password != dto.ConfirmPasword)
                throw new BadRequestException("Passwords do not match");

            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = hashedPassword;

            await _accountRepository.RegisterAsync(newUser);
        }

        public async Task<string> GenerateJwt(LoginDTO dto)
        {
            var email = dto.Email;

            var user = await _accountRepository.GetUserByEmail(email);

            if (user is null)
            {
                throw new BadRequestException("Invalid email or password.");
            }

            var result = _passwordHasher.VerifyHashedPassword(user,
                                                              user.PasswordHash,
                                                              dto.Password);

            if (result == PasswordVerificationResult.Failed)
                throw new BadRequestException("Invalid email or password.");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}")
            };

            if ((user.DateOfBirth.HasValue))
                claims.Add(new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                                             _authenticationSettings.JwtIssuer,
                                             claims: claims,
                                             expires: expires,
                                             signingCredentials: signingCredentials);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        public async Task ChangePassword(int id, ChangePasswordDTO dto)
        {

            var user = _userContextService.User;

            var userId = id;

            var existingUser = await _accountRepository.GetUserById(userId);

            if (existingUser is null)
                throw new NotFoundException("User not found.");

            var authorizationResult = await _authorizationService.AuthorizeAsync(user,
                                                                                 existingUser,
                                                                                 new ResourceOperationRequirement(ResourceOperation.Update));

            if (!authorizationResult.Succeeded)
                throw new ForbiddenException("Access denied.");

            var result = _passwordHasher.VerifyHashedPassword(user: existingUser,
                                                              existingUser.PasswordHash,
                                                              dto.OldPassword);

            if (result == PasswordVerificationResult.Failed)
                throw new BadRequestException("Invalid password.");

            if (dto.NewPassword != dto.ConfirmPassword)
                throw new BadRequestException("Passwords do not match.");

            var hashedPassword = _passwordHasher.HashPassword(existingUser, dto.NewPassword);

            existingUser.PasswordHash = hashedPassword;

            await _accountRepository.SaveAsync();
        }
    }
}

