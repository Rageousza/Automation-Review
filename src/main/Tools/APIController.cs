using System;
using System.Collections.Generic;
using System.Text;
using AbsaAutomation.src.main.Core;
using AbsaAutomation.src.Models;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using OpenQA.Selenium.Remote;
using RestSharp;

namespace AbsaAutomation.src.main.Controller
{
    public class APIController
    {
        public string url;

        public APIController(string BaseURL)
        {
            url = BaseURL;
        }

        //add comstructor
        //add base url as property

        public APIModel GetRequest(string APIEndPoint, ExtentTest CurrentTest)
        {
            Dictionary<string, string> Headers = new Dictionary<string, string>();
            Headers.Add("Cookie", "__cfduid=de02f8e5eac0368cd2f92235381eb69621593899937");

            //Creates Http Client
            var client = new RestClient(url + APIEndPoint);

            client.Timeout = -1;

            var request = new RestRequest(Method.GET);

            if (Headers != null)
            {
                foreach (KeyValuePair<string, string> header in Headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            var TimeStarted = DateTime.Now;
            IRestResponse response = client.Execute(request);
            var TimeFinished = DateTime.Now;

            Base.Report.StepPassed("Status Description: " + response.StatusCode, CurrentTest);
            Base.Report.StepInfoAPI(response.Content, CodeLanguage.Xml, CurrentTest);

            //Serializtion
            APIModel Model = new APIModel()
            {
                Response = response,
                EndPoint = url + APIEndPoint,
                Duration = TimeFinished - TimeStarted,
                StatusCode = response.StatusCode,
            };

            return Model;
        }

    }
}
