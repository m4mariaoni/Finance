using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Data.Entity
{
    public class Account
    {
        public long Id { get; set; }
        public string StudentId { get; set; }
        public virtual List<Invoice> Invoices { get; set; }
        public bool HasOutstandingBalance { get; set; }
    }
}
