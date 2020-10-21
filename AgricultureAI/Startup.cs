using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AgricultureAI.Persistence;
using AgricultureAI.Persistence.HigherLevel;
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // Set Firebase URL:
            RestfulDBConnection.FIREBASE_URL = Configuration["FirebaseURL"];

            // Run Startup Tests:
            //RunStartupTests();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().WithRazorPagesRoot("/Website/html");
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

        public void RunStartupTests()
        {
            // Testing Machine Learning:
            var predictionResult = MLModel.Predict(@"https://firebasestorage.googleapis.com/v0/b/agricultureai-15ce0.appspot.com/o/IMG_6700.Jpeg?alt=media");
            Debug.Write($"Predicted Label value {predictionResult.Prediction} \nPredicted Label scores: [{String.Join(",", predictionResult.Score)}]\n");

            // Testing Database:
            if (UserManagement.TestIfUsernameAvailable("bknutson77") == "Available")
            {
                UserManagement.CreateUser("Ben", "benjk117@gmail.com", "Software Engineer", "no", "bknutson77", "haha1234");
            }
        }
    }
}
