using Finance.Data.Entity;
using Finance.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Service.Interface
{
    public interface IAccountService
    {

        Task<AccountViewModel> CreateAccount(AccountModel account, string url);
        Task<IEnumerable<AccountViewModel>> GetAllAccounts(string url);

        Task<AccountViewModel> GetAccountByStudentId(string studentId, string url);

        Task<Account> GetAccountById(long id);

        Task<bool> UpdateAccount(AccountViewModel model);
    }
}
