using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject.BasePage;
using TestProject.Pages;
using TestProject.Utilities;

namespace TestProject.TestScripts
{
    /// <summary>
    /// Run Tests Individually
    /// </summary>
    [CodedUITest]
    public class TransformAndCompareImageTests : TestBase
    {
        WndImagePro wndImagePro;

        [TestInitialize]
        public new void TestSetup()
        {
            wndImagePro = new WndImagePro();
            wndImagePro.CloseAllToolsAndViews(true);
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            wndImagePro.CloseAllToolsAndViews();
        }

        [TestMethod, Description("Edit two images in same process and compare, Images should be identical")]
        public void TransformTwoImagesAndCompare()
        {
            //Saturation button should be in unselected state

            #region TestData
            Image image1, image2;
            bool compareWithZeroTolerance, compareWith100Tolerance;
            ColorDifference diff = new ColorDifference(0);
            string imagesPath = @"C:\AutomationImages\" + System.Reflection.MethodBase.GetCurrentMethod().Name + @"\";
            string originalImage = imagesPath + "Actual.jpg";
            string runTimeImage1 = imagesPath + "First.jpg";
            string runTimeImage2 = imagesPath + "Second.jpg";

            #endregion

            #region OpenImageAndEdit1

            FileUtilities.OpenFile(originalImage);
            wndImagePro.ClickTabAdjust();
            wndImagePro.ClickBtnRotate();

            wndImagePro.ClickTabMeasure();
            wndImagePro.ClickBtnCreate();
            wndImagePro.ClickTabCapture();
            wndImagePro.ClickBtnSaturation();

            //wndImagePro.TabCountOrSize().ClickPoint();
            //wndImagePro.BtnCount().ClickPoint(-40);

            wndImagePro.ImageViewer.ClickPoint();
            wndImagePro.ImageViewer.CaptureImage(runTimeImage1);

            #endregion

            #region setup

            wndImagePro.CloseAllToolsAndViews(true);

            #endregion

            #region OpenImageAndEdit2

            System.Threading.Thread.Sleep(2000);
            FileUtilities.OpenFile(originalImage);
            FileUtilities.OpenFile(originalImage);

            wndImagePro.ClickTabAdjust();
            wndImagePro.ClickBtnRotate();

            wndImagePro.ClickTabMeasure();
            wndImagePro.ClickBtnCreate();
            System.Threading.Thread.Sleep(1000);

            //Observation: Saturation is selected by default
            //wndImagePro.TabCapture.ClickPoint();
            //wndImagePro.BtnSaturation.Click();

            wndImagePro.ImageViewer.ClickPoint();
            wndImagePro.ImageViewer.CaptureImage(runTimeImage2);

            #endregion

            
            image1 = Image.FromFile(runTimeImage1);
            image2 = Image.FromFile(runTimeImage2);

            compareWithZeroTolerance = ImageComparer.Compare(image1, image2, diff, out Image outPutFile);
            outPutFile.Save(imagesPath + "Output1.jpg");
            Assert.IsTrue(compareWithZeroTolerance, "Images should be same compareWithZeroTolerance");

            diff = new ColorDifference(100);
            compareWith100Tolerance = ImageComparer.Compare(image1, image2, diff, out outPutFile);
            outPutFile.Save(imagesPath + "Output2.jpg");
            Assert.IsTrue(compareWith100Tolerance, "Images should be same compareWith100Tolerance");
        }

        [TestMethod, Description("Edit first image completely, edit second image step wise and compare at each step")]
        public void TransformStepByStepAndCompare()
        {
            /*Open original image, transform image and save
            open original image again, apply partial transformation again, save
            Compare two images, the compare should result false*/

            /*Complete the transformation on second image
            Compare two images with different threshold levels
            Test case should pass*/

            #region Fields
            Image image1, image2, image3;
            ColorDifference diff = new ColorDifference(0);
            string imagesPath = @"C:\AutomationImages\" + System.Reflection.MethodBase.GetCurrentMethod().Name + @"\";
            bool compare;
            string originalImage = imagesPath + "Actual.jpg";
            string runTimeImage1 = imagesPath + "First.jpg";
            string runTimeImage2 = imagesPath + "Second.jpg";
            string runTimeImage3 = imagesPath + "Third.jpg";
            #endregion


            #region OpenImageAndEdit1

            FileUtilities.OpenFile(originalImage);
            wndImagePro.ClickTabAdjust();
            wndImagePro.ClickBtnRotate();

            wndImagePro.ClickTabMeasure();
            wndImagePro.ClickBtnCreate();

            System.Threading.Thread.Sleep(1000);

            wndImagePro.ClickTabCapture();
            wndImagePro.ClickBtnSaturation();
            //Select Saturation

            wndImagePro.ImageViewer.ClickPoint();
            wndImagePro.ImageViewer.CaptureImage(runTimeImage1);

            #endregion

            wndImagePro.CloseAllToolsAndViews(true);

            #region OpenImageAndEdit Partial

            //System.Threading.Thread.Sleep(2000);
            FileUtilities.OpenFile(originalImage);
            FileUtilities.OpenFile(originalImage);

            wndImagePro.ClickTabAdjust();
            wndImagePro.ClickBtnRotate();

            wndImagePro.ClickTabMeasure();
            wndImagePro.ClickBtnCreate();

            wndImagePro.ClickTabCapture();
            wndImagePro.ClickBtnSaturation();
            //Unselect Saturation

            wndImagePro.ImageViewer.ClickPoint();
            wndImagePro.ImageViewer.CaptureImage(runTimeImage2);

            #endregion

            
            image1 = Image.FromFile(runTimeImage1);
            image2 = Image.FromFile(runTimeImage2);

            compare = ImageComparer.Compare(image1, image2, diff, out Image outPutFile);
            outPutFile.Save(imagesPath + "Output1.jpg");
            Assert.IsFalse(compare, "Images should not match");

            #region Continue transform

            wndImagePro.ClickTabCapture();
            wndImagePro.ClickBtnSaturation();
            //Select Saturation

            wndImagePro.ImageViewer.ClickPoint();
            wndImagePro.ImageViewer.CaptureImage(runTimeImage3);


            image3 = Image.FromFile(runTimeImage3);

            diff = new ColorDifference(0);
            compare = ImageComparer.Compare(image1, image3, diff, out outPutFile);
            outPutFile.Save(imagesPath + "Output2.jpg");
            Assert.IsTrue(compare, "Images should match");

            #endregion
        }

        [TestMethod, Description("Transform Image Partially and compare, the comparision to match, test should fail")]
        public void TransformPartiallyAndCompare_ExpectedFail()
        {
            #region Fields
            bool compare = false;
            Image image1;
            Image image2;
            string imagesPath = @"C:\AutomationImages\" + System.Reflection.MethodBase.GetCurrentMethod().Name + @"\";
            ColorDifference diff = new ColorDifference(0);
            string originalImage = imagesPath + "Actual.jpg";
            string runTimeImage1 = imagesPath + "First.jpg";
            string runTimeImage2 = imagesPath + "Second.jpg";
           
            #endregion

            #region OpenImageAndEdit1

            FileUtilities.OpenFile(originalImage);
            wndImagePro.ClickTabAdjust();
            wndImagePro.ClickBtnRotate();

            wndImagePro.ClickTabMeasure();
            wndImagePro.ClickBtnCreate();

            System.Threading.Thread.Sleep(1000);

            wndImagePro.ClickTabCapture();
            wndImagePro.ClickBtnSaturation(); //Select Saturation

            wndImagePro.ImageViewer.ClickPoint();
            wndImagePro.ImageViewer.CaptureImage(runTimeImage1);

            #endregion

            wndImagePro.CloseAllToolsAndViews(true);

            #region OpenImageAndEdit Partial

            //System.Threading.Thread.Sleep(2000);
            FileUtilities.OpenFile(originalImage);
            FileUtilities.OpenFile(originalImage);

            wndImagePro.ClickTabAdjust();
            wndImagePro.ClickBtnRotate();

            wndImagePro.ClickTabMeasure();
            wndImagePro.ClickBtnCreate();

            wndImagePro.ClickTabCapture();
            wndImagePro.ClickBtnSaturation(); //Unselect Saturation

            wndImagePro.ImageViewer.ClickPoint();
            wndImagePro.ImageViewer.CaptureImage(runTimeImage2);

            #endregion

           
            image1 = Image.FromFile(runTimeImage1);
            image2 = Image.FromFile(runTimeImage2);
            compare = ImageComparer.Compare(image1, image2, diff, out Image outPutFile);
            outPutFile.Save(imagesPath + "Output1.jpg");
            Assert.IsTrue(compare, "Falsy expecting: Images should not match");
        }
    }
}