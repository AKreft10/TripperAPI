using FluentEmail.MailKitSmtp;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Authorization;
using TripperAPI.Entities;
using TripperAPI.Middleware;
using TripperAPI.Models;
using TripperAPI.Models.ValidatorModels;
using TripperAPI.Services;
using TripperAPI.Services.ApiDataServices;

namespace TripperAPI
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

            var from = Configuration.GetSection("Email")["From"];
            var sender = Configuration.GetSection("Gmail")["Sender"];
            var password = Configuration.GetSection("Gmail")["Password"];
            var port = Convert.ToInt32(Configuration.GetSection("Gmail")["Port"]);
            var server = Configuration.GetSection("Gmail")["Server"];


            services
                .AddFluentEmail(sender, from)
                .AddRazorRenderer()
                .AddMailKitSender(new SmtpClientOptions
                {
                    Server = server,
                    Port = port,
                    UseSsl = true,
                    RequiresAuthentication = true,
                    User = sender,
                    Password = password,
                });


            var authConfiguration = new AuthConfiguration();

            Configuration.GetSection("Authentication").Bind(authConfiguration);

            services.AddSingleton(authConfiguration);
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authConfiguration.JwtIssuer,
                    ValidAudience = authConfiguration.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfiguration.JwtKey)),
                };
            });
            services.AddControllers().AddFluentValidation();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
            services.AddScoped<ExceptionMiddleware>();
            services.AddDbContext<DatabaseContext>();
            services.AddScoped<DbSeeder>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IValidator<RegisterNewUserDto>, RegisterUserDtoValidator>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IPlaceDistanceService, PlaceDistanceService>();
            services.AddScoped<IBingMapsDistanceService, BingMapsDistanceService>();
            services.AddHttpContextAccessor();
            services.AddSwaggerGen();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbSeeder seeder)
        {
            seeder.SeedData();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tripper - API");
                c.RoutePrefix = "api/swagger";
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
