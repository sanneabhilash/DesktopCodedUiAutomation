using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject.Utilities
{
    /// <summary>
    /// Class containing extension methods for Mouse UI actions on WinControls
    /// </summary>
    public static class MouseActionsExtension
    {
        /// <summary>
        /// Mouse Left click on given UI control
        /// </summary>
        /// <param name="uiControl">Control to be clicked</param>
        /// <param name="checkEnabled"></param>
        public static void Click(this WinControl uiControl, bool checkEnabled = true)
        {
            try
            {
                if (uiControl.Exists)
                {
                    if (checkEnabled)
                    {
                        if (uiControl.Enabled)
                        {
                            uiControl.WaitForControlEnabled();
                            Mouse.Click(uiControl);
                        }
                        else Assert.Fail("Failed to perform Click operation, control not enabled in UI");
                    }
                    else
                    {
                        uiControl.WaitForControlExist();
                        Mouse.Click(uiControl);
                    }
                }
                else Assert.Fail("Failed to perform Click operation, control not available in UI");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Mouse 'Right Click' on given UI control
        /// </summary>
        /// <param name="uiControl">Control to be clicked</param>
        public static void RightClick(this WinControl uiControl)
        {
            Mouse.Click(uiControl, MouseButtons.Right);
        }

        /// <summary>
        /// Click on center of given UI control
        /// </summary>
        /// <param name="uiControl">Control to be clicked</param>
        /// <param name="top">Number of pixels to deviate from center</param>
        public static void ClickPoint(this WinControl uiControl, int top = 0)
        {
            var pnt = new Point(uiControl.Left + uiControl.Width / 2, uiControl.Top + uiControl.Height / 2 + top);
            Mouse.Click(pnt);
        }

        /// <summary>
        /// Mouse 'Right Click' on given UI controls Midpoint
        /// </summary>
        /// <param name="uiControl">Control to be clicked</param>
        public static void ClickPoint_RightClick(this WinControl uiControl)
        {
            Console.WriteLine("Right click on control" + uiControl.AccessibleDescription);
            Point pnt = new Point(uiControl.Left + uiControl.Width / 2, uiControl.Top + uiControl.Height / 2);
            Mouse.Click(MouseButtons.Right, System.Windows.Input.ModifierKeys.None, pnt);
        }
    }
}
