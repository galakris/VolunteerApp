using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Volunteer.DAL;
using Volunteer.Services.Users.Interfaces;
using Volunteer.Services.Users.Services;
using Volunteer.SharedObjects;
using VolunteerApi.ConfigurationModels;

namespace VolunteerApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DalContext>(options =>
            options
            .UseNpgsql(Configuration.GetConnectionString("VolunteerApi"))
                .UseSnakeCaseNamingConvention());

            services.AddControllers();
            services.AddCors();
            services.AddAuthentication()
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userService.GetUserById(userId);
                        if (user == null)
                        {
                            context.Fail("Unauthorized");
                        }
                        var claimsIdentity = new ApiIdentity();
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                        claimsIdentity.UserAcountId = user.UserAccountId;
                        context.Principal.AddIdentity(claimsIdentity);
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretHash.Hash)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddHttpContextAccessor();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped(typeof(ApiIdentity), sp => (sp.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor).HttpContext.User.Identities.FirstOrDefault(x => x is ApiIdentity) as ApiIdentity);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(x => x
             .AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
