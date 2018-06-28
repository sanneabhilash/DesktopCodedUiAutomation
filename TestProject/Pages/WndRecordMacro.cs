using System;
using System.Windows.Automation;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using TestProject.Utilities;

namespace TestProject.Pages
{
    /// <summary>
    /// This class represents the window 'Record Macro'
    /// </summary>
    public class WndRecordMacro : WinWindow
    {
        #region Properties

        private WinButton BtnOk => this.GetButtonByName("OK");

        private WinButton BtnCancel => this.GetButtonByName("Cancel");

        private WinEdit TxtNewMacro
        {
            get
            {
                var mTxtNewMacro = new WinEdit(this);

                #region Search Criteria

                mTxtNewMacro.WindowTitles.Add("Record Macro");
                this.SearchProperties[WinControl.PropertyNames.ControlName] = "TextBox";

                #endregion

                return mTxtNewMacro;
            }
        }
        #endregion

        #region Constructor

        public WndRecordMacro()
        {
            #region Search Criteria
            AutomationElement rootElement = AutomationElement.RootElement;
            this.SearchProperties[UITestControl.PropertyNames.Name] = "Record Macro";
            this.SearchProperties.Add(new PropertyExpression(UITestControl.PropertyNames.ClassName, "WindowsForms10.Window", PropertyExpressionOperator.Contains));
            this.WindowTitles.Add("Record Macro");
            #endregion
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Click on OK button
        /// </summary>
        public void ClickOk()
        {
            Console.WriteLine("Click 'OK' button in window Record Macro");
            BtnOk.Click();
        }

        /// <summary>
        /// Fill 'New Macro' form
        /// </summary>
        /// <param name="newMacroName"></param>
        public void Fill(string newMacroName)
        {
            TxtNewMacro.Text = newMacroName;
        }

       

        #endregion

    }
}
