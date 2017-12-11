using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WanderDragon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using System.Net;
using Microsoft.AspNetCore.SignalR;
using WanderDragon.Hubs;

namespace WanderDragon
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();

            if (env.IsDevelopment())
            {
            }
            // builder.AddUserSecrets<Startup>();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = "130432400944847";
                facebookOptions.AppSecret = "7290480d9d7a168b35a15f1531cda893";
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<WanderDragonContext>(options =>
           options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<WanderDragonContext>(Options =>
                Options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();
            services.AddSignalR();

            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RegisteredUser", policy => policy.RequireClaim("profileId"));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireClaim("adminId", "1"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Perform automatic migration
                using (var context1 = provider.GetRequiredService<ApplicationDbContext>())
                {
                    context1.Database.Migrate();
                    DbInitializer1.Initialize1(context1);
                }
                using (var context = provider.GetRequiredService<WanderDragonContext>())
                {
                    context.Database.Migrate();
                    DbInitializer.Initialize(context);
                }

            }

            // Setup rewrite rules to allow page refreshes to work with Angular
            app.UseRewriter(new RewriteOptions()
                .AddIISUrlRewrite(env.ContentRootFileProvider, "web.config"));

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();

            

            app.UseSignalR(routes =>
            {
                routes.MapHub<Chat>("chat");
                routes.MapHub<MultiPlayer>("multiplayer");
            });
        }
    }
}
