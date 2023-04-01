using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Data.Models
{
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


    
    public class ErrorResponse
    {
        public DateTime TimeSpan { get; set; }
        public string Status { get; set; }
        public string Error { get; set; }
        public string Path { get; set; }
    }

    public class Description
    {
        public const string NotFound = "Not Found";
        public const string Exist = "Record Exist";
    }
    public class Code
    {
        public const string NotFound = "404";
        public const string Exist = "409";
    }
}