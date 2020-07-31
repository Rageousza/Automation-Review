using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using AbsaAutomation.src.main.Tools;
using System.Linq;
using AbsaAutomation.src.main.Core;
using AbsaAutomation.src.tests;
using System.Reflection;

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]

namespace AbsaAutomation.src.main.TestSuites
{
    [TestClass]
    public class UITestSuite
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void CSVTest()
        {
            var currentTest = Base.Report.CreateTest(MethodBase.GetCurrentMethod().ToString());
            SeleniumMethods SeleniumInst = new SeleniumMethods("http://www.way2automation.com/angularjs-protractor/webtables/");

            UITesting uiTesting = new UITesting(SeleniumInst, currentTest);

            uiTesting.CSVTest("UserCSV.csv"); // Over here

            SeleniumInst.GetDriver.Close();
        }

        [TestMethod]
        public void JSONTest()
        {
            var currentTest = Base.Report.CreateTest(MethodBase.GetCurrentMethod().ToString());
            SeleniumMethods SeleniumInst = new SeleniumMethods("http://www.way2automation.com/angularjs-protractor/webtables/");
            
            UITesting uiTesting = new UITesting(SeleniumInst, currentTest);
            uiTesting.JSONTest("UserData.json");
            SeleniumInst.GetDriver.Close();
        }
    }
}
