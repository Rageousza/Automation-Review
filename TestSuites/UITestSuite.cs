using System;
using OpenQA.Selenium;
using AbsaAutomation.src.main.Tools;
using System.Linq;
using AbsaAutomation.src.main.Core;
using AbsaAutomation.src.tests;
using AbsaAutomation.src.main.Pages;
using NUnit.Framework;

namespace AbsaAutomation.src.main.TestSuites
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class UITestSuite : AbsaBase
    {
        public TestContext TestContext { get; set; }
        public ProtractorApplication protractorApplication { get; set; }

        [AbsaTest, Test]
        public void CSVTest(AbsaBase test)
        {
            IWebDriver driver =  test.Selenium.CreateDriver("http://www.way2automation.com/angularjs-protractor/webtables/");
            protractorApplication = new ProtractorApplication(driver);
            protractorApplication.userPage.CSVTest("UserCSV.csv");
        }

        [AbsaTest, Test]
        public void JSONTest(AbsaBase test)
        {
            IWebDriver driver = test.Selenium.CreateDriver("http://www.way2automation.com/angularjs-protractor/webtables/");
            protractorApplication = new ProtractorApplication(driver);
            protractorApplication.userPage.JSONTest("UserData.json");
        }
    }
}
