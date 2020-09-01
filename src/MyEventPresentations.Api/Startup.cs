using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using JosephGuadagno.AzureHelpers.Storage;
using JosephGuadagno.AzureHelpers.Storage.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyEventPresentations.Api.Models;
using MyEventPresentations.BusinessLayer;
using MyEventPresentations.Data;
using MyEventPresentations.Data.Queueing.Queues;
using MyEventPresentations.Data.SqlServer;
using MyEventPresentations.Domain.Interfaces;
using MyEventPresentations.Domain.Models;
using Newtonsoft.Json;

namespace MyEventPresentations.Api
{
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
            services.AddSingleton<IAzureConfiguration, AzureConfiguration>(provider =>
            {
                var azureConfiguration = new AzureConfiguration();
                Configuration.Bind("AzureConfiguration", azureConfiguration);
                return azureConfiguration;
            });

            services.AddSingleton<PresentationAddedQueue>(provider =>
            {
                var azure = provider.GetService<IAzureConfiguration>();
                return new PresentationAddedQueue(azure.AzureWebJobsStorage);
            });
            services.AddSingleton<PresentationScheduleAddedQueue>(provider =>
            {
                var azure = provider.GetService<IAzureConfiguration>();
                return new PresentationScheduleAddedQueue(azure.AzureWebJobsStorage);
            });
            
            services.AddControllers()
                .AddNewtonsoftJson(o =>
                {
                    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
            services.AddDbContext<PresentationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PresentationsSqlDb")));
                //options.UseSqlite(Configuration.GetConnectionString("PresentationsDb")));

            services.AddAutoMapper(typeof(PresentationRepositoryStorage));
            services.AddTransient<IPresentationManager, PresentationManager>();
            services.AddTransient<IPresentationRepository, PresentationRepository>();
            services.AddTransient<IPresentationRepositoryStorage, PresentationRepositoryStorage>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "My Event Presentations API",
                    Description = "The API for My Event Presentations",
                    TermsOfService = new Uri("https://www.myeventpresentations.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Joseph Guadagno",
                        Email = "jguadagno@hotmail.com",
                        Url = new Uri("https://www.josephguadagno.net")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://www.myeventpresentations.com/license"),
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Event Presentations");
            });
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}