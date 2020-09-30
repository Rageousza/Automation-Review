using System;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using AbsaAutomation.src.main.Core;
using AventStack.ExtentReports.MarkupUtils;
using OpenQA.Selenium;
using System.Text;
using System.IO;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AbsaAutomation.src.main.Tools
{
    public class Reporting
    {
        private static int ScreenshotCounter;

        private static AventStack.ExtentReports.ExtentReports _report;
        private static List<ExtentTest> extentTest { get; set; } = new List<ExtentTest>();
        public static string ReportName { get; set; }
        public static string ReportDescription { get; set; }
        public static string TestName { get; set; }
        public static string _reportDir;
        public static string ReportDirectory { get => _reportDir; set => _reportDir = value; }
        public static DateTime starttime { get; set; }
        public static DateTime endTime { get; set; }

        public static void Setup()
        {
            if (_report == null)
            {
                ReportDirectory = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\TestResults\" + ReportName + @"\" + GetDateTime() + @"\";
                System.IO.Directory.CreateDirectory(ReportDirectory);
                var htmlReport = new ExtentHtmlReporter(ReportDirectory);
                _report = new AventStack.ExtentReports.ExtentReports();
                _report.AttachReporter(htmlReport);
            }

            if (extentTest == null) extentTest = new List<ExtentTest>();
        }

        private static string GetDateTime()
        {
            return DateTime.Now.ToString("hh-mm-ss dd-MM-yyyy");
        }

        public static ExtentTest CreateTest()
        {
            string TestName = TestContext.CurrentContext.Test.MethodName;
            ExtentTest Test = _report.CreateTest(TestName);
            extentTest.Add(Test);
            return Test;

        }

        public static void Warning(string message, ExtentTest curTest)
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

        public static string StepPassed(string message, ExtentTest curTest)
        {
            ExtentTest test = GetTest(GetCurrentTestName());
            try
            {
                test.Pass(message);
                _report.Flush();
                return null;
            }
            catch
            {
  
                return "Failed to log step passed - " + message;
            }
        }

        public static void TestFailed(string message, Screenshot screenshot = null)
        {
            ScreenshotCounter++;
            string file = _reportDir + @"Screenshots\";
            Directory.CreateDirectory(file);
            screenshot.SaveAsFile(file + ScreenshotCounter + ".png");
            ExtentTest test = GetTest(GetCurrentTestName());
            message = message.Replace("<", "&lt");
            message = message.Replace(">", "&lt");
            try
            {
                test.Fail(message + "<br>", MediaEntityBuilder.CreateScreenCaptureFromPath(file).Build());
                _report.Flush();
               
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void TestFailed(string message, ExtentTest curTest)
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

        public static void StepPassedWithScreenshot(string message, Screenshot screenshot = null)
        {
            ScreenshotCounter++;
            string screenshotfolder = @"Screenshots\";
            string ScreenshotName = ScreenshotCounter + ".png";
            //string file = _reportDir + @"Screenshots\";
            Directory.CreateDirectory(_reportDir + screenshotfolder);
            screenshot.SaveAsFile(_reportDir + screenshotfolder + ScreenshotName);
            var t = ".\\" + screenshotfolder + ScreenshotName;

            //ScreenshotCounter++;
            //string file = _report + @"Screenshots\";
            //string AbsolutePath = file;
            //Directory.CreateDirectory(file);
            //file = file + ScreenshotCounter + ".png";
            //AbsolutePath += ScreenshotCounter + ".png";
            //file = @"Screenshots\" + ScreenshotCounter + ".png";
            //screenshot.SaveAsFile(AbsolutePath);
            ExtentTest test = GetTest(GetCurrentTestName());

            try
            {
                test.Pass(message + "<br>",
                     MediaEntityBuilder.CreateScreenCaptureFromPath(t).Build());
                _report.Flush();
   
            }
            catch (Exception exc)
            {
               //Prints the message without the screenshot on the report
                Warning("Failed to capture Screenshot", test);

                //Returns the error message
                throw exc;
            }
        }

        //public string TakeScreenshot(bool status)
        //{
        //    string screenshotpath = null;
        //    if (_driver != null)
        //    {
        //        screenshotpath = (screenshotpath == null) ? STakeScreenshot(status) : screenshotpath;
        //    }

        //    return screenshotpath;
        //}

        public static void StepInfoAPI(string message, CodeLanguage codeLanguageFormat, ExtentTest curTest)
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

        public static ExtentTest GetTest(string testname)
        {
            Setup();
            if(extentTest.Count < 1)
            {
                return CreateTest();
            }

            var test = extentTest.Where(x => x.Model.Name == testname).FirstOrDefault();
            return (test != null) ? test : CreateTest();
        }

        private static string GetCurrentTestName()
        {
            return TestContext.CurrentContext.Test.MethodName;
        }

        public static void TestPassed()
        {
            ExtentTest test = GetTest(GetCurrentTestName());
            test.Pass("Test Comeplete");
            _report.Flush();
        }

        //public string STakeScreenshot(bool status)
        //{
        //    ScreenshotCounter++;
        //    StringBuilder builder = new StringBuilder();
        //    StringBuilder relativeBuilder = new StringBuilder();
        //    builder.Append(ReportDirectory);
        //    relativeBuilder.Append("Screenshots\\");
        //    System.IO.Directory.CreateDirectory("" + builder + relativeBuilder);
        //    relativeBuilder.Append(ScreenshotCounter + "_" + ((status) ? @"Passed" : @"Failed"));
        //    relativeBuilder.Append(".png");

        //    ((ITakesScreenshot)_driver).GetScreenshot().SaveAsFile("" + builder + relativeBuilder);
        //    return "./" + relativeBuilder;
        //}

    }
}
