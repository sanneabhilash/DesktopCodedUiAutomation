using System.Windows.Automation;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject.Utilities;

namespace TestProject.Pages
{
    /// <summary>
    /// This class represents window 'Recording'
    /// </summary>
    public class WndRecording : WinWindow
    {


        #region Properties

        private WinButton BtnPauseRecording => this.GetButtonByName("Pause Recording");

        private WinButton BtnResumeRecording => this.GetButtonByName("Resume Recording");

        private WinButton BtnStopRecording => this.GetButtonByName("Stop Recording");

        #endregion

        #region Constructor

        public WndRecording()
        {
            #region Search Criteria

            AutomationElement rootElement = AutomationElement.RootElement;
            SearchProperties[UITestControl.PropertyNames.Name] = "Recording";
            SearchProperties.Add(new PropertyExpression(UITestControl.PropertyNames.ClassName, "WindowsForms10.Window",
                PropertyExpressionOperator.Contains));
            WindowTitles.Add("Recording");

            #endregion
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Click on button Pause Recording
        /// </summary>
        public void PauseRecording()
        {
            BtnPauseRecording.WaitForControlExist();
            if (BtnPauseRecording.Enabled)
            {
                BtnPauseRecording.Click();
                return;
            }

            Assert.Fail("Button 'Pause Recording' is not enabled in UI");
        }

        /// <summary>
        /// Click on button Resume Recording
        /// </summary>
        public void ResumeRecording()
        {
            BtnResumeRecording.WaitForControlExist();
            if (BtnResumeRecording.Enabled)
            {
                BtnResumeRecording.Click();
                return;
            }

            Assert.Fail("Button 'Resume Recording' is not enabled in UI");
        }

        /// <summary>
        /// Click on button 'Stop' recording
        /// </summary>
        public void StopRecording()
        {
            BtnStopRecording.WaitForControlExist();
            if (BtnStopRecording.Enabled)
            {
                BtnStopRecording.Click();
                return;
            }

            Assert.Fail("Button 'Stop Recording' is not enabled in UI");
        }

        #endregion

    }
}
