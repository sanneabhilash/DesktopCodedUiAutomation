using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject.BasePage
{
    [CodedUITest]
    public class TestBase
    {

        [ClassInitialize]
        public void ClassSetup()
        {
            /*Initial one setup actions to run before test cases in a class execute
            Eg: Read configuration files*/

    }

        [ClassCleanup]
        public void ClassTeardown()
        {
            /*One time clean up actions to run after all test cases in a class execute
            Eg: Closing the browser intance
            dispose resources*/
        }

        [TestInitialize]
        public void TestSetup()
        {
            /*Setup actions to run before every test case execution
            Eg: Login action
            load test case data
            Start timer*/
        }

        [TestCleanup]
        public void TestTeardown()
        {
            /*Clean up actions to run after every test case execution
            Eg: Logout action
            check if previous test execution failed and do required clean up
            Update test result*/
        }

        #region Business Resuables

        public static WinMenu GetContextMenu(string controlName = "", string windowTitle = "Image-Pro")
        {
            WinMenu menu = new WinMenu();
            menu.WindowTitles.Add(windowTitle);

            return (WinMenu)menu.FindMatchingControls()[0];
        }

        #endregion






    }
}
