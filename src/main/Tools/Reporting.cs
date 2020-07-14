using System;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using AbsaAutomation.src.main.Core;


namespace AbsaAutomation.src.main.Tools
{
    public class Reporting : Base
    {
        public static AventStack.ExtentReports.ExtentReports report;
        public static ExtentTest curTest;

        private static bool CheckTest()
        {
            return curTest == null || curTest.Model.Name != TestName;
        }

        private static bool CheckReport()
        {
            return report == null;
        }

        private static void Setup()
        {
            ReportDirectory = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\TestResults\" + ReportName + @"\" + GetDateTime() + @"\";
            System.IO.Directory.CreateDirectory(ReportDirectory);
            var htmlReport = new ExtentHtmlReporter(ReportDirectory);
            report = new AventStack.ExtentReports.ExtentReports();
            report.AttachReporter(htmlReport);
            CreateTest();
            report.Flush();
        }

        private static string GetDateTime()
        {
            return DateTime.Now.ToString("hh-mm-ss dd-MM-yyyy");
        }

        public static void CreateTest()
        {
            if (CheckReport())
            {
                Setup();
            }
            if (CheckTest())
            {
                curTest = report.CreateTest(TestName, ReportDescription);
            }
        }

        public static void LogSuccess(string message)
        {
            WriteToLogFile("\t \t[SUCCESS] - " + message);
        }

        public static void WriteToLogFile(string logInfo)
        {
            using (System.IO.StreamWriter file = 
                new System.IO.StreamWriter(LogFile, true))
            {
                file.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss ") + " - " + logInfo);
                Console.WriteLine(logInfo);
            }
        }

        public static void Warning(string message)
        {
            try
            {
                if (CheckTest()) CreateTest();
                curTest.Warning(message);
                report.Flush();
                LogWarning(message);
            }
            catch
            {
                Console.WriteLine("Failed to log step passed - " + message);
            }
        }

        public static void LogError (string error)
        {
            WriteToLogFile("\t[ERROR] - " + error);
        }

        public static void LogWarning (string warning)
        {
            WriteToLogFile("[WARNING] - " + warning);
        }

        public static string StepPassed(string message)
        {
            try
            {
                if (CheckTest()) CreateTest();
                curTest.Pass(message);
                report.Flush();
                LogSuccess(message);
                return null;
            }
            catch
            {
                LogError("Failed to log step passed - " + message);
                return "Failed to log step passed - " + message;
            }
        }

        public static string StepPassedWithScreenshot(string message)
        {
            try
            {
                if (CheckTest()) CreateTest();

                curTest.Pass(message + "<br>",
                    MediaEntityBuilder.CreateScreenCaptureFromPath(
                        TakeScreenshot(true)).Build());
                report.Flush();
                LogSuccess(message);
                return null;
            }
            catch (Exception exc)
            {
                //Prints out error
                Reporting.LogError("[REPORT ERROR] - Failed to log a step passed with a screenshot onto the report." +
                "\n Message - " + message + "." +
                " \n Stack trace - " + exc.StackTrace);

                //Prints the message without the screenshot on the report
                Warning(message);

                //Returns the error message
                return message;
            }
        }

        public static void FinaliseTest()
        {
            try
            {
                if (CheckTest()) CreateTest();

                curTest.Pass("Test Complete", MediaEntityBuilder.CreateScreenCaptureFromPath(TakeScreenshot(true)).Build());
                report.Flush();
                LogSuccess("[TEST COMPLETED]");
            }
            catch
            {
                Reporting.LogWarning("[EXCEPTION] - Failed to log Test Complete");
            }
        }

        public static string TakeScreenshot(bool status)
        {
            string screenshotpath = null;
            if (ObjectCallsInstance.driver != null)
            {
                screenshotpath = (screenshotpath == null) ? ObjectCallsInstance.TakeScreenshot(status) : screenshotpath;
            }

            return screenshotpath;
        }


    }
}
