using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Interface
{
    public interface IAppRepository : IDisposable
    {
        IAccountRepository Accounts { get; }

        int Save();

        IInvoiceRepository Invoices { get; }
    }
}