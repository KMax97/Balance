using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationBalance2.Models;

namespace WebApplicationBalance2.Services
{
    public class CalculatorService
    {
        public BalanceOutput Calculate(BalanceInput balanceInput)
        {

            //BalanceOutput balanceOutput = new BalanceOutput();

            //balanceOutput.OutputVariables = new List<OutputVariable>();

            ////рассчет
            //balanceOutput.OutputVariables.Add(new OutputVariable { Id = 1, ReconsiledValues = 10 });

            

             Converter dataConverter = new Converter(balanceInput);
            BalanceOutput balanceOutput = dataConverter.Calculate1();
            return balanceOutput;
        }  

        
    }
}