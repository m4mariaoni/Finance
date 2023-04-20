using Finance.Data.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FinanceAPIIntegration.Test
{
    [TestClass]
    public class APITests
    {
        private HttpClient httpClient;


        public APITests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            httpClient = webAppFactory.CreateDefaultClient();
        }
        [TestMethod]
        public async Task AccountGet_StudentByStudentId()
        {
            var response = await httpClient.GetAsync("http://localhost:5179/api/Account/student/C0914010906");
            var stringResult = response.Content.ReadAsStringAsync().Result;          
            var model = JsonConvert.DeserializeObject<AccountViewModel>(stringResult);
            Assert.AreEqual("C0914010906", model.StudentId);
            Assert.AreEqual(10007, model.Id);
        }

        [TestMethod]
        public async Task InvoiceGet_ReturnsListOfInvoice()
        {
            var response = await httpClient.GetAsync("http://localhost:5179/api/Invoice");
            var result = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(!string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public async Task InvoiceGet_CheckReferenceStatus()
        {
            var response = await httpClient.GetAsync("http://localhost:5179/api/Invoice/gN9R6990");
            var result = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


    }
}