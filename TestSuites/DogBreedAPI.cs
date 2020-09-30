using System;
using RestSharp;

using AbsaAutomation.src.main.Tools;
using AbsaAutomation.src.main.Core;
using System.Linq;
using AventStack.ExtentReports.MarkupUtils;
using System.Collections.Generic;
using AbsaAutomation.src.main;
using AbsaAutomation.src.main.Controller;
using System.Reflection;
using System.Net;
using NUnit.Framework;

namespace AbsaAutomation.src.main.API_Test
{
    [TestFixture]
    public class DogBreedAPI : AbsaBase
    {

        public TestContext TestContext { get; set; }
        public static string BaseUrl = "https://dog.ceo/api/";
  
        [AbsaTest, Test]
        public void All_Breed_API(AbsaBase test)
        {
            APIController APIControllerInst = new APIController(BaseUrl);
            var CurrentTest = Reporting.CreateTest();
            var response = APIControllerInst.GetRequest("breeds/list/all", CurrentTest);
            response.ValidateResponseCode(HttpStatusCode.OK, CurrentTest);
        }

        [AbsaTest, Test]
        public void Retriever_Breed_API(AbsaBase test)
        {
            APIController APIControllerInst = new APIController(BaseUrl);
            var CurrentTest = Reporting.CreateTest();
            var response = APIControllerInst.GetRequest("breeds/list/all", CurrentTest); 
            response.ValidateResponseCode(HttpStatusCode.OK, CurrentTest);
            response.ResponseContains("retriever", CurrentTest);
        }

        [AbsaTest, Test]
        public void Sub_Breed_API(AbsaBase test)
        {
            APIController APIControllerInst = new APIController(BaseUrl);
            var CurrentTest = Reporting.CreateTest();
            var response = APIControllerInst.GetRequest("breed/retriever/list", CurrentTest);
            response.ValidateResponseCode(HttpStatusCode.OK, CurrentTest);
        }

        [AbsaTest, Test]
        public void Random_Golden_Image_API(AbsaBase test)
        {
            APIController APIControllerInst = new APIController(BaseUrl);
            var CurrentTest = Reporting.CreateTest();
            var response = APIControllerInst.GetRequest("breed/retriever/golden/images/random", CurrentTest);
            response.ValidateResponseCode(HttpStatusCode.OK, CurrentTest);
        }
    }
}
