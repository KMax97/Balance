using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationBalance2.Models
{
    public class BalanceInput
    {
        private List<Flow> flows;

        public List<Flow> Flows { get => flows; set => flows = value; }
    }
}