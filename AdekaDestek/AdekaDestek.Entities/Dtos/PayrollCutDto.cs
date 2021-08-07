using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdekaDestek.Entities.Dtos
{
    public class PayrollCutDto
    {
        public decimal InsuranceDay { get; set; }
        public decimal InsuranceBase { get; set; }
        public decimal IncomeTaxBase { get; set; }
        public decimal DisabilityAllowance { get; set; }
        public decimal OhterDiscount { get; set; }
        public decimal TaxableBase { get; set; }
        public decimal CumulativeBase { get; set; }
        public decimal CumulativeTax { get; set; }
        public decimal StampTaxBase { get; set; }
        public decimal SskWorkerShare { get; set; }
        public decimal UnemploymentWorkerShare { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal StampDuty { get; set; }
        public decimal SskEmployerShare { get; set; }
        public decimal TotalOfLegalDeductions { get; set; }
        public decimal EmployerShareOfUnemployment { get; set; }
        public decimal LawEncouragement4857 { get; set; }


    }
}
