﻿using Finance.Data.Models;
using Finance.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : Controller
    {
        public readonly IInvoiceService _invoiceService;
        Uri address;
        string url;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        /// <summary>
        /// Add a new Invoice to Finance
        /// </summary>
        /// <param name="Invoice"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateInvoice(InvoiceModel model)
        {
            if (model == null)
            {
                return BadRequest(ModelState);
            }
            address = new Uri(Request.Host.ToString());
            url = address.ToString() + "/invoice";

            var result = await _invoiceService.CreateInvoice(model, url);
            return Ok(result);

        }

        /// <summary>
        /// Pay Invoice to Finance
        /// </summary>
        [HttpPut("{id}/pay")]
        public async Task<IActionResult> PayInvoice(long id)
        {
            if (id <= 0)
            {
                return BadRequest("Invoice does not exist");
            }
            address = new Uri(Request.Host.ToString());
            url = address.ToString() + "/invoice";

            var result = await _invoiceService.PayInvoice(id, url);
            return Ok(result);

        }

        /// <summary>
        /// Check for invoice reference number
        /// </summary>
        [HttpGet("{reference}")]
        public async Task<IActionResult> GetInvoicebyReference(string reference)
        {
            if (reference == null)
            {
                return BadRequest("Enter a valid Reference");
            }
            url = getLinks();
            var result = await _invoiceService.GetInvoiceByReferenceId(reference, url);
           return Ok(result);
        }

        /// <summary>
        /// Return all invoice
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllInvoice()
        {
            url = getLinks();
            var result = await _invoiceService.GetAllInvoice(url);
            return Ok(result);
        }

        /// <summary>
        /// Delete invoice by ID
        /// </summary>
        [HttpDelete("/{id}/cancel")]
        public async Task<IActionResult> CancelInvoice(long id)
        {
            url = getLinks();
            var result = await _invoiceService.DeleteInvoice(id, url);
            return Ok(result);
        }

        private string getLinks()
        {
            address = new Uri(Request.Host.ToString());
            return url = address.ToString() + "/invoice";
        }
    }
}