using Finance.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FinanceAPIFunctionalTest
{
    public class AccountControllerTest : BaseControllerTests
    {
        public AccountControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetAccount_ReturnsAllRecords()
        {
            var client = this.GetNewClient();
            var response = await client.GetAsync("http://localhost:5179/api/Account");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<AccountViewModel>>(stringResponse).ToList();
            var statusCode = response.StatusCode.ToString();

            Assert.AreEqual("OK", statusCode);
            Assert.IsTrue(result.Count >  2);

            
            
        }
    }
}
