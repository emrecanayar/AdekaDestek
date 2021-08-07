using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdekaDestek.Entities.Dtos
{
    public class PayrollAllPaymentDto
    {
        public decimal NormalWorkingDay { get; set; }
        public decimal NormalWorkingHour { get; set; }
        public decimal NormalWorkingAmount { get; set; }
        public decimal WeekendDay { get; set; }
        public decimal WeekendHour { get; set; }
        public decimal WeekendAmount { get; set; }
        public decimal PublicHoliday { get; set; }
        public decimal PublicHolidayHour { get; set; }
        public decimal PublicHolidayAmount { get; set; }
        public decimal Fm50Hour { get; set; }
        public decimal Fm50Amount { get; set; }
        public decimal Fm100Hour { get; set; }
        public decimal Fm100Amount { get; set; }
        public decimal FmTotalHour { get; set; }
        public decimal FmTotalAmount { get; set; }
        public decimal SumOfAllGain { get; set; }
        public decimal SumOfAllDeduction { get; set; }
        public decimal MinimumLivingAllowance { get; set; }
        public decimal NetTotalPaid { get; set; }
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
        public decimal TotalOfAdditionalPayments { get; set; }
        public decimal MarriageBenefitNet { get; set; }
        public decimal MarriageBenefitNb { get; set; }
        public decimal MarriageBenefitNk { get; set; }
        public decimal MaternityAllowanceNet { get; set; }
        public decimal MaternityAllowanceNb { get; set; }
        public decimal MaternityAllowanceNk { get; set; }
        public decimal ChildBenefit { get; set; }
        public decimal GoldenHandshake { get; set; }
        public decimal TerminationBenefits { get; set; }
        public decimal Premium { get; set; }
        public decimal AdvancePayment { get; set; }
        public decimal DeathAidNet { get; set; }
        public decimal DeathAidNb { get; set; }
        public decimal DeathAidNk { get; set; }
        public decimal CollectibleNet { get; set; }
        public decimal CollectibleNb { get; set; }
        public decimal CollectibleNk { get; set; }
        public decimal FoodAllowanceNet { get; set; }
        public decimal FoodAllowanceNb { get; set; }
        public decimal FoodAllowanceNk { get; set; }
        public decimal TravelAllowance { get; set; }
        public decimal SpecialDeductionsTotal { get; set; }
        public decimal AnnualPermit { get; set; }
        public decimal TrafficPenalty { get; set; }
        public decimal PhoneDataUsage { get; set; }
        public decimal PrivateHealthInsurance { get; set; }
        public decimal PremiumDeduction { get; set; }
        public decimal AdvanceDeduction { get; set; }
        public decimal Bes { get; set; }
    }
}
