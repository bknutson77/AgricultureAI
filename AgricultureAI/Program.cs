using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using AgricultureAI.Persistence;
using MachineLearning;

namespace AgricultureAI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Testing Machine Learning:
            ModelInput sampleData = new ModelInput()
            {
                ImageSource = @".\MachineLearning\PinnacleAlgorithm\TrainingImages\Healthy\DSC00027.JPG",
            };

            // Make a single prediction on the sample data and print results
            var predictionResult = ConsumeModel.Predict(sampleData);

            Debug.Write("Using model to make single prediction -- Comparing actual Label with predicted Label from sample data...\n\n");
            Debug.Write($"ImageSource: {sampleData.ImageSource}");
            Debug.Write($"\n\nPredicted Label value {predictionResult.Prediction} \nPredicted Label scores: [{String.Join(",", predictionResult.Score)}]\n\n");
            Debug.Write("=============== End of process, hit any key to finish ===============");

            // Testing Database:
            RestfulDBConnection restfulDBConnection = new RestfulDBConnection();
            String testValue = restfulDBConnection.Retrieve("TestValue");
            Debug.Write("This is the test value: " + testValue);
            String storeResponse = restfulDBConnection.Store("User1", "username", "turdFerguson77");
            Debug.Write("This is the response from storing: " + storeResponse);

            // Create Website and Launch:
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseWebRoot("Website");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
