using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel;
using System.Security.Principal;
using System.Xml;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var _accounts = _accountService.GetAllAccountAsync();
            return Ok(_accounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var _userAccount = await _accountService.GetAccountByIdAsync(id);
            if (_userAccount == null) return NotFound();
            else return Ok(_userAccount);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Account NewAccount)
        {
            var _userAccount = await _accountService.AddAccountAsync(NewAccount);
            return CreatedAtAction(nameof(GetById), new { id = NewAccount.Id }, NewAccount);
        }        

        [HttpPut("TopUp/{id}")]
        public async Task<IActionResult> TopUp(int id, decimal diposit)
        {
            var _userAccount = await _accountService.TopUpBalanceAccount(id, diposit);
            if (_userAccount == null) return NotFound("Счет не найден или заблокирован");
            else return Ok();
        }

         [HttpPut("Withdraw/{id}")]
        public async Task<IActionResult> Withdraw(int id, decimal diposit)
        {
            
            var _userAccount = await _accountService.WithdrawMoneyAccount(id, diposit);
            if (_userAccount == null) return NotFound("Счет не найден или заблокирован");
            else return Ok();
        }

        [HttpPut("Transfer")]
        public async Task<IActionResult> Transfer(int senderid, int recipientid, decimal diposit)
        {
            var result = await _accountService.TransferMoneyAccount(senderid, recipientid, diposit);
             if (result == null) return NotFound("Счет не найден или заблокирован");
            else return Ok();
        }

        [HttpPut("BlockAccount/{id}")]
        public async Task<IActionResult> BlockAccount(int id)
        {
            var _userAccount = await _accountService.BlockAccountById(id);
            if (_userAccount == null) return NotFound("Счет не найден");
            else return Ok();
        }

        [HttpPut("UnblockAccount/{id}")]
        public async Task<IActionResult> UnblockAccount(int id)
        {
            var _userAccount = await _accountService.UnblockAccountById(id);
            if (_userAccount == null) return NotFound("Счет не найден");
            else return Ok();
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var _userAccount = _accountService.DeleteAccountByIdAsync(id);
            if (_userAccount == null) return NotFound("Счет не найден");
            else return Ok();
        }

    }
}
