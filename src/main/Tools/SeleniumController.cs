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
using System.Runtime.Serialization;
using System.Xml;
using AventStack.ExtentReports;

namespace AbsaAutomation.src.main.Tools
{
    public abstract class SeleniumController : ReportHandler
    {
        private Random random = new Random();

        public SeleniumController(IWebDriver driver)
        {
            _driver = driver;
        }

        public static IWebDriver CreateDriver(string URL)
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(URL);
            driver.Manage().Window.Maximize();
            return driver;
        }

        public void Navigate(string url)
        {
            _driver.Url = url;
        }

        public IWebDriver GetDriver => _driver;

        public static bool DriverClose(IWebDriver driver)
        {
            if (driver != null)
            {
                driver.Close();
                driver.Quit();
            }

            return true;
        }


        public bool WaitForElement(By selector, int sec = 5)
        {
            try 
            { 
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(sec));
                wait.Until(element => element.FindElement(selector));
                wait.Until(element => element.FindElement(selector).Displayed);
                wait.Until(element => element.FindElement(selector).Enabled);
                return true;
            }
            catch(Exception e)
            {
                TestFailed("Failed to locate element - " + selector);
                throw e;
                //difference between throw new and just throw
                
            }
        }

        public bool ClickElement(By selector)
        {
            try
            {
                _driver.FindElement(LocateElement(selector)).Click();
                return true;
            }
            catch(Exception e)
            {
                TestFailed("Failed to interact with element - " + selector);
                throw e;
            }
        }

        public bool Pause()
        {
            Thread.Sleep(3000);
            return true;
        }

        public bool EnterText(By selector, string text, bool clear = false)
        {
            try
            {
                if (clear)
                {
                    _driver.FindElement(selector).Clear();
                }

                _driver.FindElement(LocateElement(selector)).SendKeys(text); ;
      
                return true;
            }
            catch(Exception e)
            {
                TestFailed("Failed to enter text - " + selector);
                throw e;
            }
        }

        public By LocateElement(By selector)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                wait.Until(drv => drv.FindElement(selector));
                wait.Until(drv => drv.FindElement(selector).Displayed);
                wait.Until(drv => drv.FindElement(selector).Enabled);
                return selector;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public int GenerateRandomNumber(int num)
        {
            //Generates a random number to ensure Fname, Lname and User is unique
            int i = random.Next(num);
            return i;
        }

    }
}
