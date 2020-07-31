using System;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using AbsaAutomation.src.main.Core;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Model;
using OpenQA.Selenium;
using System.Text;

namespace AbsaAutomation.src.main.Tools
{
    public class Reporting
    {
        private IWebDriver _driver;
        public Reporting()
        {
            Setup();
        }

        private static int ScreenshotCounter;

        private AventStack.ExtentReports.ExtentReports _report;

        public string ReportName { get; set; }
        
        public string ReportDescription { get; set; }
        public string TestName { get; set; }
        public string _reportDir;
        public string ReportDirectory { get => _reportDir; set => _reportDir = value; }
        public DateTime starttime { get; set; }
        public DateTime endTime { get; set; }

        private void Setup()
        {
            ReportDirectory = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\TestResults\" + ReportName + @"\" + GetDateTime() + @"\";
            System.IO.Directory.CreateDirectory(ReportDirectory);
            var htmlReport = new ExtentHtmlReporter(ReportDirectory);
            _report = new AventStack.ExtentReports.ExtentReports();
            _report.AttachReporter(htmlReport);

            _report.Flush();
        }

        private string GetDateTime()
        {
            return DateTime.Now.ToString("hh-mm-ss dd-MM-yyyy");
        }

        public ExtentTest CreateTest(string TestName)
        {
            Base.Report.starttime = DateTime.Now;
            return _report.CreateTest(TestName);

        }

        public void Warning(string message, ExtentTest curTest)
        {
            try
            {
                curTest.Warning(message);
                _report.Flush();
           
            }
            catch
            {
                Console.WriteLine("Failed to log step passed - " + message);
            }
        }

        public string StepPassed(string message, ExtentTest curTest)
        {
            try
            {
                curTest.Pass(message);
                _report.Flush();
                return null;
            }
            catch
            {
  
                return "Failed to log step passed - " + message;
            }
        }

        public void TestFailed(string message, IWebDriver driver, ExtentTest curTest)
        {
            try
            {
                curTest.Fail(message + "<br>", MediaEntityBuilder.CreateScreenCaptureFromPath(STakeScreenshot(false)).Build());
                _report.Flush();
               
            }
            catch 
            {
  
            }
        }
        public void TestFailed(string message, ExtentTest curTest)
        {
            try
            {
                curTest.Fail(message);
                _report.Flush();
            }
            catch
            {

            }
        }

        public string StepPassedWithScreenshot(string message, IWebDriver driver, ExtentTest curTest)
        {
            try
            {
                curTest.Pass(message + "<br>",
                     MediaEntityBuilder.CreateScreenCaptureFromPath(
                STakeScreenshot(true)).Build());
                _report.Flush();
                return null;
            }
            catch (Exception exc)
            {
               //Prints the message without the screenshot on the report
                Warning(message, curTest);

                //Returns the error message
                throw exc;
            }
        }

        public string TakeScreenshot(bool status)
        {
            string screenshotpath = null;
            if (_driver != null)
            {
                screenshotpath = (screenshotpath == null) ? STakeScreenshot(status) : screenshotpath;
            }

            return screenshotpath;
        }

        public void StepInfoAPI(string message, CodeLanguage codeLanguageFormat, ExtentTest curTest)
        {
            try
            {
                var json = MarkupHelper.CreateCodeBlock(message, codeLanguageFormat);
                curTest.Log(Status.Info, json);
            }
            catch (Exception e)
            {
                curTest.Log(Status.Fail, e.Message);
                throw e;
            }
            finally
            {
                _report.Flush();
            }
        }

        public string STakeScreenshot(bool status)
        {
            ScreenshotCounter++;
            StringBuilder builder = new StringBuilder();
            StringBuilder relativeBuilder = new StringBuilder();
            builder.Append(ReportDirectory);
            relativeBuilder.Append("Screenshots\\");
            System.IO.Directory.CreateDirectory("" + builder + relativeBuilder);
            relativeBuilder.Append(ScreenshotCounter + "_" + ((status) ? @"Passed" : @"Failed"));
            relativeBuilder.Append(".png");

            ((ITakesScreenshot)_driver).GetScreenshot().SaveAsFile("" + builder + relativeBuilder);
            return "./" + relativeBuilder;
        }


    }
}
