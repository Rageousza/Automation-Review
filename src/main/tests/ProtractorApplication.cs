using AbsaAutomation.src.tests;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbsaAutomation.src.main.Pages
{
    public class ProtractorApplication
    {
        public UserPage userPage { get; set; }
        public ProtractorApplication(IWebDriver driver, ExtentTest Test)
        {
            userPage = new UserPage(driver, Test);
        }

    }
}
