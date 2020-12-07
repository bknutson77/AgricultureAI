using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AgricultureAI.MachineLearning
{
    public class GroundTruth
    {
        // Dictionary to hold the ground truths:
        public static Dictionary<string, Boolean> GroundTruthDictionary = new Dictionary<string, Boolean>();

        /// <summary>
        /// Method to create the ground truth lookup dictionary (which is created by parsing the CSV file provided by 
        /// the research group that took these pictures of plants.
        /// </summary>
        public static void CreateGroundTruthLookup()
        {
            //they key will be the image name, the boolean plant healthiness true = healthy false = unhealthy
            
            using (var reader = new StreamReader(@".\MachineLearning\GroundTruths\annotations_handheld.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    //check if the current image being parsed is already in the dictionary
                    if (GroundTruthDictionary.ContainsKey(values[0]) == false)
                    {
                        //check if the 4 coordinate variables are all zero, if they are put that image into the dictionary as healthy
                        if (values[1] == "0" && values[2] == "0" && values[3] == "0" && values[4] == "0")
                        {
                            GroundTruthDictionary.Add(values[0], true);
                        }
                        else //if they are not all zero, then the image is put into the dictionary as unhealthy
                        {
                            GroundTruthDictionary.Add(values[0], false);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Method to look up the ground truth healthy status of a plant in an image.
        /// </summary>
        /// <param name="imageKey">The key, or associated name, of the image</param>
        /// <returns>
        /// A boolean - true if the plant is healthy, false otherwise.
        /// </returns>
        public static bool Lookup(String imageKey)
        {
            return GroundTruthDictionary[imageKey];
        }

        /// <summary>
        /// Method to obtain the list of image keys (or associated names of each plant image).
        /// </summary>
        /// <returns>
        /// An array of image keys.
        /// </returns>
        public static string[] GetImageKeys()
        {
            string[] returnArray = GroundTruthDictionary.Keys.ToArray();
            return returnArray;
        }
    }
}
