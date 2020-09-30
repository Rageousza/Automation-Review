using AbsaAutomation.src.main.Core;
using AbsaAutomation.src.main.Tools;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AbsaAutomation.src.Models
{
    public class APIModel
    {
        public IRestResponse Response { get; set; }
        public string EndPoint { get; set; }
        public TimeSpan Duration { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public void ValidateResponseCode(HttpStatusCode Status, ExtentTest CurrentTest)
        {
            if (StatusCode != Status)
            {
                Reporting.TestFailed("Failed due to Status not being " + Status, CurrentTest);
                Assert.Equals(StatusCode, Status);
            }

            Reporting.StepInfoAPI("Response Success: "+ Status, CodeLanguage.Xml, CurrentTest);

        }

        public void ResponseContains(string ExpectedText, ExtentTest CurrentTest)
        {
            var breed = Response.Content.Contains(ExpectedText);

            if(breed == false)
            {
                Reporting.TestFailed("Reponse did not contain ", CurrentTest);
                Assert.Fail("Reponse did not contain " + ExpectedText);
            }

            Reporting.StepPassed("Found Breed: " + ExpectedText, CurrentTest);
            
            
        }
    }
}
