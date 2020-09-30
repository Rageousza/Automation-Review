using System;
using System.Text;
using System.Threading;
using AbsaAutomation.src.main.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using AbsaAutomation.src.main.Tools;
using System.Runtime.Serialization;
using System.Xml;
using AventStack.ExtentReports;

namespace AbsaAutomaition.src.main.Tools
{
    public class SeleniumDriver : ReportHandler, IDisposable
    {
        public IWebDriver CreateDriver(string URL)
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var driver = new ChromeDriver();
            _driver = driver;
            _driver.Navigate().GoToUrl(URL);
            _driver.Manage().Window.Maximize();
            return _driver;
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

        public void Dispose()
        {
            if (_driver != null)
            {
                _driver.Dispose();
            }
        }

        public bool DriverRunning()
        {
            return (_driver != null);
        }
    }
}

