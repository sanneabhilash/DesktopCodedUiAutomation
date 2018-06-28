using System.Windows.Automation;
using System.Windows.Input;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;

namespace TestProject.Pages
{
    /// <summary>
    /// This class represents window 'Macros.vb - Project Workbench'
    /// </summary>
    public class WndMacrosVb : WinWindow
    {
        #region Constructor

        public WndMacrosVb()
        {
            #region Search Criteria
            AutomationElement rootElement = AutomationElement.RootElement;
            SearchProperties[UITestControl.PropertyNames.Name] = "Macros.vb - Project Workbench";
            SearchProperties.Add(new PropertyExpression(UITestControl.PropertyNames.ClassName, "WindowsForms10.Window", PropertyExpressionOperator.Contains));
            WindowTitles.Add("Macros.vb - Project Workbench");
            #endregion
        }

        #endregion

        public void Exit()
        {
            SetFocus();
            Keyboard.SendKeys("{F4}", ModifierKeys.Alt);
        }
        #region ChildWindows

        WndNewProject _wndNewProject;
        public WndNewProject WndNewProject
        {
            get
            {
                if (_wndNewProject == null)
                {
                    _wndNewProject = new WndNewProject();
                }
                return _wndNewProject;
            }
        }
        #endregion
    }
}
