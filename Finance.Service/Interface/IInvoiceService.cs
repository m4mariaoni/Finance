﻿using Finance.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Service.Interface
{
    public interface IInvoiceService
    {
        Task<InvoiceViewModel> CreateInvoice(InvoiceModel model, string url);
        Task<SaveResponse> GetAllInvoice(string url);
        Task<InvoiceViewModel> GetInvoiceById(long id, string url);

        Task<InvoiceViewModel> PayInvoice(long id, string url);
        Task<InvoiceViewModel> GetInvoiceByReferenceId(string reference, string url);

        Task<InvoiceViewModel> DeleteInvoice(long id, string url);
    }
}
