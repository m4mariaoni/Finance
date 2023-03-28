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
        [StringLength(10, MinimumLength = 10)]
        [Required(ErrorMessage = @"This field is required with 10 AlphaNumeric Characters")]
        [RegularExpression(@"^[A-Z][0-9]*$", ErrorMessage = "This field requires this format:  C123456789")]
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
