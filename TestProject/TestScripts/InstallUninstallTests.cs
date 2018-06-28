using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security;
using System.Text;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject.TestScripts
{
    [CodedUITest]
    public class InstallUninstallTests
    {

        #region Fields
        string scriptText;
        Runspace runspace;
        Pipeline pipeline;
        Collection<PSObject> results;
        #endregion
        [TestMethod]
        public void InstallNotepad()
        {
            //Start-Process "C:\sw\notepad.exe" -ArgumentList "/S" -Wait
            scriptText = "Start-Process \"C:\\sw\\notepad.exe\" -ArgumentList \"/S\" -Wait";
            runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript(scriptText);
            pipeline.Commands.Add("Out-String");
            results = pipeline.Invoke(); // execute the script
            runspace.Close(); // close the runspace


            Assert.IsTrue(CheckNotepadInstalled(), "Software should be installed");

        }

        [TestMethod]
        public void UninstallNotepad()
        {

            if (CheckNotepadInstalled())
            {
                //Program is installed, proceed with uninstallation
                string scriptText =
                    "$app = Get-WmiObject -Class Win32_Product | Where-Object { $_.DisplayName -like \"*Notepad++*\" } \n$app.Uninstall()";

                runspace = RunspaceFactory.CreateRunspace();
                runspace.Open();
                pipeline = runspace.CreatePipeline();
                pipeline.Commands.AddScript(scriptText);
                pipeline.Commands.Add("Out-String");
                pipeline.Invoke(); // execute the script

                runspace.Close(); // close the runspace
                Assert.IsFalse(CheckNotepadInstalled(), "Software should be uninstalled");
            }
            else Console.WriteLine("Software uninstalled already");

        }

        public bool CheckNotepadInstalled()
        {
            #region mock

            //32 bit //(gp HKLM:\Software\Microsoft\Windows\CurrentVersion\Uninstall\* | ? { $_.DisplayName - like "*Image-Pro*" }) -ne $null
            string scriptText =
                "(gp HKLM:\\SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\* | ? { $_.DisplayName -like \"*Notepad*\" }) -ne $null"; //64 bit
            runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript(scriptText);
            pipeline.Commands.Add("Out-String");

            #endregion

            return Convert.ToBoolean(pipeline.Invoke()[0].ToString().Trim());
        }

        #region Inprogress

        [TestMethod]
        public void GetInstalledApps()
        {
            //Process.Start(@"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe", @"-File C:\sw\Scripts\CheckIfInstalled.ps1");

            var pass = new SecureString();
            pass.AppendChar('1');
            pass.AppendChar('2');
            pass.AppendChar('3');
            pass.AppendChar('4');
            pass.AppendChar('5');
            pass.AppendChar('6');
            Process.Start(@"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe",
                @"-File C:\sw\Scripts\SilentInstallNp.ps1", "admin", pass, "");


            Process.Start("notepad", "admin", pass, "");


            //echo %PROCESSOR_ARCHITECTURE%

            //            x86(32 bit)
            //Open C:\Windows\SysWOW64\cmd.exe
            //Run the command powershell Set-ExecutionPolicy RemoteSigned

            //x64(64 bit)
            //Open C:\Windows\system32\cmd.exe
            //Run the command powershell Set-ExecutionPolicy RemoteSigned

        }

        [TestMethod]
        public void CheckImageProInstalled()
        {
            string command = File.ReadAllText(@"c:\sw\CheckIfInstalled.txt", Encoding.UTF8);

            RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();
            Command myCommand = new Command(command, true);

            Runspace runspace = RunspaceFactory.CreateRunspace(runspaceConfiguration);
            runspace.Open();

            RunspaceInvoke scriptInvoker = new RunspaceInvoke(runspace);

            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.Add(myCommand);

            // Execute PowerShell script
            var results = pipeline.Invoke();
        }

        #endregion
    }
}