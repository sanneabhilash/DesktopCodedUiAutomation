using System.Windows.Automation;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject.Utilities;

namespace TestProject.Pages
{
    /// <summary>
    /// This class represents window 'New Project'
    /// </summary>
    public class WndNewProject : WinWindow
    {
        #region Constructor

        public WndNewProject()
        {
            #region Search Criteria
            AutomationElement rootElement = AutomationElement.RootElement;
            this.SearchProperties[WinWindow.PropertyNames.Name] = "New Project";
            this.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.ClassName, "WindowsForms10.Window", PropertyExpressionOperator.Contains));
            this.WindowTitles.Add("New Project");
            #endregion
        }

        #endregion

        #region Properties

        private WinEdit _mTxtModule;
        private WinButton BtnCancel => this.GetButtonByName("Cancel");

        public WpfCheckBox ChkRecordingTarget
        {
            get
            {
                var txtProps = new WpfCheckBox(this) { TechnologyName = "MSAA" };
                txtProps.SearchProperties.Add("Name", "Recording Target");
                return txtProps;
            }
        }

        public WpfCheckBox ChkNewApp
        {
            get
            {
                var txtProps = new WpfCheckBox(this) { TechnologyName = "MSAA" };
                txtProps.SearchProperties.Add("Name", "New App:");
                return txtProps;
            }
        }
        #endregion

        #region Fields

        public WinEdit TxtModule
        {
            get
            {
                if (this._mTxtModule == null)
                {
                    this._mTxtModule = new WinEdit(this);
                    #region Search Criteria
                    this._mTxtModule.WindowTitles.Add("New Project");
                    this.SearchProperties[WinWindow.PropertyNames.ControlName] = "txtModule";
                    this.SearchProperties[WinWindow.PropertyNames.ClassName] = "WindowsForms10.EDIT";

                    #endregion
                }
                return this._mTxtModule;
            }
        }

        #region Public Methods

        /// <summary>
        /// Click on Cancel button
        /// </summary>
        public void ClickCancel()
        {
            BtnCancel.WaitForControlExist();
            BtnCancel.ClickPoint();
        }

       

       

        #endregion
        public void selectCheckBox(string Name)
        {

            WpfCheckBox btn = new WpfCheckBox(this);
            btn.TechnologyName = "MSAA";
            btn.SearchProperties[WpfCheckBox.PropertyNames.Name] = Name;

            if (!btn.Checked)
                Mouse.Click(btn);
        }
        public void unSelectcheckBox(string Name)
        {
            WpfCheckBox btn = new WpfCheckBox(this);
            btn.TechnologyName = "MSAA";
            btn.SearchProperties[WpfCheckBox.PropertyNames.Name] = Name;

            if (btn.Checked)
                Mouse.Click(btn);
        }
        public void VerifyCheckBoxIsSelected(string Name, bool selected = true, string message = "")
        {
            WpfCheckBox btn = new WpfCheckBox(this);
            btn.TechnologyName = "MSAA";
            btn.SearchProperties[WpfCheckBox.PropertyNames.Name] = Name;

            if (selected)
            {
                message = message == "" ? "Checkbox: " + Name + " is selected as expected" : message;
                Assert.IsTrue(btn.Checked, message);
            }
            else
            {
                message = message == "" ? "Checkbox: " + Name + " is not Selected as expected" : message;
                Assert.IsFalse(btn.Checked, message);
            }
        }

        public void VerifyCheckBoxEnabled(string name, bool enabled = true)
        {
            WpfCheckBox btn = new WpfCheckBox(this);
            btn.TechnologyName = "MSAA";
            btn.SearchProperties[WpfCheckBox.PropertyNames.Name] = name;

            if (enabled)
            {
                Assert.IsTrue(btn.Enabled, "Checkbox should be enabled ");
            }
            else Assert.IsFalse(btn.Enabled, "Checkbox should be disabled");
        }
        #endregion
    }
}
