using AbsaAutomation.src.main.Core;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbsaAutomation.src.main.Tools
{
    public abstract class ReportHandler
    {
        protected IWebDriver _driver { get; set; }
        protected ExtentTest _curTest { get; set; }

        public void StepPassScreenshot(string message)
        {
            Reporting.StepPassedWithScreenshot(message, TakeScreenshot());
        }

        public void TestFailed(string message)
        {
            Reporting.TestFailed(message, TakeScreenshot());
        }

        public Screenshot TakeScreenshot()
        {
            Screenshot ss = ((ITakesScreenshot) _driver).GetScreenshot();
            return ss;
        }
    }
}
