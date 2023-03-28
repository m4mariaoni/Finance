using Finance.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Service.Utility
{
    public static class RandomGenerator
    {
        //Function to Generate 4 Random Alphabet Character
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //Method to Generate a link
        public static Links LinkGenerator(string acctId, string url)
        {
            Links links = new Links()
            {
                Self = new Url()
                {
                    href = url + "/" + acctId.ToString()
                },
                Accounts = new Url()
                {
                    href = url
                }
            };

            return links;
        }


    }
}
