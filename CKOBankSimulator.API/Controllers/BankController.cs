using CKOBankSimulator.API.BusinessRule;
using CKOBankSimulator.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CKOBankSimulator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : Controller
    {
        private readonly IBusiness _business;
        public BankController(IBusiness business)
        {
            _business = business;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CheckMerchantPayment([FromBody] BankDetails details)
        {

            var result = _business.CheckBankDetails(bankDetails: details);
             return Ok(result);
            
        }
    }
}
