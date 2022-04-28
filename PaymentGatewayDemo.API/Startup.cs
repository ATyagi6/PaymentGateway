using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PaymentGatewayDemo.API.Interface;
using PaymentGatewayDemo.API.Middlewares;
using PaymentGatewayDemo.API.Mapping;
using PaymentGatewayDemo.Application.Commands;
using PaymentGatewayDemo.Core.Repositories.Interfaces;
using PaymentGatewayDemo.Core.ThirdParty.Interfaces;
using PaymentGatewayDemo.Infrastructure.Data.InMemory;
using PaymentGatewayDemo.Infrastructure.Repositories;
using PaymentGatewayDemo.ThirdParty.Extensions;
using PaymentGatewayDemo.ThirdParty.External;
using PaymentGatewayDemo.ThirdParty.Interfaces;
using PaymentGatewayDemo.ThirdParty.Shared;
using System;
using System.Reflection;
using System.Text;
using PaymentGatewayDemo.Core.Models;
using Microsoft.Extensions.Hosting;

namespace PaymentGatewayDemo.API
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

            services.AddControllers().AddFluentValidation(s =>
            {
                s.RegisterValidatorsFromAssemblyContaining<Startup>();
                s.DisableDataAnnotationsValidation = true;
            });
            services.AddHealthChecks();
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PaymentGateway Demo API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                            {
                                 new OpenApiSecurityScheme
                                 {
                                     Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme,Id = "Bearer" }
                                 },  Array.Empty<string>()
                            }
                     });
            });



            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });

            services.AddSingleton<IExceptionHandler, ExceptionHandler>();
            services.AddDbContext<PaymentGatewayDemoContext>(options => options.UseInMemoryDatabase(databaseName: "Payments"));
            services.AddTransient<IMapper, Mapper>();
            services.AddMediatR(typeof(CreatePaymentCommand).GetTypeInfo().Assembly);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<MerchantClaim>();
            services.AddTransient<IPaymentGatewayRepository, PaymentGatewayRepository>();
            services.AddTransient<IBankCKO, BankSimulator>();
            services.AddTransient<IBankClient, BankClient>();
            services.AddTransient<IAuthenticationRepository, PaymentGatewayAuthenticationRepository>();
            services.AddReilientBankSimulatorClient(new Uri(Configuration["bankBaseUrl"]), numberOfRetries: Int32.Parse(Configuration["bankSimulatorRetries"]));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthChecks("/health");
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentGatewayDemo.API v1"));

            app.UseMiddleware<PaymentGatewayDemoExceptionMiddleware>();
            app.UseRouting();

            app.UseAuthentication();
            app.UseMiddleware<ClaimsMiddleware>();
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
