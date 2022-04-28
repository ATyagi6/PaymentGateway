using CKOBankSimulator.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CKOBankSimulator.API.BusinessRule
{
  public interface IBusiness
    {
        public bool CheckBankDetails(BankDetails bankDetails);
    }
}
