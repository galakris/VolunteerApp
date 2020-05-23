using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volunteer.Identity.DAL.Entities;
using Volunteer.Identity.DAL;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Volunteer.Identity
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
            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("VolunteerAuth")).UseSnakeCaseNamingConvention());

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
               .AddEntityFrameworkStores<AuthDbContext>()
               .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                    .AddConfigurationStore(option =>
                           option.ConfigureDbContext = builder => builder.UseNpgsql(Configuration.GetConnectionString("VolunteerAuth"), options =>
                           options.MigrationsAssembly("Volunteer.Identity.DAL")).UseSnakeCaseNamingConvention())
                    .AddOperationalStore(option =>
                           option.ConfigureDbContext = builder => builder.UseNpgsql(Configuration.GetConnectionString("VolunteerAuth"), options =>
                           options.MigrationsAssembly("Volunteer.Identity.DAL")).UseSnakeCaseNamingConvention());

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AuthDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");
            AuthDbInitializer.Initialize(app, context);
            app.UseIdentityServer();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
