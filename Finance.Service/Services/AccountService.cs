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
    public class AccountService : IAccountService
    {
        public IAppRepository _appRepository;
        public AccountService(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public async Task<AccountViewModel> CreateAccount(AccountModel model, string url)
        {
            try
            {
                Account account = new Account();
                account.StudentId = model.StudentId;
                account.HasOutstandingBalance = false;


                await _appRepository.Accounts.Add(account);
                _appRepository.Save();

                Links links = RandomGenerator.LinkGenerator(account.StudentId, url);
                AccountViewModel viewModel = new AccountViewModel()
                {
                    Id = account.Id,
                    StudentId = account.StudentId,
                    HasOutstandingBalance = account.HasOutstandingBalance,
                    Links = links
                };

                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured:  {ex.Message}");
            }

        }

        public async Task<AccountViewModel> GetAccountByStudentId(string studentId, string url)
        {
            try
            {
                if (studentId != null)
                {
                    var account = _appRepository.Accounts.Search(x => x.StudentId == studentId).FirstOrDefault();
                    if (account != null)
                    {
                        Links links = RandomGenerator.LinkGenerator(account.StudentId, url);
                        AccountViewModel viewModel = new AccountViewModel()
                        {
                            Id = account.Id,
                            StudentId = account.StudentId,
                            HasOutstandingBalance = account.HasOutstandingBalance,
                            Links = links
                        };
                        return viewModel;
                    }
                }
                throw new Exception("StudentId does not exist");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured:  {ex.Message}");
            }
        }


        public Task<Account> GetAccountById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AccountViewModel>> GetAllAccounts(string url)
        {
            List<AccountViewModel> viewModel = new List<AccountViewModel>();
            var account = await _appRepository.Accounts.GetAll();

            if (account.Any())
            {
                foreach (var item in account)
                {
                    AccountViewModel model = new AccountViewModel();

                    Links links = RandomGenerator.LinkGenerator(item.StudentId, url);
                    model = new AccountViewModel()
                    {
                        Id = item.Id,
                        StudentId = item.StudentId,
                        HasOutstandingBalance = item.HasOutstandingBalance,
                        Links = links
                    };
                    viewModel.Add(model);
                }
                return viewModel;
            }

            return null;
        }

        public async Task<bool> UpdateAccount(AccountViewModel model)
        {
            var acct = _appRepository.Accounts.Search(x => x.StudentId == model.StudentId).FirstOrDefault();
            if(acct != null)
            {
                acct.HasOutstandingBalance = model.HasOutstandingBalance;
                _appRepository.Accounts.Update(acct);
                _appRepository.Save();
                return true;
            }
           return false;
        }


        
    }
}
