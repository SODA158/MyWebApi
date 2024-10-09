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
        public async Task<ActionResult> GetAll()
        {
            try 
            {
                return Ok(await _accountService.GetAllAccountAsync());
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetById(int id)
        {
            try
            {
                return await _accountService.GetAccountByIdAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Account>> Create(Account NewAccount)
        {
            try
            {
                return  await _accountService.AddAccountAsync(NewAccount);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }        

        [HttpPut("TopUp/{id}")]
        public async Task<ActionResult<Account>> TopUp(int id, decimal diposit)
        {
            try
            {
                return await _accountService.TopUpBalanceAccount(id, diposit);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

         [HttpPut("Withdraw/{id}")]
        public async Task<ActionResult<Account>> Withdraw(int id, decimal diposit)
        {
            try
            {
                return  await _accountService.WithdrawMoneyAccount(id, diposit);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Transfer")]
        public async Task<ActionResult<Account>> Transfer(int senderid, int recipientid, decimal diposit)
        {
            try
            {
                return await _accountService.TransferMoneyAccount(senderid, recipientid, diposit);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("BlockAccount/{id}")]
        public async Task<ActionResult<Account>> BlockAccount(int id)
        {
            try
            {
                return  await _accountService.BlockAccountById(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("UnblockAccount/{id}")]
        public async Task<ActionResult<Account>> UnblockAccount(int id)
        {
            try
            {
                return  await _accountService.UnblockAccountById(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<Account>> Delete(int id)
        {
            try
            {
                return await _accountService.DeleteAccountByIdAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }           
        }
    }
}
