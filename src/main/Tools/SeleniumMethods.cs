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
    public class SeleniumMethods
    {
        private Random random = new Random();
        private IWebDriver _driver;


        public SeleniumMethods(string URL)
        {
            _driver = CreateDriver(URL);
        }

        public IWebDriver CreateDriver(string URL)
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(URL);
            _driver.Manage().Window.Maximize();
            return _driver;
        }

        public IWebDriver GetDriver => _driver;

        public bool DriverClose()
        {
            if (_driver != null)
            {
                _driver.Close();
                _driver.Quit();
            }

            return true;
        }


        public bool WaitForElement(By selector, int sec, ExtentTest CurrentTest)
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
                Base.Report.TestFailed("Failed to locate element - " + selector, _driver, CurrentTest);
                throw e;
                //difference between throw new and just throw
                
            }
        }

        public bool ClickElement(By selector, ExtentTest CurrentTest)
        {
            try
            {
                _driver.FindElement(LocateElement(selector)).Click();
                return true;
            }
            catch(Exception e)
            {
                Base.Report.TestFailed("Failed to interact with element - " + selector, _driver, CurrentTest);
                throw e;
            }
        }

        public bool Pause()
        {
            Thread.Sleep(3000);
            return true;
        }

        public bool EnterText(By selector, string text, ExtentTest CurrentTest, bool clear = false )
        {
            try
            {
                if (clear)
                    _driver.FindElement(selector).Clear();

                _driver.FindElement(LocateElement(selector)).SendKeys(text); ;
      
                return true;
            }
            catch(Exception e)
            {
                Base.Report.TestFailed("Failed to enter text - " + selector, _driver, CurrentTest);
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
