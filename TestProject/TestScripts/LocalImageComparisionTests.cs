using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject.BasePage;

namespace TestProject.TestScripts
{
    /// <summary>
    /// Locally stored image comparision tests with ' ImageComparer.Compare' method
    /// </summary>
    [CodedUITest]
    public class LocalImageComparisionTests : TestBase
    {
        private readonly string _imagesPath = @"C:\AutomationImages\";

        [TestMethod,
         Description("Open two identical Images and compare with '0' tolerance, comparision should return 'TRUE'")]
        public void IdenticalImage_ZeroTolerance()
        {
            Image expectedImage = Image.FromFile(_imagesPath + "Actual.jpg");
            Image actualImage = Image.FromFile(_imagesPath + "Actual_Copy.jpg");
            ColorDifference diff = new ColorDifference(0);
            bool compare = ImageComparer.Compare(expectedImage, actualImage, diff, out var outPutFile);
            outPutFile.Save(_imagesPath + "Output\\" + System.Reflection.MethodBase.GetCurrentMethod().Name + ".jpg");
            Assert.IsTrue(compare, "Comparision should return 'TRUE'");

        }

        [TestMethod,
         Description(
             "Open two similare images which are around 10% different, compare with '0' tolerance, comparison will return 'FALSE'")]
        public void SimilarImage_ZeroTolerance()
        {
            Image expectedImage = Image.FromFile(_imagesPath + "Actual.jpg");
            Image actualImage = Image.FromFile(_imagesPath + "Actual_Edited.jpg");
            ColorDifference diff = new ColorDifference(0);
            bool compare = ImageComparer.Compare(expectedImage, actualImage, diff);
            Assert.IsFalse(compare, "Compared Edited Images With Zero Tolerance");
        }

        [TestMethod,
         Description(
             "Open two similare images which are around 10% different, compare with 'Least' tolerance, comparison will return 'TRUE'")]
        public void SimilarImage_LowTolerance()
        {
            Image expectedImage = Image.FromFile(_imagesPath + "Actual.jpg");
            Image actualImage = Image.FromFile(_imagesPath + "Actual_Edited.jpg");
            ColorDifference diff = new ColorDifference(255);
            bool compare = ImageComparer.Compare(expectedImage, actualImage, diff);
            Assert.IsTrue(compare, "Compared Edited Images With Low Tolerance");
        }

        [TestMethod,
         Description(
             "Open two similare images which are around 10% different, compare with 'Medium+' tolerance, comparison will return 'TRUE'")]
        public void SimilarImage_MediumTolerance_Positive()
        {
            Image expectedImage = Image.FromFile(_imagesPath + "Actual.jpg");
            Image actualImage = Image.FromFile(_imagesPath + "Actual_Edited.jpg");
            ColorDifference diff = new ColorDifference(225);
            bool compare = ImageComparer.Compare(expectedImage, actualImage, diff);
            Assert.IsTrue(compare, "Compared Edited Images With Medium Tolerance");
        }

        [TestMethod,
         Description(
             "Open two similare images which are around 10% different, compare with 'Medium-' tolerance, comparison will return 'FALSE'")]
        public void SimilarImage_MediumTolerance_Negative()
        {
            Image expectedImage = Image.FromFile(_imagesPath + "Actual.jpg");
            Image actualImage = Image.FromFile(_imagesPath + "Actual_Edited.jpg");
            ColorDifference diff = new ColorDifference(0);
            bool compare = ImageComparer.Compare(expectedImage, actualImage, diff, out var outPutFile);
            outPutFile.Save(_imagesPath + "Output\\" + System.Reflection.MethodBase.GetCurrentMethod().Name + ".jpg");
            Assert.IsFalse(compare, "Compared Edited Images With Medium Tolerance");
        }

        [TestMethod,
         Description("An Image with its mirror Image with '0' tolerance, comparision should return 'FALSE'")]
        public void MirrorImageComparision_LowTolerance()
        {
            Image expectedImage = Image.FromFile(_imagesPath + "Actual.jpg");
            Image actualImage = Image.FromFile(_imagesPath + "Actual_Mirror.jpg");
            ColorDifference diff = new ColorDifference(0);
            bool compare = ImageComparer.Compare(expectedImage, actualImage, diff, out var outPutFile);
            outPutFile.Save(_imagesPath + "Output\\" + System.Reflection.MethodBase.GetCurrentMethod().Name + ".jpg");
            Assert.IsFalse(compare, "Compared Edited Images With Medium Tolerance");
        }
    }
}