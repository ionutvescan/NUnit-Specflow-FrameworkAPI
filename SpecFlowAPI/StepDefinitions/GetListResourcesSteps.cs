using Newtonsoft.Json;
using NUnit.Framework;
using SpecFlowAPI.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace SpecFlowAPI.StepDefinitions
{
    [Binding]
    public class GetListResourcesSteps
    {
        private static string ENDPOINT = "https://reqres.in/api/unknown";
        private readonly ISpecFlowOutputHelper specFlowOutputHelper;
        private HttpClient httpClient;
        private HttpResponseMessage httpResponse;
        private string response;
        private GetListResources getListResources;

        public GetListResourcesSteps (ISpecFlowOutputHelper specFlowOutput)
        {
            httpClient = new HttpClient ();
            this.specFlowOutputHelper = specFlowOutput;
        }

        [When(@"I make the API call to get list of resources")]
        public async Task WhenIMakeTheAPICallToGetListOfResources()
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage (HttpMethod.Get, ENDPOINT);
            httpResponse = await httpClient.SendAsync(httpRequestMessage);
            response = await httpResponse.Content.ReadAsStringAsync();
        }

        [Then(@"Status code is (.*)")]
        public void ThenStatusCodeIs(int code)
        {
            HttpStatusCode statusCode = httpResponse.StatusCode;
            specFlowOutputHelper.WriteLine("status code is: " + (int)statusCode);
            Assert.AreEqual(code, (int)statusCode);
        }

        [Then(@"The first page of resources is retrieve")]
        public void ThenTheFirstPageOfResourcesIsRetrieve()
        {
            getListResources = JsonConvert.DeserializeObject<GetListResources>(response);
            Assert.AreEqual(1, getListResources.page);
        }
    }
}
