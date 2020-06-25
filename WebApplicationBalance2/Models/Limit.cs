using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationBalance2.Models
{
    public class Limit
    {
        private List<A> a;
        private String sign;
        private double b;

        public string Sign { get => sign; set => sign = value; }
        public double B { get => b; set => b = value; }
        public List<A> A { get => a; set => a = value; }
    }
    public class A
    {
        private int coefficient;
        private string flowId;

        public string FlowId { get => flowId; set => flowId = value; }
        public int Coefficient { get => coefficient; set => coefficient = value; }
    }
}