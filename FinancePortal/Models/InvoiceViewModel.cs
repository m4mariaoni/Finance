namespace FinancePortal.Models
{
    public class InvoiceViewModel
    {
       public long Id { get; set; }
        public string Reference { get; set; }
        public double Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string StudentId { get; set; }

    }

}
