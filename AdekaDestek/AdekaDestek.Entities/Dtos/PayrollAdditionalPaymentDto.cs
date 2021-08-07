using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdekaDestek.Entities.Dtos
{
    public class PayrollAdditionalPaymentDto
    {
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
    }
}
