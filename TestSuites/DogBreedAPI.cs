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

        public TestContext TestContext { get; set; }
        public static string BaseUrl = "https://dog.ceo/api/";
  
        [TestMethod]
        public void All_Breed_API()
        {
            APIController APIControllerInst = new APIController(BaseUrl);
            var CurrentTest = Base.Report.CreateTest(TestContext.TestName);
            var response = APIControllerInst.GetRequest("breeds/list/all", CurrentTest);
            response.ValidateResponseCode(HttpStatusCode.OK, CurrentTest);
        }

        [TestMethod]
        public void Retriever_Breed_API()
        {
            APIController APIControllerInst = new APIController(BaseUrl);
            var CurrentTest = Base.Report.CreateTest(TestContext.TestName);
            var response = APIControllerInst.GetRequest("breeds/list/all", CurrentTest); 
            response.ValidateResponseCode(HttpStatusCode.OK, CurrentTest);
            response.ResponseContains("retriever", CurrentTest);
        }

        [TestMethod]
        public void Sub_Breed_API()
        {
            APIController APIControllerInst = new APIController(BaseUrl);
            var CurrentTest = Base.Report.CreateTest(TestContext.TestName);
            var response = APIControllerInst.GetRequest("breed/retriever/list", CurrentTest);
            response.ValidateResponseCode(HttpStatusCode.OK, CurrentTest);
        }

        [TestMethod]
        public void Random_Golden_Image_API()
        {
            APIController APIControllerInst = new APIController(BaseUrl);
            var CurrentTest = Base.Report.CreateTest(TestContext.TestName);
            var response = APIControllerInst.GetRequest("breed/retriever/golden/images/random", CurrentTest);
            response.ValidateResponseCode(HttpStatusCode.OK, CurrentTest);
        }
    }
}
