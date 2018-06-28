using System;
using System.Drawing.Imaging;
using System.Windows.Input;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;

namespace TestProject.Utilities
{
    public static class UiActionsExtension
    {
        public static bool HandleWindow(this WinWindow parentWindow, string windowTitle, string buttonName)
        {
            bool exists = false;
            WinWindow childWindow = new WinWindow(parentWindow);
            childWindow.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.Name, windowTitle, PropertyExpressionOperator.EqualTo));

            exists = childWindow.WaitForControlExist(1000);
            if (exists)
            {
                WinButton btn = new WinButton(childWindow);
                btn.SearchProperties[WinButton.PropertyNames.Name] = buttonName;
                btn.Click();
            }
            return exists;
        }


        /// <summary>
        /// Close the current Window by pressing Alt+F4
        /// </summary>
        public static void CloseWindow(this WinWindow window)
        {
            string title = window.Name ?? "";
            Console.WriteLine("Close window: " + title);
            window.SetFocus();
            Keyboard.SendKeys("{F4}", ModifierKeys.Alt);
        }

        /// <summary>
        /// Capture the UI control as an Image during runtime
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="fullFilePath">Full path of the file system location</param>
        /// <param name="format">Image output ImageFormat, default is Jpeg</param>
        /// <param name="hover">True, hover on control</param>
        public static void CaptureImage(this UITestControl ctrl, string fullFilePath, ImageFormat format = null, bool hover = false)
        {
            Console.WriteLine("Capturing Image: " + fullFilePath);
            format = format ?? ImageFormat.Jpeg;
            ctrl.SetFocus();
            if (hover)
            {
                Mouse.HoverDuration = 1000;
                Mouse.Hover(ctrl);
            }
            ctrl.CaptureImage().Save(fullFilePath, format);
        }
    }
}
