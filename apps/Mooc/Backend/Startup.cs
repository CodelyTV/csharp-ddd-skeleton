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
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Shared.Domain;
    using Shared.Domain.Bus.Event;
    using Shared.Infrastructure;
    using Shared.Infrastructure.Bus.Event;

    public class Startup
    {
        private readonly IConfiguration _configuration;
        private IHostingEnvironment _currentEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment currentEnvironment)
        {
            _configuration = configuration;
            _currentEnvironment = currentEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IRandomNumberGenerator, CSharpRandomNumberGenerator>();
            services.AddScoped<CourseCreator, CourseCreator>();
            services.AddScoped<ICourseRepository, MySqlCourseRepository>();

            services.AddScoped<CoursesCounterIncrementer, CoursesCounterIncrementer>();
            services.AddScoped<IUuidGenerator, CSharpUuidGenerator>();
            services.AddScoped<ICoursesCounterRepository, MySqlCoursesCounterRepository>();

            services.AddScoped<IEventBus, InMemoryEventBus>();
            services.AddDomainEventSuscribersServices(AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.FullName.Contains("CodelyTv.Mooc")));

            services.AddDbContext<MoocContext>(GetDatabaseOptions());
        }

        private Action<DbContextOptionsBuilder> GetDatabaseOptions()
        {
            if (this._currentEnvironment.IsEnvironment("Testing"))
            {
                return options => options.UseInMemoryDatabase("TestingDB");
            }
            
            return options => options.UseMySQL(_configuration.GetConnectionString("MoocDatabase"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc(routes => { routes.MapRoute("default", "{controller=Home}/{action=Invoke}/{id?}"); });
        }
    }
}