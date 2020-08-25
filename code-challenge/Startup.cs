using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using challenge.Data;
using challenge.Repositories;
using challenge.Services;

namespace code_challenge
{
    public class Startup
    {
        // Constructors
        //
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Properties
        //
        public IConfiguration Configuration { get; }

        // Class methods
        //

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Employee
            //
            services.AddDbContext<EmployeeContext>(options =>
            {
                options.UseInMemoryDatabase("EmployeeDB");
            });
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<EmployeeDataSeeder>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddMvc();

            // ReportingStructure
            //
            services.AddDbContext<ReportingStructureContext>();
            services.AddScoped<IReportingStructureRepository, ReportingStructureRepository>();
            services.AddScoped<IReportingStructureService, ReportingStructureService>();
            services.AddMvc();

            // Compensation
            //
            services.AddDbContext<CompensationContext>(options =>
            {
                options.UseInMemoryDatabase("CompensationDB");
            });
            services.AddScoped<ICompensationRepository, CompensationRepository>();
            services.AddScoped<ICompensationService, CompensationService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, EmployeeDataSeeder seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                seeder.Seed().Wait();
            }

            app.UseMvc();
        }
    }
}
