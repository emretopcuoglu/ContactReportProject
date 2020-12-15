using AutoMapper;
using ContactReport.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReportAPI.Core;
using ReportAPI.Entity;
using ReportAPI.Service;

namespace ReportAPI
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
            var connectionUrl = Configuration.GetSection("ConnectionStrings")["AzureConnection"];
            services.AddEntityFrameworkNpgsql().AddDbContext<DatabaseContext>(opt =>
            opt.UseNpgsql(connectionUrl));

            services.AddAutoMapper(typeof(Startup), typeof(ReportProfile));

            services.AddScoped<IReport, ReportService>();
            services.AddScoped<IRabbitService, RabbitService>();

            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<IRabbitService>().ReceiveMessage();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }
    }
}
