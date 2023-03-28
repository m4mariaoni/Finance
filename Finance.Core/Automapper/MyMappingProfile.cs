using AutoMapper;
using Finance.Data.Entity;
using Finance.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Data.Automapper
{
    public class MyMappingProfile : Profile
    {
        public MyMappingProfile()
        {
            CreateMap<Account, AccountViewModel>().ReverseMap();
            CreateMap<Invoice, InvoiceViewModel>().ReverseMap();
        }
    }
}
