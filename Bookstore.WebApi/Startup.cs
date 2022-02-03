using Bookstore.BLL;
using Bookstore.BLL.Interface;
using Bookstore.BLL.Service;
using Bookstore.DAL;
using Bookstore.DAL.Entities;
using Bookstore.DAL.Interface;
using Bookstore.DAL.Repository;
using Bookstore.WebApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Bookstore.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var authenticationSettings = new AuthenticationSettings();

            Configuration.GetSection("Authentication").Bind(authenticationSettings);

            services.AddSingleton(authenticationSettings);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
                };
            });

            services.AddControllers();

            services.AddDbContext<BookstoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);
            });
            services.AddScoped<IBookService, BookService>();

            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<IAuthorService, AuthorService>();

            services.AddScoped<IAuthorRepository, AuthorRepository>();

            services.AddScoped<ITagService, TagService>();

            services.AddScoped<ITagRepository, TagRepository>();

            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddAutoMapper(this.GetType().Assembly);

            services.AddScoped<ErrorHandlingMiddleware>();

            services.AddScoped<RequestTimeMiddleware>();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseMiddleware<RequestTimeMiddleware>();

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bookstore");
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
