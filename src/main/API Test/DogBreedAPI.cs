using System;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbsaAutomation.src.main.Tools;
using AbsaAutomation.src.main.Core;
using System.Linq;

namespace AbsaAutomation.src.main.API_Test
{
    [TestClass]
    public class DogBreedAPI : Base
    {
        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void Initialize(TestContext TestContext)
        {
            string[] s = TestContext.FullyQualifiedTestClassName.Split('.');
            var suiteName = s[s.Length - 1];
            Reporting.ReportName = suiteName;
        }

        [TestInitialize]
        public void BeforeEach()
        {
            Reporting.starttime = DateTime.Now;
            Reporting.TestName = TestContext.TestName;
            //ReportDescription = TestContext.Properties.Where(x => x.Key == "Description").First().Value.ToString();

            Reporting.CreateTest();
            Reporting.WriteToLogFile("[START] - Test Started - " + Reporting.TestName);

        }

        [TestMethod]
        public void AllBreedAPI()
        {
            var client = new RestClient("https://dog.ceo/api/breeds/list/all");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "__cfduid=de02f8e5eac0368cd2f92235381eb69621593899937");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            var statusDesc = response.StatusDescription;
            Assert.AreEqual("OK", statusDesc.ToString());
            Reporting.StepPassed("Status Description: " + response.StatusDescription);
            Reporting.StepPassed("API Content - " + response.Content);

        }

        [TestMethod]
        public void RetrieverBreedAPI()
        {
            var client = new RestClient("https://dog.ceo/api/breeds/list/all");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "__cfduid=de02f8e5eac0368cd2f92235381eb69621593899937");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            var breed = response.Content.Contains("retriever");
            Assert.IsTrue(breed);
            Reporting.StepPassed("Status Description: " + response.StatusDescription);
            Reporting.StepPassed("API Content - " + response.Content);

        }

        [TestMethod]
        public void SubBreedAPI()
        {
            var client = new RestClient("https://dog.ceo/api/breed/retriever/list");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "__cfduid=de02f8e5eac0368cd2f92235381eb69621593899937");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            var StatusDescription = response.StatusDescription;
            Assert.AreEqual("OK", StatusDescription.ToString());
            Reporting.StepPassed("Status Description: " + response.StatusDescription);
            Reporting.StepPassed("API Content - " + response.Content);

        }

        [TestMethod]
        public void RandomGoldenImageAPI()
        {
            var client = new RestClient("https://dog.ceo/api/breed/retriever/golden/images/random");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "__cfduid=de02f8e5eac0368cd2f92235381eb69621593899937");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            var StatusDescription = response.StatusDescription;
            Assert.AreEqual("OK", StatusDescription.ToString());
            Reporting.StepPassed("Status Description: " + response.StatusDescription);
            Reporting.StepPassed("API Content - " + response.Content);
        }
    }
}
