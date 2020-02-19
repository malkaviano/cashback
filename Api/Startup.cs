using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using EFRepository;
using Domain.Interfaces;
using Api.Identity;
using Domain.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Api.Gateways;
using Domain.Cashback;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Http;

namespace Api
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
            services.AddDbContext<DataContext>(c => c.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AuthDbContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("AuthConnection"))
            );

            services.AddIdentity<AuthUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IResellerRepository, ResellerRepository>();

            services.AddScoped<ISalesRepository, SalesRepository>();

            services.AddScoped<IResellerService, ResellerService>();

            services.AddScoped<ISalesService, SalesService>();

            services.AddScoped<ICashbackStrategy, CashbackDefaultStrategy>(ctx =>
            {
                // TODO: Use appsettings
                return new CashbackDefaultStrategy(
                    DateTime.Now.AddMonths(-6),
                    DateTime.Now.AddMonths(6)
                );
            });

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddJsonOptions(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddHttpClient<CashbackClient>("CashbackClient", x => { x.BaseAddress = new Uri("https://mdaqk8ek5j.execute-api.us-east-1.amazonaws.com"); });

            services.AddSingleton<CashbackClientFactory>();

            services.AddScoped<AuthService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["jwt:key"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    }
            );

            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            // Redirect fix
            services.ConfigureApplicationCookie(options =>
            {
                // Not creating a new object since ASP.NET Identity has created
                // one already and hooked to the OnValidatePrincipal event.
                // See https://github.com/aspnet/AspNetCore/blob/5a64688d8e192cacffda9440e8725c1ed41a30cf/src/Identity/src/Identity/IdentityServiceCollectionExtensions.cs#L56
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
