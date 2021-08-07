using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdekaDestek.Entities.Dtos
{
    public class PayrollPersonelInfoDto
    {
        public string CompanyCode { get; set; }
        public string Company { get; set; }
        public string Department { get; set; }
        public string RegistrationNumber { get; set; }
        public string FirstNameAndLastName { get; set; }
        public string PersonelNo { get; set; }
        public string TCIdentificationNumber { get; set; }
        public string InsuranceRegistrationNumber { get; set; }
        public string StartDateOfWork { get; set; }
        public string SskStatu { get; set; }
        public string Period { get; set; }
        public string Task { get; set; }
        public string AccountNumber { get; set; }
        public string TypeOfFee { get; set; }
        public decimal FeeAmount { get; set; }
        public string FeePb { get; set; }
    }
}
