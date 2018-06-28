using System;
using Microsoft.VisualStudio.TestTools.UITest.Input;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject.BasePage;
using TestProject.Pages;
using TestProject.Utilities;

namespace TestProject.TestScripts
{
    /// <summary>
    /// Class containing Simple UI automation tests
    /// </summary>
    [CodedUITest]
    public class AutomatedTests : TestBase
    {

        #region Fields
        WndImagePro wndImagePro;
        Random rand = new Random();
        #endregion

        #region Properties
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }
        #endregion

        #region setup and clean up methods
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Code to open the logFile object
        }
        [TestInitialize]
        public void Setup()
        {
            wndImagePro = new WndImagePro();
            wndImagePro.SetFocus();
            Console.WriteLine("Begin Execution: " + TestContext.TestName);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Console.WriteLine("End Execution: " + TestContext.TestName);
        }
        #endregion


        [TestMethod, Description("Record a Macro, Execute recorded macro and compare the result")]
        public void TestRecordMacroWithAction()
        {
            #region TestData
            string imagesPath = FileUtilities.CheckAndCreateDirectory(testImagesPath + System.Reflection.MethodBase.GetCurrentMethod().Name + @"\");
            string originalImage = testImagesPath + "Actual.jpg";
            string runTimeImage1 = imagesPath + "First.jpg";
            string runTimeImage2 = imagesPath + "Second.jpg";
            string newMacroName = "AutoTest" + rand.Next(100, 999999);
            float diff;
            #endregion

            wndImagePro.ClickTabAutomate();
            wndImagePro.StartRecodingMacro(newMacroName);

            #region Record New Macro and Save

            FileUtilities.OpenFile(originalImage);
            wndImagePro.ClickTabAdjust();
            wndImagePro.ClickBtnRotate();
            System.Threading.Thread.Sleep(3000);

            wndImagePro.WndRecording.PauseRecording();
            System.Threading.Thread.Sleep(1000);

            wndImagePro.WndRecording.ResumeRecording();
            wndImagePro.ClickTabMeasure();
            wndImagePro.ClickBtnCreate();
            System.Threading.Thread.Sleep(3000);

            wndImagePro.WndRecording.StopRecording();

            ////WndProjectWorkbench WndProjectWorkbench = new WndProjectWorkbench();
            ////WndProjectWorkbench.ConfirmSave();
            wndImagePro.ImageViewer.CaptureImage(runTimeImage1);

            #endregion

            #region ReOpen Saved Macro
            wndImagePro.CloseAllToolsAndViews(true);

            wndImagePro.ClickTabAutomate();
            Assert.IsTrue(wndImagePro.CheckRecordedMacroExists(newMacroName), "Should be true");
            wndImagePro.ClickTabAutomate();
            wndImagePro.SelectRecordedMacro(newMacroName);
            System.Threading.Thread.Sleep(5000); //Wait for macro execution
            wndImagePro.ImageViewer.CaptureImage(runTimeImage2);

            #endregion

            #region Verify Macro Output Images
            //ColorDifference diff = new ColorDifference(0);
            //Image image1 = Image.FromFile(runTimeImage1);
            //Image image2 = Image.FromFile(runTimeImage2);

            //Image outPutFile = null;
            //bool compar2e = ImageComparer.Compare(image1, image2, diff, out outPutFile);
            //outPutFile.Save(imagesPath + "Output.jpg");
            //Assert.IsTrue(compar2e, "Images should match");

            diff = ImageUtilities.GetPercentageDifference(runTimeImage1, runTimeImage2);
            Assert.IsTrue(diff < 0.02, "Images should be Identical");
            #endregion
        }

        [TestMethod, Description("Test Case PremCore-2360:10 to Test Case PremCore-2365: 60")]
        public void VerifyCheckboxFunctionality()
        {
            //Verify functionality of Checkboxes
            wndImagePro.ClickTabAutomate();
            wndImagePro.ClickBtnNewProject();

            #region Verify Disabled Checkbox

            wndImagePro.WndMacrosVb.WndNewProject.unSelectcheckBox("New Module:");
            Assert.IsFalse(wndImagePro.WndMacrosVb.WndNewProject.ChkRecordingTarget.Enabled, "Checkbox should be in disabled state");
            Assert.IsFalse(wndImagePro.WndMacrosVb.WndNewProject.ChkRecordingTarget.Checked, "Checkbox should be in unchecked state");
            wndImagePro.WndMacrosVb.WndNewProject.selectCheckBox("Recording Target");
            Assert.IsFalse(wndImagePro.WndMacrosVb.WndNewProject.ChkRecordingTarget.Checked, "Checkbox should be in unchecked state");

            #endregion

            #region Parent Control > Toggle State
            wndImagePro.WndMacrosVb.WndNewProject.VerifyCheckBoxEnabled("Recording Target", false);

            wndImagePro.WndMacrosVb.WndNewProject.selectCheckBox("New App:");
            wndImagePro.WndMacrosVb.WndNewProject.selectCheckBox("New Module:");
            wndImagePro.WndMacrosVb.WndNewProject.VerifyCheckBoxEnabled("Recording Target");
            wndImagePro.WndMacrosVb.WndNewProject.selectCheckBox("Recording Target");
            wndImagePro.WndMacrosVb.WndNewProject.selectCheckBox("New Report:");


            wndImagePro.WndMacrosVb.WndNewProject.VerifyCheckBoxIsSelected("New App:");
            wndImagePro.WndMacrosVb.WndNewProject.VerifyCheckBoxIsSelected("New Module:");
            wndImagePro.WndMacrosVb.WndNewProject.VerifyCheckBoxIsSelected("New Report:");


            wndImagePro.WndMacrosVb.WndNewProject.unSelectcheckBox("New App:");
            wndImagePro.WndMacrosVb.WndNewProject.unSelectcheckBox("New Module:");
            wndImagePro.WndMacrosVb.WndNewProject.VerifyCheckBoxEnabled("Recording Target", false);
            wndImagePro.WndMacrosVb.WndNewProject.unSelectcheckBox("New Report:");

            #endregion

            wndImagePro.WndMacrosVb.WndNewProject.VerifyCheckBoxIsSelected("New App:", false);
            wndImagePro.WndMacrosVb.WndNewProject.VerifyCheckBoxIsSelected("New Module:", false);
            wndImagePro.WndMacrosVb.WndNewProject.VerifyCheckBoxIsSelected("New Report:", false);
            wndImagePro.WndMacrosVb.WndNewProject.VerifyCheckBoxIsSelected("Recording Target", true);

            #region Space Bar > Toggle State
            //New App checkbox is in selected state
            wndImagePro.WndMacrosVb.WndNewProject.ChkNewApp.SetFocus();
            Keyboard.SendKeys("{SPACE}"); //unselecting the checkbox using 'SPACE' key
            wndImagePro.WndMacrosVb.WndNewProject.VerifyCheckBoxIsSelected("New App:", true, "Able to Change checkbox state with keyboard");

            //Check if using 'SPACE' key you can select and unselect checkbox
            wndImagePro.WndMacrosVb.WndNewProject.ChkNewApp.SetFocus();
            Keyboard.SendKeys("{SPACE}");
            wndImagePro.WndMacrosVb.WndNewProject.VerifyCheckBoxIsSelected("New App:", false, "Able to Change checkbox state with keyboard");

            #endregion

            #region Tab key focuses on Checkbox
            wndImagePro.WndMacrosVb.WndNewProject.ChkNewApp.SetFocus();
            Keyboard.SendKeys("{TAB}");
            Keyboard.SendKeys("{TAB}");
            //Focus is shifted to 'New Report' checkbox and is selected
            Keyboard.SendKeys("{SPACE}");
            wndImagePro.WndMacrosVb.WndNewProject.VerifyCheckBoxIsSelected("New Report:");

            #endregion

            wndImagePro.WndMacrosVb.WndNewProject.ClickCancel();
            wndImagePro.WndMacrosVb.Exit();
        }

        [TestMethod, Description("Test Case PremCore-2383: Help Availability")]
        public void VerifyHelpButtonAvailability()
        {
            wndImagePro.ClickTabAdjust();
            Assert.IsTrue(wndImagePro.Click_btnDisplayHelp(false).Enabled, "Help button should be enabled");
            wndImagePro.Click_btnDisplayHelp();
            Assert.IsTrue(wndImagePro.WndImageProOnlineHelp.Exists, "Help window should be displayed");
            wndImagePro.WndImageProOnlineHelp.CloseWindow();
        }

        [TestMethod, Description("Test Case PremCore-2155: 10: Add To Quick Access Toolbar.")]
        public void VerifyQuickAccessToolbar()
        { 
            wndImagePro.ClickTabAutomate();

            #region setup   
            if (wndImagePro.GetButtonsByName("Record Macro").Count == 2)
            {
                wndImagePro.ClickBtnRecordMacro(MouseButtons.Right);
                GetContextMenu().SelectMenuItem("Remove from Quick Access Toolbar");
            }
            #endregion  

            wndImagePro.ClickBtnRecordMacro(MouseButtons.Right);
            GetContextMenu().SelectMenuItem("Add to Quick Access Toolbar");

            Assert.AreEqual(2, wndImagePro.GetButtonsByName("Record Macro").Count, "'Record Macro' should be added to quick access section");

            wndImagePro.ClickBtnRecordMacro(MouseButtons.Right);
            GetContextMenu().SelectMenuItem("Remove from Quick Access Toolbar");

            Assert.AreEqual(1, wndImagePro.GetButtonsByName("Record Macro").Count, "'Record Macro' should be removed from quick access section");
        }
    }
}
