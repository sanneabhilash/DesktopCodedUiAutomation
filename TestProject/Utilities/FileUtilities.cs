using System;
using System.IO;
using System.Windows.Input;
using AutoItX3Lib;
using Microsoft.VisualStudio.TestTools.UITesting;
using TestProject.Pages;

namespace TestProject.Utilities
{
   public  static class FileUtilities
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool CheckIfFileExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return false;//throw new FileNotFoundException("File '" + filePath + "' not found!");
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string CheckAndCreateDirectory(string filePath)
        {
            if (!Directory.Exists(filePath)){
                Directory.CreateDirectory(filePath);
            }
            return filePath;
        }

        /// <summary>
        /// Open a file from local system
        /// </summary>
        /// <param name="fullFilePath">Full path of the file (along with extension)</param>
        public static void OpenFile(string fullFilePath)
        {
            Console.WriteLine("Open File: " + fullFilePath);
            WndImagePro wndImagePro = new WndImagePro();
            wndImagePro.SetFocus();

            Keyboard.SendKeys("O", ModifierKeys.Control);
            System.Threading.Thread.Sleep(2000);
            AutoItX3 autoIT = new AutoItX3();
            autoIT.WinWait("Open");
            autoIT.WinActivate("Open");
            autoIT.ControlFocus("Open", "", "[CLASS:Edit]");
            autoIT.ControlSetText("Open", "", "[CLASS:Edit]", fullFilePath);
            //autoIT.Send(@"Actual.jpg");
            // autoIT.ControlClick("Open", "", "[CLASS:Button; Text: Open]");
            Keyboard.SendKeys("O", ModifierKeys.Alt);
        }
    }
}
