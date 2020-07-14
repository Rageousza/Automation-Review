using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using AbsaAutomation.src.main.Tools;
using System.Linq;
using AbsaAutomation.src.main.Core;
using AbsaAutomation.src.tests;

namespace AbsaAutomation.src.main.TestSuites
{
    [TestClass]
    public class UITestSuite : Base
    {

        [ClassInitialize]
        public static void Initialize(TestContext TestContext)
        {
            string[] s = TestContext.FullyQualifiedTestClassName.Split('.');
            var suiteName = s[s.Length - 1];
            Reporting.ReportName = suiteName;
        }

        [TestInitialize]
        public void BeforeEach()
        {
            Reporting.starttime = DateTime.Now;
            Reporting.TestName = TestContext.TestName;

            Reporting.CreateTest();
            Reporting.WriteToLogFile("[START] - Test Started - " + Reporting.TestName);
            ObjectCallsInstance = new ObjectCalls("http://www.way2automation.com/angularjs-protractor/webtables/");
        }

        [TestCleanup]
        public void AfterEach()
        {
            ObjectCallsInstance.DriverClose();
        }

        [TestMethod]
        public void UITest()
        {
            Assert.IsNull(UITesting.Test1());
        }

        [TestMethod]
        public void UITest2()
        {
            Assert.IsNull(UITesting.Test2());
        }
    }
}
