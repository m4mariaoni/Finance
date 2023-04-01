using AutoMapper;
using Finance.Data.Entity;
using Finance.Data.Models;
using Finance.Infrastructure.Interface;
using Finance.Service.Interface;
using Finance.Service.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Service.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IMapper _mapper;
        private IAppRepository _appRepository;
        private IAccountRepository _accountRepository;
        public InvoiceService(IAppRepository appRepository, IAccountRepository accountRepository, IMapper mapper)
        {
            _appRepository = appRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;

        }
        public async Task<InvoiceViewModel> CreateInvoice(InvoiceModel model, string url)
        {

            var studentExist = _accountRepository.Search(x => x.StudentId == model.Accounts.StudentId).FirstOrDefault();
            if (studentExist == null)
            {
                throw new Exception("You can't create an invoice without a valid student ID.t");
            }

            var charRef = RandomGenerator.RandomString(4);

            int num = new Random().Next(1000, 9999);
            string numRef = num.ToString();
            var referenceNo = charRef + numRef;

            while (referenceNo != null)
            {
                var searchRef = _appRepository.Invoices.Search(x => x.Reference == referenceNo).FirstOrDefault();
                if (searchRef != null)
                {
                    charRef = RandomGenerator.RandomString(4);
                    num = new Random().Next(1000, 9999);
                    numRef = num.ToString();
                    referenceNo = charRef + numRef;
                }
                break;
            }


            Invoice invoice = new Invoice()
            {
                Amount = model.Amount,
                DueDate = model.DueDate,
                Type = model.Type,
                Status = Status.OUTSTANDING,
                AccountId = studentExist.Id,
                Reference = charRef + numRef
            };
            await _appRepository.Invoices.Add(invoice);
            _appRepository.Save();

            Links links = RandomGenerator.LinkGenerator(invoice.Reference, url);

            InvoiceViewModel invoiceViewModel = new InvoiceViewModel()
            {
                Id = invoice.Id,
                Reference = invoice.Reference,
                Amount = invoice.Amount,
                DueDate = invoice.DueDate,
                Type = ((Finance.Data.Entity.Type)invoice.Type).ToString(), //nameof(invoice.Type),
                Status = ((Status)invoice.Status).ToString(),
                StudentId = model.Accounts.StudentId,
                Links = links
            };

            return invoiceViewModel;
        }

        public async Task<InvoiceViewModel> DeleteInvoice(long id, string url)
        {
            var invoiceExist = _appRepository.Invoices.Search(x => x.Id == id).FirstOrDefault();
            if (invoiceExist == null)
            {

            }
            invoiceExist.Status = Status.CANCELLED;
            _appRepository.Invoices.Update(invoiceExist);
            _appRepository.Save();
            Links links = RandomGenerator.LinkGenerator(id.ToString(), url);
            var student = _appRepository.Accounts.Search(x => x.Id == invoiceExist.AccountId).Select(y => y.StudentId).FirstOrDefault();

            var result = _mapper.Map<InvoiceViewModel>(invoiceExist);
            result.StudentId = student;
            result.Links = links;
            return result;

        }

        public async Task<SaveResponse> GetAllInvoice(string url)
        {
            List<InvoiceViewModel> viewModel = new List<InvoiceViewModel>();
            var invoices = await _appRepository.Invoices.GetAll();

            if (invoices.Any())
            {
                foreach (var item in invoices)
                {
                    InvoiceViewModel model = new InvoiceViewModel();
                    var student = _appRepository.Accounts.Search(x => x.Id == item.AccountId).Select(y => y.StudentId).FirstOrDefault();

                    Links links = RandomGenerator.LinkGenerator(item.Reference, url);
                    model = new InvoiceViewModel()
                    {
                        Id = item.Id,
                        Amount = item.Amount,
                        DueDate = item.DueDate,
                        Status = ((Status)item.Status).ToString(),
                        StudentId = student,
                        Type = ((Finance.Data.Entity.Type)item.Type).ToString(),
                        Links = links
                    };
                    viewModel.Add(model);
                }
                return RespMessage.Response(viewModel.ToList());
            }

            return null;
        }

        public async Task<InvoiceViewModel> GetInvoiceById(long id, string url)
        {
            var invoiceExist = _appRepository.Invoices.Search(x => x.Id == id).FirstOrDefault();
            var map = _mapper.Map<InvoiceViewModel>(invoiceExist);
            Links links = RandomGenerator.LinkGenerator(invoiceExist.Reference, url);
            map.Links = links;
            return map;
        }

        public async Task<InvoiceViewModel> GetInvoiceByReferenceId(string reference, string url)
        {
           var invRef = _appRepository.Invoices.Search(x => x.Reference == reference).FirstOrDefault();
            var studentid = _appRepository.Accounts.Search(x => x.Id == invRef.AccountId).Select(y => y.StudentId).FirstOrDefault();
            var result =  _mapper.Map<InvoiceViewModel>(invRef);
            result.StudentId = studentid;
            Links links = RandomGenerator.LinkGenerator(reference, url);
            result.Links = links;
            return result;
        }

        public async Task<InvoiceViewModel> PayInvoice(long id, string url)
        {
            try
            {
                var invoiceExist = _appRepository.Invoices.Search(x => x.Id == id).FirstOrDefault();
                if (invoiceExist == null)
                {              
                   throw new Exception("Invoice does not exist");
                }

                invoiceExist.Status = Status.PAID;
                _appRepository.Invoices.Update(invoiceExist);
                _appRepository.Save();

                Links links = RandomGenerator.LinkGenerator(invoiceExist.Reference, url);
                var student = _appRepository.Accounts.Search(x => x.Id == invoiceExist.AccountId).Select(y => y.StudentId).FirstOrDefault();

                InvoiceViewModel view = new InvoiceViewModel()
                {
                    Id = invoiceExist.Id,
                    Amount = invoiceExist.Amount,
                    DueDate = invoiceExist.DueDate,
                    Reference = invoiceExist.Reference,
                    StudentId = student,
                    Status = ((Status)invoiceExist.Status).ToString(),
                    Type = ((Finance.Data.Entity.Type)invoiceExist.Type).ToString(),
                    Links = links

                };

                return view;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}