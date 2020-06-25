using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationBalance2.Models
{
    public class Flow
    {
        private double delta_error;
        private List<InfoFlows> flows;
        private List<Limit> limitations;

        public List<InfoFlows> Flows { get => flows; set => flows = value; }
        public List<Limit> Limitations { get => limitations; set => limitations = value; }
        public double Delta_error { get => delta_error; set => delta_error = value; }
    }
}