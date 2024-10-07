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
    [Route("api/Account")]
    public class AccountController : Controller
    {
       private AppDBContext _context;
       public AccountController(AppDBContext context)
        {
            _context = context;
            //_context.SaveChanges(); 
        }


        [HttpPost("Create")]
        public IActionResult Create(Account NewAccount)
        {
            var _userAccount = _context.Accounts.FirstOrDefault(w => w.Id == NewAccount.Id);
            if (_userAccount != null) return Content("Счет уже зарегистрирован!");
            else
            {
                NewAccount.Balance = Decimal.Round(NewAccount.Balance,2);
                _context.Accounts.Add(NewAccount);
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpGet("Get/{id}")]
        public IActionResult Get(int id)
        {
            var _userAccount = _context.Accounts.FirstOrDefault(w => w.Id == id);
            if (_userAccount == null) return NotFound();
            else return new JsonResult(_userAccount);
        }


        [HttpGet("AllAccounts")]
        public IActionResult GetAll()
        {
            return new JsonResult(_context.Accounts.ToArray());
        }

        

        [HttpPut("TopUpBalance/{id}")]
        public IActionResult TopUpBalance(int id, decimal diposit)
        {
            var _userAccount = _context.Accounts.FirstOrDefault(w => w.Id == id);
            if (_userAccount == null) return NotFound();

            if (_userAccount.Blocked) return Content("Счет заблокирован!");
            else if (diposit > 0)
            {
                _userAccount.Balance += Decimal.Round(diposit, 2);
                _context.SaveChanges();
                return Ok();
            }
            else return Content("Сумма для пополнения не может быть меньше либо равна 0!");
        }


        [HttpPut("Transfer")]
        public IActionResult TransferMoney(int senderid, int recipientid, decimal diposit)
        {
            var _sender = _context.Accounts.FirstOrDefault(w => w.Id == senderid);
            var _recipient = _context.Accounts.FirstOrDefault(w => w.Id == recipientid);
            if (_sender == null) return Content("Счет отправителя не найден!");
            if (_recipient == null) return Content("Счет получателя не найден!");
            if(_sender.Blocked) return Content("Счет отправителя заблокирован!");
            if (_recipient.Blocked) return Content("Счет получателя заблокирован!");

            if (diposit > 0 && diposit<=_sender.Balance) {
                _sender.Balance -= Decimal.Round(diposit, 2);
                _recipient.Balance += Decimal.Round(diposit, 2);
                _context.SaveChanges();
                return Ok();
            }
            else return Content("Неверная сумма перевода или недостаточно средств для перевода!");
        }


        [HttpPut("WithdrawMoney/{id}")]
        public IActionResult WithdrawMoney(int id, decimal diposit)
        {
            
            var _userAccount = _context.Accounts.FirstOrDefault(w => w.Id == id);
            if (_userAccount == null) return NotFound();

            if (_userAccount.Blocked)  return Content("Счет заблокирован!");
            else if (_userAccount.Balance >= diposit)
            {
                _userAccount.Balance -= Decimal.Round(diposit, 2);
                _context.SaveChanges();
                return Ok();
            }
            else return Content("Недостаточно средств для снятия!");
        }

        [HttpPut("BlockAccount/{id}")]
        public IActionResult BlockAccount(int id)
        {
            var _userAccount = _context.Accounts.FirstOrDefault(w => w.Id == id);
            if (_userAccount == null) return NotFound();
            else 
            {
                _userAccount.Blocked = true;
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpPut("UnblockAccount/{id}")]
        public IActionResult UnblockAccount(int id)
        {
            var _userAccount = _context.Accounts.FirstOrDefault(w => w.Id == id);
            if (_userAccount == null) return NotFound();
            else
            {
                _userAccount.Blocked = false;
                _context.SaveChanges();
                return Ok();
            }
        }


        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var _userAccount = _context.Accounts.FirstOrDefault(w => w.Id == id);
            if (_userAccount == null) return NotFound();
            else
            {
                _context.Accounts.Remove(_userAccount);
                _context.SaveChanges();
                return Ok();
            }
        }

    }
}
