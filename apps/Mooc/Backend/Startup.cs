namespace MoocApps.Backend
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Mooc.Courses.Application.Create;
    using Mooc.Courses.Domain;
    using Mooc.Courses.Infrastructure.Persistence;
    using Mooc.Courses.Infrastructure.Persistence.EntityFramework;
    using Shared.Domain;
    using Shared.Infrastructure;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IRandomNumberGenerator, CSharpRandomNumberGenerator>();
            services.AddScoped<CourseCreator, CourseCreator>();
            services.AddScoped<ICourseRepository, MySqlCourseRepository>();

            services.AddDbContext<CourseContext>(options => options.UseMySQL(Configuration.GetConnectionString("MoocDatabase")));
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
            app.UseMvc(routes => { routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); });
        }
    }
}