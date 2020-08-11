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
            Base.Report.StepPassedWithScreenshot(message, _curTest, TakeScreenshot());
        }

        public void TestFailed(string message)
        {
            Base.Report.TestFailed(message, _driver, _curTest);
        }

        public Screenshot TakeScreenshot()
        {
            Screenshot ss = ((ITakesScreenshot)_driver).GetScreenshot();
            return ss;
        }
    }
}
