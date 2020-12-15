using AutoMapper;
using ContactAPI.Core;
using ContactAPI.Entity;
using ContactAPI.Service;
using ContactReport.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactAPI
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

            services.AddAutoMapper(typeof(Startup), typeof(ContactProfile));

            services.AddScoped<IContact, ContactService>();

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
