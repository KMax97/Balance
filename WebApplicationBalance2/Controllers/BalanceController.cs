using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplicationBalance2.Models;
using WebApplicationBalance2.Services;

namespace WebApplicationBalance2.Controllers
{
    public class BalanceController : ApiController
    {
        // POST: api/Balance
        public BalanceOutput Post([FromBody]BalanceInput balanceInput)
        {
            CalculatorService service = new CalculatorService();
            BalanceOutput balanceOutput = service.Calculate(balanceInput);

            return balanceOutput;
        }
                
    }
}
