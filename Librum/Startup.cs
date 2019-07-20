using Librum.Interfaces;
using Librum.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Westwind.AspNetCore.Markdown;
using Markdig;
using Markdig.Extensions.AutoIdentifiers;
using System;

namespace Librum
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseNpgsql(Configuration.GetValue<string>("ConnectionString"));
            });

            services.Configure<LibrumSettings>(Configuration);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddTransient<Articles>();
            services.AddTransient<Users>();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(365);
                options.Cookie.IsEssential = true;
            });

            services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/forbidden";
                options.LogoutPath = "/account/logout";
            });

            services.AddMarkdown(config =>
            {
                config.ConfigureMarkdigPipeline = builder =>
                {
                    builder.UseEmphasisExtras(Markdig.Extensions.EmphasisExtras.EmphasisExtraOptions.Default)
                        .UsePipeTables()
                        .UseGridTables()
                        .UseAutoIdentifiers(AutoIdentifierOptions.GitHub) // Headers get id="name" 
                        .UseAutoLinks()
                        .UseAbbreviations()
                        .UseYamlFrontMatter()
                        .UseEmojiAndSmiley(true)
                        .UseListExtras()
                        .UseFigures()
                        .UseTaskLists()
                        .UseCustomContainers()
                        .UseGenericAttributes();
                };
            });

            services.AddMvc();
            services.AddHttpContextAccessor();

            services.AddSingleton<IConfiguration>(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/error?errorCode={0}");
                // app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
