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


    public class SaveResponse
    {
        public object Account { get; set; }

    }

    public static class RespMessage
    {
        public static SaveResponse Response(object data = null)
        {
            var response = new SaveResponse
            {
                Account = data,
            };
            return response;
        }
    }
}
