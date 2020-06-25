using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationBalance2.Models
{
    public class InfoFlows
    {
        private String id;
        private String sourceId;
        private String destinationId;
        private bool isExcluded;
        private double value;
        private double tolerance;
        private bool useTecnologicRanges;
        private List<TecnologicRange> tecnologicRanges;

        public String Id { get => id; set => id = value; }
        public String SourceId { get => sourceId; set => sourceId = value; }
        public String DestinationId { get => destinationId; set => destinationId = value; }
        public bool IsExcluded { get => isExcluded; set => isExcluded = value; }
        public double Value { get => value; set => this.value = value; }
        public double Tolerance { get => tolerance; set => tolerance = value; }
        public bool UseTecnologicRanges { get => useTecnologicRanges; set => useTecnologicRanges = value; }
        public List<TecnologicRange> TecnologicRanges { get => tecnologicRanges; set => tecnologicRanges = value; }
        
    }

    public class TecnologicRange
    {
        private double minValue;
        private double maxValue;

        public double MinValue { get => minValue; set => minValue = value; }
        public double MaxValue { get => maxValue; set => maxValue = value; }
    }
}