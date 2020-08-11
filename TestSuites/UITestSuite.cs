using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using AbsaAutomation.src.main.Tools;
using System.Linq;
using AbsaAutomation.src.main.Core;
using AbsaAutomation.src.tests;
using AbsaAutomation.src.main.Pages;

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]

namespace AbsaAutomation.src.main.TestSuites
{
    [TestClass]
    public class UITestSuite 
    {
        public TestContext TestContext { get; set; }
        public ProtractorApplication protractorApplication { get; set; }

        [TestMethod]
        public void CSVTest()
        {
            var currentTest = Base.Report.CreateTest(TestContext.TestName);
            IWebDriver driver =  SeleniumController.CreateDriver("http://www.way2automation.com/angularjs-protractor/webtables/");
            protractorApplication = new ProtractorApplication(driver, currentTest);

            protractorApplication.userPage.CSVTest("UserCSV.csv");

            SeleniumController.DriverClose(driver);
        }

        [TestMethod]
        public void JSONTest()
        {
            var currentTest = Base.Report.CreateTest(TestContext.TestName);
            IWebDriver driver = SeleniumController.CreateDriver("http://www.way2automation.com/angularjs-protractor/webtables/");
            protractorApplication = new ProtractorApplication(driver, currentTest);

            protractorApplication.userPage.JSONTest("UserData.json");
            
            SeleniumController.DriverClose(driver);
        }
    }
}
