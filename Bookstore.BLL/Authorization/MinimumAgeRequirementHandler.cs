using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bookstore.BLL.Authorization
{
    public class MinimumAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        private readonly ILogger<MinimumAgeRequirementHandler> _logger;

        public MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger)
        {
            _logger = logger;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       MinimumAgeRequirement requirement)
        {
            var userDateOfBirthClaim = context.User.FindFirst(x => x.Type == "DateOfBirth");

            if (userDateOfBirthClaim is null)
                return Task.CompletedTask;

            var dateOfBirth = DateTime.Parse(context.User.FindFirst(x => x.Type == "DateOfBirth").Value);

            var userName = context.User.FindFirst(x => x.Type == ClaimTypes.Name).Value;

            _logger.LogInformation($"User: {userName} with date of birth {dateOfBirth}.");

            if (dateOfBirth.AddYears(requirement.MinimumAge) <= DateTime.Today)
            {
                _logger.LogInformation("Authorization succeded.");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation("Authorization failed.");
            }

            return Task.CompletedTask;
        }
    }
}
