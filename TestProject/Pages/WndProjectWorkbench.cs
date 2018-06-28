using System.Windows.Automation;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using TestProject.Utilities;

namespace TestProject.Pages
{
    /// <summary>
    /// This class represents window 'Project Workbench'
    /// </summary>
    public class WndProjectWorkbench: WinWindow
    {
        #region Properties

        private WinButton BtnYes => this.GetButtonByName("Yes");

        #endregion
        #region Constructor

        public WndProjectWorkbench()
        {
            #region Search Criteria
            AutomationElement rootElement = AutomationElement.RootElement;
            SearchProperties.Add(new PropertyExpression(UITestControl.PropertyNames.Name, "Project Workbench", PropertyExpressionOperator.Contains));
            #endregion
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Click 'Yes' when asked for save confirmation
        /// </summary>
        public void ConfirmSave()
        {
            SetFocus();
            BtnYes.WaitForControlExist();
            BtnYes.Click();
        }

        #endregion


    }
}
