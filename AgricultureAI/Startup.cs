using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AgricultureAI.Persistence;
using AgricultureAI.Persistence.HigherLevel;
using AgricultureAI.MachineLearning;
using MachineLearning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AgricultureAI
{
    public class Startup
    {
        /// <summary>
        /// This is the starting point of the server program. Here we assign the app a configuration and initialize Machine Learning engines.
        /// </summary>
        /// <param name="configuration">An IConfiguration object</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // Set Firebase URL:
            RestfulDBConnection.FIREBASE_URL = Configuration["FirebaseURL"];

            // Initialize Machine Learning Components:
            MLModel.CreatePredictionEngine();
            GroundTruth.CreateGroundTruthLookup();

            // Get the Machine Learning Warmed Up:
            MLModel.Predict("https://firebasestorage.googleapis.com/v0/b/agricultureai-15ce0.appspot.com/o/DSC00025.JPG?alt=media");
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Method to add services to the html container (such as razor pages).
        /// </summary>
        /// <param name="services">An IServiceCollection (collection of services)</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().WithRazorPagesRoot("/Website/html");
            services.AddRazorPages();
        }

        /// <summary>
        /// Method to configure the application's exception handling, authorization, and rerouting.
        /// </summary>
        /// <param name="app">The application</param>
        /// <param name="env">The host environment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
