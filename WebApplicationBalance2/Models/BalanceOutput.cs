using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationBalance2.Models
{
    public class BalanceOutput
    {
        private bool isBalancing;
        private String message;
        private double[] flows;
        
        public bool IsBalancing { get => isBalancing; set => isBalancing = value; }
        public string Message { get => message; set => message = value; }
        public double[] Flows { get => flows; set => flows = value; }
        
    }
}