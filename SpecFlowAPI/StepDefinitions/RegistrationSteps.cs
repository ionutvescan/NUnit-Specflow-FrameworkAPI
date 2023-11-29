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
    internal class RegistrationSteps
    {
        private static readonly string ENDPOINT = "https://reqres.in/api/register";
        private RegistrationRequest registrationRequest;
        private HttpResponseMessage httpResponse;
        private string response;
        private readonly ISpecFlowOutputHelper specFlowOutputHelper;

        public RegistrationSteps(ISpecFlowOutputHelper specFlowOutputHelpe)
        {
            this.specFlowOutputHelper = specFlowOutputHelpe;
        }

        [Given(@"I populate the API call with email address and password")]
        public void GivenIPopulateTheAPICallWithEmailAddressAndPassword()
        {
            registrationRequest = new RegistrationRequest { Email = "eve.holt@reqres.in", Password = "pistol" };
        }

        [When(@"I make the API call to register")]
        public async Task WhenIMakeTheAPICallToRegister()
        {
            var httpClient = new HttpClient();

            var serialized = JsonConvert.SerializeObject(registrationRequest);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

            httpResponse = await httpClient.PostAsync(ENDPOINT, stringContent);
            response = await httpResponse.Content.ReadAsStringAsync();
        }

        [Then(@"the API call is successful with status (.*)")]
        public void ThenTheAPICallIsSuccessfulWithStatus(int code)
        {
            HttpStatusCode statusCode = httpResponse.StatusCode;
            specFlowOutputHelper.WriteLine("status code is: " + (int)statusCode);
            Assert.AreEqual(code, (int)statusCode);
        }

        [Given(@"I populate the API call with email address")]
        public void GivenIPopulateTheAPICallWithEmailAddress()
        {
            registrationRequest = new RegistrationRequest { Email = "sydney@fife" };
        }


        [Then(@"the API call is unsuccessful with status code (.*)")]
        public void ThenTheCallIsUnsuccessfulWithStatusCode(int code)
        {
            HttpStatusCode statusCode = httpResponse.StatusCode;
            specFlowOutputHelper.WriteLine("status code is: " + (int)statusCode);
            Assert.AreEqual(code, (int)statusCode);
        }


        [Then(@"the error is ""([^""]*)""")]
        public void ThenTheErrorIs(string message)
        {

            RegistrationResponse registration = JsonConvert.DeserializeObject<RegistrationResponse>(response);
            Assert.AreEqual(message, registration.Error);
        }

    }
}
