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
using System.Xml.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace SpecFlowAPI.StepDefinitions
{
    [Binding]
    internal class Create_Update_DeleteUserSteps
    {

        private static readonly string ENDPOINT = "https://reqres.in/api/users";
        private HttpClient httpClient;
        private CreateUserRequest createUserRequest;
        private HttpResponseMessage httpResponse;
        private string response;
        private readonly ISpecFlowOutputHelper specFlowOutputHelper;
        private CreateUserResponse createUserResponse;
        private CreateUserResponse updateUserResponse;
        private string userId;

        public Create_Update_DeleteUserSteps(ISpecFlowOutputHelper specFlowOutputHelpe)
        {
            this.specFlowOutputHelper = specFlowOutputHelpe;
        }

        [Given(@"I populate the API call with ""([^""]*)"" and ""([^""]*)""")]
        public void GivenIPopulateTheAPICallWithAnd(string name, string job)
        {
            createUserRequest = new CreateUserRequest { Name = name, Job = job };
        }

        [When(@"I make the API call to create a new user")]
        public async Task WhenIMakeTheAPICallToCreateANewUser()
        {
            httpClient = new HttpClient();

            var serialized = JsonConvert.SerializeObject(createUserRequest);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

            httpResponse = await httpClient.PostAsync(ENDPOINT, stringContent);
            response = await httpResponse.Content.ReadAsStringAsync();

        }

        [Then(@"the call is successful with status (.*)")]
        public void ThenTheCallIsSuccessfulWithStatus(int code)
        {
            HttpStatusCode statusCode = httpResponse.StatusCode;
            specFlowOutputHelper.WriteLine("status code is: " + (int)statusCode);
            Assert.AreEqual(code, (int)statusCode);
        }

        [Then(@"the user profile with ""([^""]*)"" and ""([^""]*)"" is created")]
        public async Task ThenTheUserProfileWithAndIsCreated(string name, string job)
        {
            createUserResponse = JsonConvert.DeserializeObject<CreateUserResponse>(response);
            Assert.AreEqual(name, createUserResponse.name);
            Assert.AreEqual(job, createUserResponse.job);
        }

        [Given(@"The user is created with ""([^""]*)"" and ""([^""]*)""")]
        public async Task GivenTheUserIsCreatedWithAnd(string name, string job)
        {
            GivenIPopulateTheAPICallWithAnd(name, job);
            await WhenIMakeTheAPICallToCreateANewUser();
            await ThenTheUserProfileWithAndIsCreated(name, job);
            userId = createUserResponse.id;
        }

        [When(@"I make the API call to update ""([^""]*)"" user's job with ""([^""]*)""")]
        public async Task WhenIMakeTheAPICallToUpdateUsersJobWith(string name, string newJob)
        {
            CreateUserRequest updateUserRequest = new CreateUserRequest { Name = name, Job = newJob };

            string ParamEndpoint = ENDPOINT + "/" + userId;

            var serialized = JsonConvert.SerializeObject(updateUserRequest);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

            httpResponse = await httpClient.PatchAsync(ParamEndpoint, stringContent);
            response = await httpResponse.Content.ReadAsStringAsync();
        }

        [Then(@"the ""([^""]*)"" user's job is update with ""([^""]*)""")]
        public void ThenTheUsersJobIsUpdateWith(string name, string newJob)
        {
            updateUserResponse = JsonConvert.DeserializeObject<CreateUserResponse>(response);
            Assert.AreEqual(name, updateUserResponse.name);
            Assert.AreEqual(newJob, updateUserResponse.job);
        }

        [When(@"I make the API call to delete user")]
        public async Task WhenIMakeTheAPICallToDeleteUser()
        {
            string ParamEndpoint = ENDPOINT + "/" + userId;
            httpResponse = await httpClient.DeleteAsync(ParamEndpoint);
        }

    }
}