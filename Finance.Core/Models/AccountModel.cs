using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Data.Models
{
    public class AccountModel
    {
        public string StudentId { get; set; }
    }

    public class AccountViewModel
    {
        public long Id { get; set; }
        public string StudentId { get; set; }
        public bool HasOutstandingBalance { get; set; }
        public Links Links { get; set; }
    }

    public class Links
    {
        public Url Self { get; set; }
        public Url Accounts { get; set; }
    }

    public class Url
    {
        public string href { get; set; }
    }
}
