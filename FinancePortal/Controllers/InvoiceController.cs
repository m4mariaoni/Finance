using FinancePortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace FinancePortal.Controllers
{
    public class InvoiceController : Controller
    {

        private readonly IConfiguration _configuration;

        public InvoiceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string FinanceAPIUrl
        {
            get { return _configuration.GetSection("FinanceAPIUrl").Value; }
        }

        [HttpGet]
        public IActionResult FindInvoice()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FindInvoice(string reference)
        {
            var viewModel = new InvoiceViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(FinanceAPIUrl);
                client.DefaultRequestHeaders.Clear();
               client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Resp = await client.GetAsync($"api/Invoice/{reference}");
                if (Resp.IsSuccessStatusCode)
                {
                    var answer = Resp.Content.ReadAsStringAsync().Result;
                    viewModel = JsonConvert.DeserializeObject<InvoiceViewModel>(answer);

                }
                return View("GetInvoice",viewModel);
            }
        }

        //Post API to handle Invoice Payment Request
        [HttpPost]
        public async Task<IActionResult> PayInvoice([FromForm] long Id)
        {
            var viewModel = new InvoiceViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(FinanceAPIUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Resp = await client.PutAsJsonAsync($"api/Invoice/{Id}/pay", Id);
                if (Resp.IsSuccessStatusCode)
                {
                    var answer = Resp.Content.ReadAsStringAsync().Result;
                    viewModel = JsonConvert.DeserializeObject<InvoiceViewModel>(answer);                                  
                }
                return View("GetInvoice", viewModel);
            }
        }
    }
}
