using System;
using System.Windows.Automation;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject.Utilities;

namespace TestProject.Pages
{
    /// <summary>
    /// This class represents the window 'Capture Simulation Control'
    /// </summary>
    public class WndCaptureSuimulationControl : WinWindow
    {
        #region Properties

        private WinComboBox CmbBinningComboBox => this.GetComboboxByControlName("cmbBinning");

        #endregion

        #region Constructor

        public WndCaptureSuimulationControl()
        {
            #region Search Criteria

            AutomationElement rootElement = AutomationElement.RootElement;
            SearchProperties[WinControl.PropertyNames.ControlName] = "DockSite2";
            SearchProperties.Add(new PropertyExpression(UITestControl.PropertyNames.ClassName, "WindowsForms10.Window",
                PropertyExpressionOperator.Contains));

            #endregion
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Select option from Binning dropdown
        /// </summary>
        /// <param name="index">Position index of item to select</param>
        public void SelectBinningOption(int index)
        {
            try
            {
                CmbBinningComboBox.SelectedIndex = index;
                if (CmbBinningComboBox.SelectedIndex == index)
                {
                    Assert.IsTrue(true, "Binning option index: '" + index + "' selected");
                }
                else Assert.Fail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Select option from Binning dropdown
        /// </summary>
        /// <param name="optionText">Visible text of item to select</param>
        public void SelectBinningOption(string optionText)
        {
            try
            {
                CmbBinningComboBox.SelectedItem = optionText;
                if (CmbBinningComboBox.SelectedItem == optionText)
                {
                    Assert.IsTrue(true, "Binning option: '"+ optionText + "' selected" );
                }
                else Assert.Fail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


    }
}
