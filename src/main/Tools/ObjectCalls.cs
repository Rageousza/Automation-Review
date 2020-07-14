using System;
using System.Text;
using System.Threading;
using AbsaAutomation.src.main.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using AbsaAutomation.src.main.Tools;


namespace AbsaAutomation.src.main.Tools
{
    public class ObjectCalls : Base
    {
        public IWebDriver driver;

        private static int ScreenshotCounter;

        public ObjectCalls(string url)
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
        }

        public IWebDriver GetDriver => driver;

        public bool DriverClose()
        {
            driver.Close();
            driver.Quit();

            return true;
        }


        public bool WaitForElement(By selector)
        {
            try 
            { 
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
                wait.Until(element => element.FindElement(selector));
                return true;
            }
            catch(Exception e)
            {
                Reporting.LogError("Error - While locating element - " + e.StackTrace);
                return false;
            }
        }

        public bool ClickElement(By Selector)
        {
            try
            {
                IWebElement element = driver.FindElement(Selector);
                element.Click();
                return true;
            }
            catch(Exception e)
            {
                Reporting.LogError("Error - While interacting with element - " + e.StackTrace);
                return false;
            }
        }

        public bool Pause()
        {
            Thread.Sleep(3000);
            return true;
        }

        public bool EnterText(By Selector, string text)
        {
            try
            {
                IWebElement element = driver.FindElement(Selector);
                element.Clear();
                element.SendKeys(text);
                return true;
            }
            catch(Exception e)
            {
                Reporting.LogError("Error - While entering text - " + e.StackTrace);
                return false;
            }
        }

        public string RetrieveText(By Selector)
        {
            IWebElement element = driver.FindElement(Selector);
            return element.Text;
        }

        public string TakeScreenshot(bool status)
        {
            ScreenshotCounter++;
            StringBuilder builder = new StringBuilder();
            StringBuilder relativeBuilder = new StringBuilder();
            builder.Append(Reporting.ReportDirectory);
            relativeBuilder.Append("Screenshots\\");
            System.IO.Directory.CreateDirectory("" + builder + relativeBuilder);
            relativeBuilder.Append(ScreenshotCounter + "_" + ((status) ? @"Passed" : @"Failed"));
            relativeBuilder.Append(".png");

            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("" + builder + relativeBuilder);
            return "./" + relativeBuilder;
        }
    }
}
