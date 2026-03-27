using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBank.DTOs;
using SmartBank.Models;
using SmartBank.Services;

namespace SmartBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _accountService.GetAllAccounts();

            var result = accounts.Select(a => new AccountDto
            {
                Id = a.Id,
                AccountNumber = a.AccountNumber,
                Name = a.Name,
                Balance = a.Balance
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var account = await _accountService.GetAccountById(id);

            if (account == null)
                return NotFound();

            return Ok(new AccountDto
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                Name = account.Name,
                Balance = account.Balance
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountDto dto)
        {
            var account = new Account
            {
                Name = dto.Name,
                Balance = dto.InitialDeposit,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _accountService.CreateAccount(account);

            return Ok(new AccountDto
            {
                Id = created.Id,
                AccountNumber = created.AccountNumber,
                Name = created.Name,
                Balance = created.Balance
            });
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit(TransactionDto dto)
        {
            await _accountService.Deposit(dto.AccountId, dto.Amount);
            return Ok("Deposit successful");
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw(TransactionDto dto)
        {
            await _accountService.Withdraw(dto.AccountId, dto.Amount);
            return Ok("Withdraw successful");
        }
    }
}