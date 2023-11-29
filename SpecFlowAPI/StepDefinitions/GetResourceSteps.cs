using Newtonsoft.Json;
using NUnit.Framework;
using SpecFlowAPI.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace SpecFlowAPI.StepDefinitions
{
    [Binding]
    internal class GetResourceSteps
    {
        private static string ENDPOINT = "https://reqres.in/api/unknown/";
        private HttpClient httpClient;
        private HttpResponseMessage httpResponse;
        private readonly ISpecFlowOutputHelper specFlowOutputHelper;
        private string response;

        public GetResourceSteps(ISpecFlowOutputHelper specFlowOutputHelper)
        {
            httpClient = new HttpClient();
            this.specFlowOutputHelper = specFlowOutputHelper;
        }

        [When(@"I make the API call to retrieve a resource with ""([^""]*)""")]
        public async Task WhenIMakeTheAPICallToRetrieveAResourceWith(string id)
        {
            string ParamEndpoint = ENDPOINT + id;
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, ParamEndpoint);
            httpResponse = await httpClient.SendAsync(httpRequestMessage);
            response = await httpResponse.Content.ReadAsStringAsync();
        }

        [Then(@"The response status code is (.*)")]
        public void ThenTheResponseStatusCodeIs(int code)
        {
            HttpStatusCode statusCode = httpResponse.StatusCode;
            specFlowOutputHelper.WriteLine("status code is: " + (int)statusCode);
            Assert.AreEqual(code, (int)statusCode);
        }

        [Then(@"the resource with ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" is retrieved")]
        public void ThenTheResourceWithIsRetrieved(int id, string name, int year)
        {
            GetResource getResource = JsonConvert.DeserializeObject<GetResource>(response);
            Assert.AreEqual(id, getResource.data.id);
            Assert.AreEqual(name, getResource.data.name);
            Assert.AreEqual(year, getResource.data.year);
        }

    }
}
