using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject.TestScripts
{
    [CodedUITest]
    public class TestAttributesDemoTests
    {
        public TestContext TestContext { get; set; }
        static int count = 1;
        #region Setup And Cleanup

        [ClassInitialize(), Description("Use ClassInitialize to run code before running the first test in the class")]
        public static void MyClassInitialize(TestContext testContext)
        {
        }

        [ClassCleanup(), Description("Use ClassCleanup to run code after all tests in a class have run")]
        public static void MyClassCleanup()
        {
            StringAssert.Equals("expected", "ExPected".ToLower());// "Strings should be equal"
        }

        [TestInitialize(), Description("Use TestInitialize to run code before running each test")]
        public void MyTestInitialize()
        {
            Console.WriteLine("Begin Test Execution: " + TestContext.TestName);
        }

        [TestCleanup(), Description("Use TestCleanup to run code after each test has run")]
        public void MyTestCleanup()
        {
            Console.WriteLine("End Test Execution: " + TestContext.TestName);
        }

        #endregion

        #region CategoryTests
        [TestMethod, TestCategory("GroupA")]
        public void categoryGroupA_1()
        {
            Console.WriteLine(TestContext.TestName);
        }
        [TestMethod, TestCategory("GroupB")]
        public void categoryGroupA_2()
        {

        }
        [TestMethod, TestCategory("GroupC")]
        public void categoryGroupA_3()
        {

        }
        #endregion

        #region PriorityTests
        [TestMethod, Priority(1)]
        public void Priority_z()
        {
            Console.WriteLine("Counter value is: " + (++count));
        }
        [TestMethod, Priority(1)]
        public void Priority_y()
        {
            Console.WriteLine("Counter value is: " + (++count));

        }
        [TestMethod, Priority(1)]
        public void Priority_x()
        {
            Console.WriteLine("Counter value is: " + (++count));
        }
        #endregion

        #region CustomeAttributeTests
        [TestMethod, TestProperty("Author", "Abhilash")]
        public void Author_A()
        {

        }
        [TestMethod, TestProperty("Author", "Bhanu")]
        public void Author_B()
        {

        }
        #endregion
    }
}
