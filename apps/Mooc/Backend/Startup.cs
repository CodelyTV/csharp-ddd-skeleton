namespace CodelyTv.Apps.Mooc.Backend
{
    using System;
    using System.Linq;
    using CodelyTv.Mooc.Courses.Application.Create;
    using CodelyTv.Mooc.Courses.Domain;
    using CodelyTv.Mooc.Courses.Infrastructure.Persistence;
    using CodelyTv.Mooc.CoursesCounter.Application.Incrementer;
    using CodelyTv.Mooc.CoursesCounter.Domain;
    using CodelyTv.Mooc.CoursesCounter.Infrastructure.Persistence;
    using CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework;
    using Extension;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Shared.Domain;
    using Shared.Domain.Bus.Event;
    using Shared.Infrastructure;
    using Shared.Infrastructure.Bus.Event;

    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddScoped<IRandomNumberGenerator, CSharpRandomNumberGenerator>();
            services.AddScoped<CourseCreator, CourseCreator>();
            services.AddScoped<ICourseRepository, MySqlCourseRepository>();

            services.AddScoped<CoursesCounterIncrementer, CoursesCounterIncrementer>();
            services.AddScoped<IUuidGenerator, CSharpUuidGenerator>();
            services.AddScoped<ICoursesCounterRepository, MySqlCoursesCounterRepository>();

            services.AddScoped<IEventBus, InMemoryEventBus>();
            services.AddDomainEventSuscribersServices(AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.FullName.Contains("CodelyTv.Mooc")));

            services.AddDbContext<MoocContext>(options => options.UseSqlServer(_configuration.GetConnectionString("MoocDatabase")));
        }

        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}"); });
        }
    }
}