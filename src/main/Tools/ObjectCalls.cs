using System;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AbsaAutomation.src.main.Tools
{
    public class ObjectCalls
    {
        static string url = "http://www.way2automation.com/angularjs-protractor/webtables/";
        public enum BrowserType { CHROME }
        public static IWebDriver Driver { get; set; }
        public BrowserType CurrentBrowser { get; set; }
        private static int ScreenshotCounter;

        public ObjectCalls()
        {
            Assert.IsNotNull(LDriver());
        }

        public IWebDriver LDriver()
        {

            Driver = new ChromeDriver(@"C:\AbsaAutomation");
            Driver.Manage().Window.Maximize();
            return Driver;
        }

        public static bool DriverClose()
        {
            Driver.Close();
            Driver.Quit();

            return true;
        }

        public static void Navigate()
        {
            
            Driver.Navigate().GoToUrl(url);
        }

        public static bool WaitForElement(By selector)
        {
            try 
            { 
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(1));
                wait.Until(element => element.FindElement(selector));
                return true;
            }
            catch(Exception e)
            {
                Reporting.LogError("Error - While locating element - " + e.StackTrace);
                return false;
            }
        }

        public static bool ClickElement(By Selector)
        {
            try
            {
                IWebElement element = Driver.FindElement(Selector);
                element.Click();
                return true;
            }
            catch(Exception e)
            {
                Reporting.LogError("Error - While interacting with element - " + e.StackTrace);
                return false;
            }
        }

        public static bool Pause()
        {
            Thread.Sleep(3000);
            return true;
        }

        public static bool EnterText(By Selector, string text)
        {
            try
            {
                IWebElement element = Driver.FindElement(Selector);
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

        public static string RetrieveText(By Selector)
        {
            IWebElement element = Driver.FindElement(Selector);
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

            ((ITakesScreenshot)Driver).GetScreenshot().SaveAsFile("" + builder + relativeBuilder);
            return "./" + relativeBuilder;
        }
    }
}
