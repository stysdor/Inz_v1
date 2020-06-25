using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using Microsoft.ML.Transforms.Text;
using ML.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ML
{
    class Program
    {
        static readonly string _trainDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "LandOffers1.csv");
        static readonly string _testDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "LandOffers1-test.csv");
        static readonly string _modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "Model.zip");

        static void Main(string[] args)
        {
            MLContext mLContext = new MLContext(seed: 0);
            var model = Train(mLContext, _trainDataPath);
            Evaluate(mLContext, model);
            TestSinglePrediction(mLContext, model);
            Console.Read();
        }

        public static ITransformer Train(MLContext mLContext, string dataPath)
        {
            //Load data
            IDataView dataView = mLContext.Data.LoadFromTextFile<Land>(dataPath, hasHeader: true, separatorChar: ';');

            #region Tranform missing values
            // Define replacement estimator
            var replacementEstimator = mLContext.Transforms.ReplaceMissingValues("Price", replacementMode: MissingValueReplacingEstimator.ReplacementMode.Maximum);

            // Fit data to estimator
            // Fitting generates a transformer that applies the operations of defined by estimator
            ITransformer replacementTransformer = replacementEstimator.Fit(dataView);

            // Transform data
            IDataView transformedData = replacementTransformer.Transform(dataView);

            #endregion

            Action<InputRow, OutputRow> mapping = (input, output) =>
            {
                output.E_longitudeConverted = (float)Convert.ToDouble(input.E_longitude);
                output.N_latitudeConverted = (float)Convert.ToDouble(input.N_latitude);
            };

            var pipeline = mLContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "Price")
                            .Append(mLContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "RoadEncoded", inputColumnName: "Road"))
                            .Append(mLContext.Transforms.CustomMapping(mapping, null))
                            .Append(mLContext.Transforms.Concatenate("Features", "Area", "RoadEncoded", "N_latitudeConverted", "E_longitudeConverted"))
                            .Append(mLContext.Regression.Trainers.FastTree());

            var model = pipeline.Fit(transformedData);

            return model;

        }

        private static void Evaluate(MLContext mLContext, ITransformer model)
        {
            IDataView dataView = mLContext.Data.LoadFromTextFile<Land>(_testDataPath, hasHeader: true, separatorChar: ';');

            var predictions = model.Transform(dataView);

            var metrics = mLContext.Regression.Evaluate(predictions, "Label", "Score");

            Console.WriteLine();
            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*       Model quality metrics evaluation         ");
            Console.WriteLine($"*------------------------------------------------");

            Console.WriteLine($"*       RSquared Score:      {metrics.RSquared:0.##}");
            Console.WriteLine($"*       Root Mean Squared Error:      {metrics.RootMeanSquaredError:#.##}");

        }

        private static void TestSinglePrediction(MLContext mLContext, ITransformer model)
        {
            var predictionFunction = mLContext.Model.CreatePredictionEngine<Land, LandPricePrediction>(model);
            var landOfferSample = new Land()
            {
                Area = 3750,
                Road = "utwardzana",
                N_latitude = "50,0317",
                E_longitude = "24,9093",
                Price = 0 //to predict. Actual/Observed = 15.5
            };

            var prediction = predictionFunction.Predict(landOfferSample);

            Console.WriteLine($"**********************************************************************");
            Console.WriteLine($"Predicted fare: {prediction.Price}, actual fare: 262500");
            Console.WriteLine($"**********************************************************************");
        }
    }
}
