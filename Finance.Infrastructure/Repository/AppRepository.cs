using Finance.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Repository
{
    public class AppRepository : IAppRepository
    {
        private readonly DbContextClass _dbContext;
        public IAccountRepository Accounts { get; }
        public IInvoiceRepository Invoices { get; }

        public AppRepository(DbContextClass dbContext, IAccountRepository accountRepository,
            IInvoiceRepository invoiceRepository)
        {
            _dbContext = dbContext;
            Accounts = accountRepository;
            Invoices = invoiceRepository;
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }


    }
}
