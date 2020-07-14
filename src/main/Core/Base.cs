using AbsaAutomation.src.main.Tools;
using System;
using AbsaAutomation.src.main.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbsaAutomation.src.main.Core
{
    public class Base
    {
        public static ObjectCalls ObjectCallsInstance;

        public static ObjectCalls DriverInstance;
        public static string ReportName { get; set; }
        public TestContext TestContext { get; set; }
        public static string ReportDescription { get; set; }
        public static string TestName { get; set; }
        public static string _reportDir;
        public static string ReportDirectory { get => _reportDir; set => _reportDir = value; }
        public static DateTime starttime { get; set; }
        public static DateTime endTime { get; set; }
        public static string LogFile { get => _reportDir + @"\Log.txt"; }
  
    }
}
