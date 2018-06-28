using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject.Utilities;

namespace TestProject.TestScripts
{
    /// <summary>
    /// Class containing custom Image comparision tests
    /// </summary>
    [CodedUITest]
    public class CustomImageComparisionTests
    {
        #region Fields

        string imagesPath = FileUtilities.CheckAndCreateDirectory(testImagesPath + System.Reflection.MethodBase.GetCurrentMethod().Name + @"\");

        #endregion

        [TestMethod, Description("Compare two identical Images")]
        public void CustomCompareIdenticalImages()
        {
            string pathImage1 = imagesPath + "Image1.jpg";
            string pathImage2 = imagesPath + "Image1_Copy.jpg";
            float diffZero = ImageUtilities.GetPercentageDifference(pathImage1, pathImage2, 3);
            Assert.AreEqual(0, diffZero, "Images should be same");
        }

        [TestMethod, Description("Compare two Similar Images")]
        public void CustomCompareSimilarImages()
        {
            string pathImage1 = imagesPath + "Image1.jpg";
            string pathImage3 = imagesPath + "Image1_Resolution.jpg";
            float diff2Percent = ImageUtilities.GetPercentageDifference(pathImage1, pathImage3, 3);
            Assert.IsTrue(diff2Percent < 0.02, "Images should be almost similar");
        }

        [TestMethod, Description("Compare two Different Images")]
        public void CustomCompareDifferentImages()
        {
            string pathImage1 = imagesPath + "Image1.jpg";
            string pathImage4 = imagesPath + "Image1_Edited.jpg";
            var diff98Percent = ImageUtilities.GetPercentageDifference(pathImage1, pathImage4, 3);
            Assert.IsTrue(diff98Percent > 0.9, "Images should be different");
        }
    }
}
