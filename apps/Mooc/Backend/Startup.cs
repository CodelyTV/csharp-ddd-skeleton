namespace CodelyTv.Apps.Mooc.Backend
{
    using CodelyTv.Mooc.Courses.Application.Create;
    using CodelyTv.Mooc.Courses.Domain;
    using CodelyTv.Mooc.Courses.Infrastructure.Persistence;
    using CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework;
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

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IRandomNumberGenerator, CSharpRandomNumberGenerator>();
            services.AddScoped<CourseCreator, CourseCreator>();
            services.AddScoped<ICourseRepository, MySqlCourseRepository>();

            services.AddScoped<IEventBus, InMemoryEventBus>();

            services.AddDbContext<MoocContext>(options => options.UseMySQL(_configuration.GetConnectionString("MoocDatabase")));
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