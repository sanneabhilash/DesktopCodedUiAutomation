using System.Threading;
using System.Windows.Automation;
using Microsoft.VisualStudio.TestTools.UITest.Input;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using TestProject.Utilities;

namespace TestProject.Pages
{
    public class WndImagePro : WinWindow
    {
        #region Properties

        #region TabButtons

        private WinButton TabAutomate => this.GetButtonByName("Automate");

        private WinButton TabCapture => this.GetButtonByName("Capture");

        private WinButton TabView => this.GetButtonByName("View");

        private WinButton TabAdjust => this.GetButtonByName("Adjust");

        private WinButton TabMeasure => this.GetButtonByName("Measure");

        #endregion

        #region Toolbar Buttons

        public WinButton BtnOutput => this.GetButtonByName("Output");


        private WinButton BtnRecordMacro => this.GetButtonByName("Record Macro");
        private WinButton BtnCloseAllTools => this.GetButtonByName("Close All Tools");
        private WinButton BtnCloseAllViews => this.GetButtonByName("Close All Views");
        private WinButton BtnBinaryMask => this.GetButtonByName("Binary Mask");
        private WinGroup BtnSaturation => this.GetWinGroupByName("Saturation");
        private WinButton BtnFullScreen => this.GetButtonByName("Full Screen");
        private WinButton BtnNewProject => this.GetButtonByName("New Project");

        #endregion

        #region Group Buttons

        private WinGroup BtnRotate => this.GetWinGroupByName("Rotate");
        private WinGroup BtnCreate => this.GetWinGroupByName("Create");

        #endregion

        #region Custom Controls

        private WinCustom BtnCount => this.GetWinCustomByName("Count");

        #region UI Controls


        public WinClient ImageViewer
        {
            get
            {
                WinClient imagePane = new WinClient(this);
                imagePane.SearchProperties.Add(new PropertyExpression(UITestControl.PropertyNames.ClassName,
                    "McView_Window_Class", PropertyExpressionOperator.Contains));
                imagePane.SearchProperties.Add(new PropertyExpression(WinClient.PropertyNames.ControlName, "AxMcView",
                    PropertyExpressionOperator.Contains));
                return imagePane;
            }
        }

        public WinStatusBar StatusBar
        {
            get
            {
                var txtProps = new WinStatusBar(this) { TechnologyName = "MSAA" };
                txtProps.SearchProperties.Add("Name", "DotNetBar Bar");
                return txtProps;
            }
        }

        #endregion

        #endregion

        #region SplitButtons

        private WinSplitButton ProjectExplorerSplitButton => this.GetWinSplitButton("Project Explorer");

        #endregion

        #endregion

        #region Constructor

        public WndImagePro()
        {
            #region Search Criteria

            AutomationElement rootElement = AutomationElement.RootElement;
            SearchProperties[UITestControl.PropertyNames.Name] = "Image-Pro";
            SearchProperties[UITestControl.PropertyNames.ControlType] = "Client";
            SearchProperties.Add(new PropertyExpression(UITestControl.PropertyNames.ClassName, "WindowsForms10.Window",
                PropertyExpressionOperator.Contains));
            WindowTitles.Add("Image-Pro");

            #endregion
        }

        #endregion

        #region Public Methods

        #region Click On Tab Buttons

        public void ClickTabMeasure()
        {
            TabMeasure.ClickPoint();
        }

        public void ClickTabAutomate()
        {
            TabAutomate.ClickPoint();
        }
        public void ClickTabAdjust()
        {
            TabAdjust.ClickPoint();
        }

        public void ClickTabCapture()
        {
            TabCapture.WaitForControlEnabled();
            TabCapture.WaitForControlReady();
            TabCapture.ClickPoint();
        }
        public void ClickTabView()
        {
            TabView.ClickPoint();
        }


        #endregion

        #region Click On Button

        public void ClickBtnNewProject()
        {
            BtnNewProject.Click();
        }
        public void ClickBtnSaturation()
        {
            BtnSaturation.Click();
        }
        public void ClickBtnRecordMacro(MouseButtons action = MouseButtons.Left)
        {
            switch(action)
            {
                case MouseButtons.Left:
                    BtnRecordMacro.ClickPoint();
                    break;

                case MouseButtons.Right:
                    BtnRecordMacro.ClickPoint_RightClick();
                    break;
            }
        }

        public void ClickBtnRotate()
        {
            BtnRotate.Click();
        }

        public void ClickBtnCreate()
        {
            BtnCreate.Click();
        }



        #endregion


        public void StartRecodingMacro(string newMacroName)
        {
            SetFocus();
            ClickTabAutomate();
            ClickBtnRecordMacro();

            WndRecordMacro.Fill(newMacroName);
            WndRecordMacro.ClickOk();
        }

        public bool CheckRecordedMacro(bool click = false)
        {
            return false;
        }
        /// <summary>
        /// Enter or Exit Image Viewer Full Screen Mode
        /// </summary>
        /// <param name="on">'True' for entering full screen mode,
        /// 'False' for exiting full screen mode</param>
        public void FullScreenMode(bool on = true)
        {
            if (!on)
            {
                SetFocus();
                Keyboard.SendKeys("{ESC}");
            }
            else if (BtnFullScreen.Enabled)
                BtnFullScreen.Click();

            Thread.Sleep(3000);
        }

        /// <summary>
        /// Get Help button control in Window 'Image-Pro'
        /// </summary>
        /// <param name="click">'True' to Click on 'Help' button</param>
        /// <returns></returns>
        public WinButton Click_btnDisplayHelp(bool click = true)
        {
            var btnDisplayHelp = this.GetButtonByName("Display");
            if (click)
            {
                btnDisplayHelp.SetFocus();
                Mouse.HoverDuration = 1000;
                Mouse.Hover(btnDisplayHelp);
                Mouse.Click(btnDisplayHelp);
            }
            return btnDisplayHelp;
        }

        public void SelectRecordedMacro(string name)
        {
            ProjectExplorerSplitButton.Click();

            WinMenu uiContextMenu = new WinMenu(this);
            uiContextMenu.WindowTitles.Add("Image-Pro");


            WinMenuItem UIMenuItem = new WinMenuItem(uiContextMenu);
            UIMenuItem.SearchProperties[UITestControl.PropertyNames.Name] = name;
            UIMenuItem.Click();
        }

        public bool CheckRecordedMacroExists(string name)
        {
            ProjectExplorerSplitButton.Click();

            WinMenu uiContextMenu = new WinMenu(this);
            uiContextMenu.WindowTitles.Add("Image-Pro");


            WinMenuItem UIMenuItem = new WinMenuItem(uiContextMenu);
            UIMenuItem.SearchProperties[UITestControl.PropertyNames.Name] = name;
            return UIMenuItem.Exists;

        }

       

        public void Select_NoToAll()
        {
            Thread.Sleep(500);
            WndImagePro image = new WndImagePro();

            WinWindow wnd = new WinWindow(image);

            wnd.SearchProperties.Add(new PropertyExpression(WinControl.PropertyNames.ControlName, "CloseAllForm", PropertyExpressionOperator.EqualTo));
            if (wnd.Exists)
            {
                WinButton client = new WinButton(wnd);
                client.SearchProperties.Add(new PropertyExpression(UITestControl.PropertyNames.Name, "No to All", PropertyExpressionOperator.EqualTo));

                client.Click();
            }

        }
        #endregion

        #region ChildWindows

        private WndRecordMacro _wndRecordMacroo;

        public WndRecordMacro WndRecordMacro
        {
            get
            {
                if (_wndRecordMacroo == null)
                {
                    _wndRecordMacroo = new WndRecordMacro();
                }

                return _wndRecordMacroo;
            }
        }

        private WndRecording _wndRecording;

        public WndRecording WndRecording
        {
            get
            {
                if (_wndRecording == null)
                {
                    _wndRecording = new WndRecording();
                }

                return _wndRecording;
            }
        }

        private WndMacrosVb _wndMacrosVb;

        public WndMacrosVb WndMacrosVb
        {
            get
            {
                if (_wndMacrosVb == null)
                {
                    _wndMacrosVb = new WndMacrosVb();
                }

                return _wndMacrosVb;
            }
        }

        private WndImageProOnlineHelp _wndImageProOnlineHelp;

        public WndImageProOnlineHelp WndImageProOnlineHelp
        {
            get
            {
                if (_wndImageProOnlineHelp == null)
                {
                    _wndImageProOnlineHelp = new WndImageProOnlineHelp();
                }

                return _wndImageProOnlineHelp;
            }
        }

        private WndConfirmSave _wndConfirmSave;

        public WndConfirmSave WndConfirmSave
        {
            get
            {
                if (_wndConfirmSave == null)
                {
                    _wndConfirmSave = new WndConfirmSave(this);
                }

                return _wndConfirmSave;
            }
        }

        #endregion

        #region ChildControls

        public WinGroup UiCaptureSimulationConGroup
        {
            get
            {
                    WinGroup mUiCaptureSimulationConGroup = new WinGroup(this);

                    #region Search Criteria

                    mUiCaptureSimulationConGroup.SearchProperties[UITestControl.PropertyNames.Name] = "DotNetBar Bar";
                    mUiCaptureSimulationConGroup.SearchProperties[WinControl.PropertyNames.ControlName] =
                        "ImageHistogram,ColocalizationPanel,SimulationPanel";
                    mUiCaptureSimulationConGroup.SearchProperties[UITestControl.PropertyNames.FriendlyName] =
                        "Capture Simulation Control";

                    #endregion

                return mUiCaptureSimulationConGroup;
            }
        }

        #endregion


        #region Setup and Clean Up mehtods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clickNoToAll"></param>
        public void CloseAllToolsAndViews(bool clickNoToAll = false)
        {
            ClickTabView();

            BtnCloseAllTools.Click();

            if (BtnCloseAllViews.Enabled)
            {
                BtnCloseAllViews.Click();
                if (clickNoToAll) Select_NoToAll();
            }
        }

       
        #endregion
    }
}
