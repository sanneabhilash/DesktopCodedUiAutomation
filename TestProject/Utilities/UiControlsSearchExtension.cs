using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject.Utilities
{
    /// <summary>
    /// Class containing extension methods to find UI controls
    /// </summary>
    public static class UiControlsSearchExtension
    {
        #region WinControls
        #region WinEdit

        /// <summary>
        /// Get UI Textbox or WinEdit control using Control's 'Name' or 'ClassName' or 'ControlName' search property
        /// </summary>
        /// <param name="window">Parent window of the control</param>
        /// <param name="name">Name of Control</param>
        /// <param name="className">Class Name of control</param>
        /// <param name="controlName">ContronName of the control</param>
        /// <returns></returns>
        public static WinEdit GetWinEdit(this WinWindow window, string name ="", string className = "", string controlName ="")
        {
            var txtProps = new WinEdit(window) { TechnologyName = "MSAA" };
            if (name != "") txtProps.SearchProperties[WinEdit.PropertyNames.Name] = name;
            if (className != "") txtProps.SearchProperties[WinEdit.PropertyNames.ClassName] = className;
            if (controlName != "") txtProps.SearchProperties[WinEdit.PropertyNames.ControlName] = controlName;

            return txtProps;
        }


        #endregion

        #region WinButton
        /// <summary>
        /// Get UI Button or WinButton control using Control's search property
        /// </summary>
        /// <param name="window">Parent window of the control</param>
        /// <param name="buttonName">Display Name of Control</param>
        /// <param name="className">Class Name of control</param>
        /// <param name="controlName">ContronName of the control</param>
        /// <returns>WinButton</returns>
        public static WinButton GetButtonByName(this WinWindow window, string buttonName, string className = "", string controlName = "")
        {
            WinButton btn = new WinButton(window);
            btn.TechnologyName = "MSAA";
            btn.SearchProperties[UITestControl.PropertyNames.Name] = buttonName;
            if (className != "")
            {
                btn.SearchProperties[WinButton.PropertyNames.ClassName] = className;
            }

            if (controlName != "")
            {
                btn.SearchProperties[WinButton.PropertyNames.ControlName] = controlName;
            }

            return btn;
        }

        /// <summary>
        /// Get all buttons in UI with the given button 'Name' property
        /// </summary>
        /// <param name="window">Parent window of the control</param>
        /// <param name="buttonName">Display Name of Control</param>
        /// <returns>List of WinButtons</returns>
        public static List<WinButton> GetButtonsByName(this WinWindow window, string buttonName)
        {
            WinButton btn = new WinButton(window);
            btn.TechnologyName = "MSAA";
            btn.SearchProperties[WinButton.PropertyNames.Name] = buttonName;
            var buttons = btn.FindMatchingControls();
            List<WinButton> buttonArray = new List<WinButton>();
            foreach (var button in buttons)
            {
                if (button.Name.Contains(buttonName))
                {
                    buttonArray.Add((WinButton)button);
                }
            }
            return buttonArray;
        }
        #endregion

        #region WinSpltButton
        /// <summary>
        /// Get UI WinSpltButton control using Control's search property
        /// </summary>
        /// <param name="window">Parent window of the control</param>
        /// <param name="name">Display Name of Control</param>
        /// <returns>WinSplitButton</returns>
        public static WinSplitButton GetWinSplitButton(this WinWindow window, string name)
        {
            var txtProps = new WinSplitButton(window) { TechnologyName = "MSAA" };
            txtProps.SearchProperties[WinSplitButton.PropertyNames.Name] = name;

            return txtProps;
        }

        #endregion

        #region WinCustom
        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static WinCustom GetWinCustomByName(this WinWindow window, string name)
        {
            var txtProps = new WinCustom(window) { TechnologyName = "MSAA" };
            txtProps.SearchProperties[WinCustom.PropertyNames.Name] = name;
            return txtProps;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static WinCustom GetCustomControlByName(this WinWindow window, string controlName)
        {
            WinCustom custom = new WinCustom(window);
            custom.SearchProperties[WinCustom.PropertyNames.ControlName] = controlName;
            return custom;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="optionName"></param>
        /// <returns></returns>
        public static WinCustom GetStatusOption(this WinStatusBar status, string optionName)
        {
            Console.WriteLine("Get status bar option: " + optionName);
            //Test Case PremCore-3312: 50: Cursor Position Option
            string searchText = "";
            switch (optionName.ToLower())
            {
                case "cursor position":
                    searchText = "X,Y: ";
                    break;
                case "pixel value":
                    searchText = "RGB: ";
                    break;

            }
            var tesst = new WinCustom(status);
            tesst.SearchProperties.Add(new PropertyExpression(WinCustom.PropertyNames.ControlName, "McStatusBar", PropertyExpressionOperator.Contains));
            tesst.SearchProperties.Add(new PropertyExpression(WinCustom.PropertyNames.Name, searchText, PropertyExpressionOperator.Contains));
            return tesst;
        }

        #endregion

        #region WinPane
        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static WinPane GetPaneByName(this WinWindow window, string controlName)
        {
            WinPane pane = new WinPane(window);
            pane.SearchProperties[WinPane.PropertyNames.Name] = controlName;
            return pane;
        }

        #endregion

        #region WinGroup
        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static WinGroup GetWinGroupByName(this WinWindow window, string name)
        {
            var txtProps = new WinGroup(window) { TechnologyName = "MSAA" };
            txtProps.SearchProperties[WinGroup.PropertyNames.Name] = name;
            return txtProps;
        }
        #endregion

        #region WinClient
        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static WinClient GetClientByControlName(this WinWindow window, string controlName)
        {
            WinClient btn = new WinClient(window);
            btn.TechnologyName = "MSAA";
            btn.SearchProperties[WinClient.PropertyNames.ControlName] = controlName;
            return btn;
        }

        #endregion

        #region WinText (Label Controls)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static WinText GetLabelByControlName(this WinWindow window, string controlName)
        {
            WinText btn = new WinText(window);
            btn.SearchProperties[WinText.PropertyNames.ControlName] = controlName;
            return btn;
        }

        #endregion

        #region WinMenu and WinMenuItem (Context menu)
        /// <summary>
        /// Check and Click given menu item
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="menuName"></param>
        public static void SelectMenuItem(this WinMenu menu, string menuName)
        {
            //var items = menu.GetChildren();
            //foreach (WinMenuItem item in items)
            //{
            //    if (item.DisplayText == menuName)
            //    {
            //        item.Click();
            //        break;
            //    }
            //}

            menu.SelectMenuItem(menuName, true);
        }
        /// <summary>
        /// Check and Click given menu item
        /// </summary>
        /// <param name="menu">Parent menu</param>
        /// <param name="menuName">Name Menu item to click</param>
        /// <param name="select">True to select, False to unselect menu item</param>
        public static void SelectMenuItem(this WinMenu menu, string menuName, bool select)
        {
            Console.WriteLine("Select menu item: " + menuName);
            if (menu.CheckMenuItemExits(menuName))
            {
                var items = menu.GetChildren();
                foreach (var uiTestControl in items)
                {
                    var item = (WinMenuItem) uiTestControl;

                    if (item.DisplayText == menuName)
                    {
                        if (select)
                        {
                            if (!item.Checked)
                            {
                                item.Click();
                            }
                        }
                        else if (item.Checked)
                            item.Click();

                        break;
                    }
                }
            }
            else Assert.Fail("Menu item: '"+ menuName+"' does not exit");
        }

        /// <summary>
        /// Check if the menu item exists or not
        /// </summary>
        /// <param name="menu">Parent Menu (Context menu) contorl</param>
        /// <param name="menuName">Display name of menu item</param>
        /// <returns>True if Menu item exists else False</returns>
        public static bool CheckMenuItemExits(this WinMenu menu, string menuName)
        {
            bool exists = false;
            var items = menu.GetChildren();
            foreach (var uiTestControl in items)
            {
                var item = (WinMenuItem)uiTestControl;
                if (item.DisplayText == menuName)
                {
                    exists = true;
                    break;
                }
            }

            return exists;
        }
        #endregion

        #region Combobox (Dropdown)
        /// <summary>
        /// Get the UI DropDown/WinComboBox control
        /// </summary>
        /// <param name="window">Parent Window</param>
        /// <param name="controlName"/>Control Name property value/param>
        /// <returns>WinComboBox</returns>
        public static WinComboBox GetComboboxByControlName(this WinWindow window, string controlName)
        {
            var btn = new WinComboBox(window);
            btn.SearchProperties[WinComboBox.PropertyNames.ControlName] = controlName;
            return btn;
        }

        #endregion
        #endregion

        #region WpfControls
        #region WpfCheckbox 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static WpfCheckBox GetWpfCheckBoxByName(this WinWindow window, string name)
        {
            WpfCheckBox btn = new WpfCheckBox(window);
            btn.SearchProperties[WpfCheckBox.PropertyNames.Name] = name;
            return btn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chk"></param>
        /// <param name="select"></param>
        public static void SelectCheckBox(this WpfCheckBox chk, bool select = true)
        {
            if (select)
            {
                if (!chk.Checked)
                    Mouse.Click();
            }
            else
            {
                if (chk.Checked)
                    Mouse.Click();
            }
        }
        #endregion

        #region WpfEdit 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="automationId"></param>
        /// <returns></returns>
        public static WpfEdit GetEditByAutomationId(this WinWindow window, string automationId)
        {
            WpfEdit btn = new WpfEdit(window);
            btn.SearchProperties[WpfEdit.PropertyNames.AutomationId] = automationId;
            return btn;
        }
        #endregion
        #endregion
    }
}
