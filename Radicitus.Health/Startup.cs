using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Radicitus.Health.Data.Contexts;
using Radicitus.Health.Data.Repositories.Implementations;
using Radicitus.Health.Data.Repositories.Interfaces;
using Radicitus.Health.Redis;
using Radicitus.Health.Redis.RadicitusRedis;
using VueCliMiddleware;

namespace Radicitus.Health
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                Configuration = configuration;
            }
            if (env.IsProduction())
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath("/opt/appsettings/radicitus-health")
                    .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
                Configuration = builder.Build();
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("radicitus-health");
            var redisConnectionString = Configuration.GetConnectionString("radicitus-health-redis");
            var dbContext = new RadicitusHealthContext();
            services.AddDbContext<RadicitusHealthContext>(optionsAction =>
            {
                optionsAction.UseNpgsql(connectionString, builder =>
                {
                    builder.MigrationsAssembly("Radicitus.Health");
                });
            });

            var redisConnection = new RedisConnection(redisConnectionString);
            redisConnection.Connect();
            var repo = new RadicitusRedisDataRepository(redisConnection);
            repo.AddTags(new List<string> { "Recipes", "Workouts", "Playlists" }).GetAwaiter().GetResult();
            services.AddSingleton<IRadicitusRedisDataRepository, RadicitusRedisDataRepository>((item) =>
            {
                return repo;
            });
            services.AddScoped<IHealthInitiativeRepository, HealthInitiativeRepository>();
            services.AddScoped<IParticipantLogRepository, ParticipantLogRepository>();
            services.AddScoped<IHealthParticipantRepository, HealthParticipantRepository>();

            services.AddControllers();
            // In production, the Vue files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "health-app/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

                if (env.IsDevelopment())
                {
                    endpoints.MapToVueCliProxy(
                        "{*path}",
                        new SpaOptions { SourcePath = "health-app" },
                        npmScript: "serve",
                        regex: "Compiled successfully");
                }
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "health-app";
            });
        }
    }
}
