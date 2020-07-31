using System;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbsaAutomation.src.main.Tools;
using AbsaAutomation.src.main.Core;
using System.Linq;
using AventStack.ExtentReports.MarkupUtils;
using System.Collections.Generic;
using AbsaAutomation.src.main;
using AbsaAutomation.src.main.Controller;
using System.Reflection;
using System.Net;

namespace AbsaAutomation.src.main.API_Test
{
    [TestClass]
    public class DogBreedAPI
    {
        public static string BaseUrl = "https://dog.ceo/api/";
  
        [TestMethod]
        public void AllBreedAPI()
        {
            APIController APIControllerInst = new APIController(BaseUrl);
            var CurrentTest = Base.Report.CreateTest(MethodBase.GetCurrentMethod().ToString());
            var response = APIControllerInst.GetRequest("breeds/list/all", CurrentTest);
            response.ValidateResponseCode(HttpStatusCode.OK, CurrentTest);
        }

        [TestMethod]
        public void RetrieverBreedAPI()
        {
            APIController APIControllerInst = new APIController(BaseUrl);
            var CurrentTest = Base.Report.CreateTest(MethodBase.GetCurrentMethod().ToString());
            var response = APIControllerInst.GetRequest("breeds/list/all", CurrentTest); 
            response.ValidateResponseCode(HttpStatusCode.OK, CurrentTest);
            response.ResponseContains("retriever", CurrentTest);
        }

        [TestMethod]
        public void SubBreedAPI()
        {
            APIController APIControllerInst = new APIController(BaseUrl);
            var CurrentTest = Base.Report.CreateTest(MethodBase.GetCurrentMethod().ToString());
            var response = APIControllerInst.GetRequest("breed/retriever/list", CurrentTest);
            response.ValidateResponseCode(HttpStatusCode.OK, CurrentTest);
        }

        [TestMethod]
        public void RandomGoldenImageAPI()
        {
            APIController APIControllerInst = new APIController(BaseUrl);
            var CurrentTest = Base.Report.CreateTest(MethodBase.GetCurrentMethod().ToString());
            var response = APIControllerInst.GetRequest("breed/retriever/golden/images/random", CurrentTest);
            response.ValidateResponseCode(HttpStatusCode.OK, CurrentTest);
        }
    }
}
