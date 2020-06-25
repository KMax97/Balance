using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationBalance2.Models
{
    public class OutputVariable
    {
        private int id;
        private double reconsiledValue;

        public int Id { get => id; set => id = value; }
        public double ReconsiledValues { get => reconsiledValue; set => reconsiledValue = value; }
    }
}