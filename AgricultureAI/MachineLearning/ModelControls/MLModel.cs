using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ML;

/*
 * EXAMPLE USE:
 * ModelInput sample = new ModelInput();
 * sample.ImageSource = @".\MachineLearning\PinnacleAlgorithm\TrainingImages\Healthy\DSC00027.JPG";
 * var predictionResult = MLModel.Predict(sample);
 * Debug.Write($"Predicted Label value {predictionResult.Prediction} \nPredicted Label scores: [{String.Join(",", predictionResult.Score)}]\n");
 */

namespace MachineLearning
{
    public class MLModel
    {
        private static Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictionEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(CreatePredictionEngine);

        // For more info on consuming ML.NET models, visit https://aka.ms/mlnet-consume
        // Method for consuming model in your app
        public static ModelOutput Predict(ModelInput input)
        {
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
