using Finance.Data.Models;
using Finance.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        Uri address;
        string url;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Add a new Student Account to Finance
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAccount(AccountModel model)
        {

            address = new Uri(Request.Host.ToString());
            url = address.ToString() + "/account";

            if (model == null)
            {
                return BadRequest(ModelState);
            }
            var result = await _accountService.CreateAccount(model, url);
            return Ok(result);
        }


        /// <summary>
        /// Get the list of Accounts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAccount()
        {
            address = new Uri(Request.Host.ToString());
            url = address.ToString() + "/account";

            var accountList = await _accountService.GetAllAccounts(url);
            if (accountList == null)
            {
                return NotFound();
            }
            return Ok(accountList);
        }


        /// <summary>
        /// Get account by studentid
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetAccountByStudentId(string studentId)
        {
            address = new Uri(Request.Host.ToString());
            url = address.ToString() + "/account";

            var account = await _accountService.GetAccountByStudentId(studentId, url);

            if (account != null)
            {
                return Ok(account);
            }
            else
            {
                return BadRequest();
            }
        }

    
        [HttpPost("UpdateOutstandingAccount")]
        public async Task<IActionResult> UpdateOutstandingAccount(AccountViewModel model)
        {
            if (model == null)
            {
                return BadRequest(ModelState);
            }
            var result = await _accountService.UpdateAccount(model);
            return Ok(result);
        }
    }
}
