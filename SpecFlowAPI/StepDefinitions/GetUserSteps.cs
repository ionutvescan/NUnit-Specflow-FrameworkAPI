using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using SpecFlowAPI.Support;
using Newtonsoft.Json;

namespace SpecFlowAPI.StepDefinitions
{
    [Binding]
    internal class GetUserSteps
    {
        private static string ENDPOINT = "https://reqres.in/api/users/";
        private HttpClient httpClient;
        private HttpResponseMessage httpResponseMessage;
        private string response;
        GetUser getUser;
        private readonly ISpecFlowOutputHelper specFlowOutputHelper;

        public GetUserSteps(ISpecFlowOutputHelper specFlowOutputHelper)
        {
            httpClient = new HttpClient();
            this.specFlowOutputHelper = specFlowOutputHelper;
        }

        [When(@"I make the Api call to get a user ""([^""]*)""")]
        public async Task WhenIMakeTheApiCallToGetAUser(string userId)
        {
            string paramEndpoint = ENDPOINT + userId;

            httpResponseMessage = await httpClient.GetAsync(paramEndpoint);
            response = await httpResponseMessage.Content.ReadAsStringAsync();
        }

        [Then(@"The response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int code)
        {
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            specFlowOutputHelper.WriteLine("status code is: " + (int)statusCode);
            Assert.AreEqual(code, (int)statusCode);
        }

        [Then(@"The user id ""([^""]*)"" is retrieved")]
        public void ThenTheUserIdIsRetrieved(int userId)
        {
            getUser = JsonConvert.DeserializeObject<GetUser>(response);
            Assert.AreEqual(userId, getUser.data.id);
            httpClient.Dispose();
        }
    }
}
