using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.ML;

/*
 * EXAMPLE USE:
 * var predictionResult = MLModel.Predict(@".\MachineLearning\PinnacleAlgorithm\TrainingImages\Healthy\DSC00027.JPG");
 * Debug.Write($"Predicted Label value {predictionResult.Prediction} \nPredicted Label scores: [{String.Join(",", predictionResult.Score)}]\n");
 */

namespace MachineLearning
{
    public class MLModel
    {
        private static Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictionEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(CreatePredictionEngine);

        // For more info on consuming ML.NET models, visit https://aka.ms/mlnet-consume
        // Method for consuming model in your app
        public static ModelOutput Predict(String url)
        {
            // Create new Model Input object:
            ModelInput input = new ModelInput();

            // Download and save a copy of the image (this is necessary for some reason):
            string destination = @"./MachineLearning/TempStorage/tempImage.png";
            using (System.Net.WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(url), destination);
            }

            // Set the input to the newly cached image:
            input.ImageSource = destination;

            // Predict and return prediction:
            ModelOutput result = PredictionEngine.Value.Predict(input);
            return result;
        }

        public static PredictionEngine<ModelInput, ModelOutput> CreatePredictionEngine()
        {
            // Create new MLContext
            MLContext mlContext = new MLContext();

            // Load model & create prediction engine
            string modelPath = @".\MachineLearning\PinnacleAlgorithm\MLModel.zip";
            ITransformer mlModel = mlContext.Model.Load(modelPath, out var modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            return predEngine;
        }
    }
}
