using AbsaAutomaition.src.main.Tools;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbsaAutomation.src.main.Tools
{
    public class AbsaBase : IABSA, IDisposable
    {
        public SeleniumDriver Selenium;

        public AbsaBase()
        {
            Selenium = new SeleniumDriver();
        }

        [SetUp]
        public void StartUp()
        {
            Reporting.Setup();
            Reporting.CreateTest();
        }

        [TearDown]
        public void CleanUp()
        {
            try
            {
                var arguments = TestContext.CurrentContext.Test.Arguments;
                ((AbsaBase)arguments[0]).Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Dispose()
        {
            if (!string.IsNullOrEmpty(TestContext.CurrentContext.Test.MethodName))
            {
                if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
                {
                    Screenshot();
                }
                else
                {
                    Reporting.TestPassed();
                }

                Selenium.Dispose();
            }
        }

        public void Screenshot(string message = null)
        {
            if (message == null) message = TestContext.CurrentContext.Result.Message;
            bool reported = false;
            if (this.Selenium.DriverRunning())
            {
                this.Selenium.TestFailed(message);
                reported = true;
            }
            if (!reported)
            {
                Selenium.TestFailed(message);
            }
        }
    }

    public interface IABSA
    {

    }
}
