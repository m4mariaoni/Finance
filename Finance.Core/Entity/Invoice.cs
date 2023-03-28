using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Data.Entity
{
    public class Invoice
    {
        public long Id { get; set; }
        [Required]
        [RegularExpression(@"[A-Z0-9]*",
        ErrorMessage = "Use the Regular Expression format [A-Z0-9]*")]
        public string Reference { get; set; }
        public double Amount { get; set; }
        public DateTime DueDate { get; set; }
        public Type Type { get; set; }
        public Status Status { get; set; }
        public long AccountId { get; set; }
        public virtual Account Account { get; set; }
    }

    public enum Status
    {
        OUTSTANDING,
        PAID,
        CANCELLED
    }

    public enum Type
    {
        LIBRARY_FINE,
        TUITION_FEES
    }

}