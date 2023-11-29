using Dynamitey;
using Newtonsoft.Json;
using NUnit.Framework;
using SpecFlowAPI.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow.Infrastructure;

namespace SpecFlowAPI.StepDefinitions
{
    [Binding]
    internal class GetListUsersSteps
    {
        private static string ENDPOINT = "https://reqres.in/api/users?page=";
        private HttpClient httpClient;
        private HttpResponseMessage httpResponseMessage;
        private string response;
        GetListUsers getListUsers;
        private readonly ISpecFlowOutputHelper specFlowOutputHelper;

        public GetListUsersSteps(ISpecFlowOutputHelper specFlowOutputHelper)
        {
            httpClient = new HttpClient();
            this.specFlowOutputHelper = specFlowOutputHelper;
        }

        [When(@"I make the API call to get the user page number ""([^""]*)""")]
        public async Task WhenIMakeTheAPICallToGetTheUserPageNumber(string page)
        {
            string paramEndpoint = ENDPOINT + page;

            httpResponseMessage = await httpClient.GetAsync(paramEndpoint);
            response = await httpResponseMessage.Content.ReadAsStringAsync();
        }

        [Then(@"The request should be successful with status code (.*)")]
        public void ThenTheRequestShouldBeSuccessfulWithStatusCode(int code)
        {
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            specFlowOutputHelper.WriteLine("status code is: " + (int)statusCode);
            Assert.AreEqual(code, (int)statusCode);
        }

        [Then(@"The user page number ""([^""]*)"" is retrieved")]
        public void ThenTheUserPageNumberIsRetrieved(int page)
        {
            getListUsers = JsonConvert.DeserializeObject<GetListUsers>(response);
            Assert.AreEqual(page, getListUsers.page);
        } 

        [Then(@"There are (.*) users per page")]
        public void ThenThereAreUsersPerPage(int users)
        {
            Assert.AreEqual(users, getListUsers.per_page);
            httpClient.Dispose();
        }

    }
}

