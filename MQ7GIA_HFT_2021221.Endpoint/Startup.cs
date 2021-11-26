using Castle.Core.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQ7GIA_HFT_2021221.Data;
using MQ7GIA_HFT_2021221.Logic;
using MQ7GIA_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221.Endpoint
{
    public class Startup
    {     
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<ICarsLogic,CarsLogic>();
            services.AddTransient<ICarsRepository, CarsRepository>();
            
            services.AddTransient<IContractsLogic, ContractsLogic>();
            services.AddTransient<IContractsRepository, ContractsRepository>();
            
            services.AddTransient<ICustomersLogic, CustomersLogic>();
            services.AddTransient<ICustomersRepository, CustomersRepository>();
            
            services.AddTransient<IDepartmentsLogic, DepartmentsLogic>();
            services.AddTransient<IDepartmentsRepository, DepartmentsRepository>();

            services.AddDbContext<CarDealershipContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) //Checks Environment name
            {
                app.UseDeveloperExceptionPage(); //You may or may not want to show this to users,you can choose not to show it and create a main error page endpoint
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //The order of function calls MATTERS, it's not coincidence
            app.UseHttpsRedirection(); //Handle redirections
            app.UseStaticFiles();

            app.UseRouting(); //Specifies Path

            //app.UseAuthorization(); => It is optional 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); //Maps controllers to path
            });
        }
    }
}
