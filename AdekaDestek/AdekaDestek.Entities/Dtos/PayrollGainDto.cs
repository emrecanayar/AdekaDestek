using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdekaDestek.Entities.Dtos
{
    public class PayrollGainDto
    {
        public string NormalWorkingDay { get; set; }
        public decimal NormalWorkingHour { get; set; }
        public decimal NormalWorkingAmount { get; set; }
        public string WeekendDay { get; set; }
        public decimal WeekendHour { get; set; }
        public decimal WeekendAmount { get; set; }
        public string PublicHoliday { get; set; }
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
    }
}
