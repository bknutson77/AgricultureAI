using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.ML;


namespace MachineLearning
{
    public class MLModel
    {
        // Prediction Engine used for classifying images:
        private static Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictionEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(CreatePredictionEngine);

        /// <summary>
        /// Method to predict the health status of a plant in an image based on Machine Learning.
        /// </summary>
        /// <param name="url">The url to the image</param>
        /// <returns>
        /// A ModelOutput object with a Prediction label (either Healthy or Unhealthy) and 
        /// Score array (probability of each healthy or unhealthy scores).
        /// </returns>
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

        /// <summary>
        /// Method to create the prediction engine (an ML.NET structure used for classifying images).
        /// </summary>
        /// <returns>
        /// The prediction engine (used to in the Predict method to apply machine learning and predict the label of an image).
        /// </returns>
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
