using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using TestProject.Utilities;

namespace TestProject.Pages
{
    /// <summary>
    /// This class represents 'Save confirmation' window with title 'Image-Pro'
    /// </summary>
    public class WndConfirmSave : WinWindow
    {
        #region Properties
        private WinButton BtnNoToAll => this.GetButtonByName("No to All");

        #endregion

        #region Constructor
        public WndConfirmSave(UITestControl searchLimitContainer) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinControl.PropertyNames.ControlName] = "btnNoAll";
            this.WindowTitles.Add("Image-Pro");
            #endregion
        }

        #endregion


        

    }
}
