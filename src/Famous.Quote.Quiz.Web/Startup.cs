using Famous.Quote.Quiz.Application.Services;
using Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate;
using Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate;
using Famous.Quote.Quiz.Domain.SeedWork;
using Famous.Quote.Quiz.Infrastructure;
using Famous.Quote.Quiz.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Famous.Quote.Quiz.Web
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
            services.AddControllersWithViews()
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining(typeof(Startup)).ConfigureClientsideValidation(enabled: false)); ;

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IQuizRepository, QuizRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IQuizService, QuizService>();

            services.AddDbContext<FamousQuoteQuizDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString(nameof(FamousQuoteQuizDbContext)));
                options.UseLazyLoadingProxies();
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                    options.AccessDeniedPath = "/login";
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            Migrate(app);
        }

        static void Migrate(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<FamousQuoteQuizDbContext>().Database.Migrate();
            }
        }
    }
}
