namespace CodelyTv.Apps.Mooc.Backend
{
    using Extension.DependencyInjection;
    using Helper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Shared;
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
            services.AddApplication();
            services.AddInfrastructure(_configuration);
            services.AddDomainEventSubscriberInformationService(AssemblyHelper.Instance());
        }

        public static void Configure(IApplicationBuilder app, IHostingEnvironment env, IEventBusConfiguration bus)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            bus.Configure();
        }
    }
}