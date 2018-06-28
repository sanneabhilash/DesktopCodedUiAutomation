using System;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using TestProject.BasePage;
using TestProject.Pages;
using TestProject.Utilities;

namespace TestProject
{
    /// <summary>
    /// Summary description for CodedUITest2
    /// </summary>
    [CodedUITest]
    public class InprogressTests : TestBase
    {
        WndImagePro wndImagePro;
        #region Windows
        //ImagePro imageProWindow;
        #endregion

        #region Setup   
        public InprogressTests()
        {

        }
        [TestInitialize]
        public void Test_Setup()
        {
            wndImagePro = new WndImagePro();
            wndImagePro.SetFocus();
        }
        #endregion


        [TestMethod, Description("7 8.2.2 Test Suite : Status Bar Context Menu"), Obsolete]
        public void StatusBar_Test_Blocked()
        {
            //Test Case PremCore-3313: 60: Pixel Value Option

            //The status bar items 'status' does not change when the option is selected or unselected

            //In UI the option is working as expected, but the controls isVisible property is not avaialble to test
            string originalImage = @"C:\AutomationImages\Actual.jpg";

            FileUtilities.OpenFile(originalImage);
            wndImagePro.StatusBar.ClickPoint_RightClick();

            GetContextMenu().SelectMenuItem("Pixel value", true);
            var statusOption = wndImagePro.StatusBar.GetStatusOption("Pixel value");


            Console.WriteLine("\nControl Status when selected");
            Console.WriteLine("Control Exists: " + statusOption.Exists);
            Console.WriteLine("Control State: " + statusOption.State);
            Console.WriteLine("Control Focus: " + statusOption.HasFocus);
            Console.WriteLine("Control Enabled: " + statusOption.Enabled);

            Console.WriteLine();
            GetContextMenu().SelectMenuItem("Pixel value", false);
            statusOption = wndImagePro.StatusBar.GetStatusOption("Pixel value");
            Console.WriteLine("\nControl Status when unselected");
            Console.WriteLine("Control Exists when TryGetClickablePoint: " + statusOption.Exists);
            Console.WriteLine("Control State: " + statusOption.State);
            Console.WriteLine("Control Focus: " + statusOption.HasFocus);
            Console.WriteLine("Control Enabled: " + statusOption.Enabled);
        }

        [TestMethod, Description("Test Case PremCore-3575: 430: Cut Button Tooltip"), Obsolete]
        public void CutButton_ToolTip_Blocked()
        {
            WndImagePro wndImagePro = new WndImagePro();
            wndImagePro.ClickTabAutomate();

            wndImagePro.BtnOutput.SetFocus();
            Mouse.HoverDuration = 2000;
            Mouse.Hover(wndImagePro.BtnOutput);
            // var test = wndImagePro.BtnRecordMacro.ToolTipText;

            WinToolTip toolTip = new WinToolTip(wndImagePro);
            toolTip.SearchProperties.Add(new PropertyExpression(WinToolTip.PropertyNames.Name, "Record Macro", PropertyExpressionOperator.Contains));


            var text = wndImagePro.BtnOutput.ToolTipText;
            var stri = toolTip.HelpText;
            var test = (WinButton)wndImagePro.BtnOutput.FindMatchingControls()[0];

        }


        [TestMethod, Description("Test Case PremCore-2407: 200: Illegal Characters > SSN")]
        public void TextBox_Validation()
        {
            //Verify a field expecting a Social Security Number will handle illegal characters correctly. Illegal characters for a Social Security Number:

        }

       

    }
}
