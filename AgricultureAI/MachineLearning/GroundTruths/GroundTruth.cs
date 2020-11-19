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
        
        public static bool Lookup(String url)
        {
            return GroundTruthDictionary[url];
        }
    }
}
