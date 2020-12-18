using AgricultureAI.MachineLearning;
using MachineLearning;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AgricultureAITestCollection
{
    [TestClass]
    public class MachineLearningTests
    {
        [TestMethod]
        public void LookupTrueTest()
        {
            GroundTruth.CreateGroundTruthLookup();

            string[] imageArray = GroundTruth.GetImageKeys();
            bool isHealthy = GroundTruth.Lookup("DSC00038.JPG");
            Assert.IsTrue(isHealthy);
        }

        [TestMethod]
        public void LookupFalseTest()
        {
            GroundTruth.CreateGroundTruthLookup();

            string[] imageArray = GroundTruth.GetImageKeys();
            bool isHealthy = GroundTruth.Lookup("DSC00041.JPG");
            Assert.IsFalse(isHealthy);
        }

        [TestMethod]
        public void GetImageKeysTest()
        {
            GroundTruth.CreateGroundTruthLookup();

            string[] imageArray = GroundTruth.GetImageKeys();
            int numEntries = imageArray.Length;
            int numPictures = 1787;
            Assert.IsTrue(numEntries == numPictures);
        }


        [TestMethod]
        public void getNumTrueAndFalse()
        {

            int numTrue = 0;
            int numFalse = 0;

            GroundTruth.CreateGroundTruthLookup();
            string[] imageArray = GroundTruth.GetImageKeys();

            int numImages = imageArray.Length;

            for (int i = 0; i < imageArray.Length; i++)
            {
                if (GroundTruth.Lookup((imageArray[i]).ToString()) == true)
                {
                    numTrue++;
                }
                else numFalse++;
            }

            Assert.IsTrue((numFalse + numTrue) == numImages);
        }
    }
}
        
