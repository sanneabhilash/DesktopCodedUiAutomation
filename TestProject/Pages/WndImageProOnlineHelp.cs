using System.Windows.Automation;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;

namespace TestProject.Pages
{
    /// <summary>
    /// This class represents window 'Image-Pro Online Help'
    /// </summary>
    public class WndImageProOnlineHelp : WinWindow
    {
        #region Constructor

        public WndImageProOnlineHelp()
        {
            #region Search Criteria
            AutomationElement rootElement = AutomationElement.RootElement;
            SearchProperties.Add(new PropertyExpression(UITestControl.PropertyNames.Name, "Image-Pro Online Help", PropertyExpressionOperator.Contains));
            #endregion
        }

        #endregion

        #region Public Methods
        
        #endregion

    }
}
