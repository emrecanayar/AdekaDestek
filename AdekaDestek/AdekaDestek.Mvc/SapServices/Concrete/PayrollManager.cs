using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using AdekaDestek.Core.Utilities.Results.Abstract;
using AdekaDestek.Core.Utilities.Results.Concrete;
using AdekaDestek.Entities.Dtos;
using AdekaDestek.Mvc.SapServices.Abstract;
using PayrollService;

namespace AdekaDestek.Mvc.SapServices.Concrete
{
    public class PayrollManager : IPayrollService
    {

        public const string binding = "ZHR_READ_PAYROLL_BIND";
        public const string endPoint = "http://adkerpqa.adeka.com.tr:8000/sap/bc/srt/rfc/sap/zhr_read_payroll_result_sdef/100/zhr_read_payroll_ser/zhr_read_payroll_bin";
        public async Task<IDataResult<PayrollListDto>> GetPayrollDetailsAsync(string period, string sapPersonelNo)
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            basicHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
            EndpointAddress endpointAddress = new EndpointAddress(endPoint);

            ZHR_READ_PAYROLL_RESULT_SDEFClient client = new ZHR_READ_PAYROLL_RESULT_SDEFClient(basicHttpBinding, endpointAddress);

            ZhrReadPayrollResult request = new ZhrReadPayrollResult();
            request.IPeriod = period;
            request.IPernr = sapPersonelNo;
            request.IPernr = "139";

            List<PayrollPersonelInfoDto> payrollPersonelInfoList = new List<PayrollPersonelInfoDto>();
            List<PayrollAllPaymentDto> payrollAllPaymentList = new List<PayrollAllPaymentDto>();
            List<PayrollAdditionalPaymentDto> payrollAdditionalPaymentList = new List<PayrollAdditionalPaymentDto>();

            ZhrReadPayrollResultResponse1 response = new ZhrReadPayrollResultResponse1();

            try
            {
                response = await client.ZhrReadPayrollResultAsync(request);
                var payrollPersonelInfo = new PayrollPersonelInfoDto();

                //Personel Bilgileri
                payrollPersonelInfo.CompanyCode = response.ZhrReadPayrollResultResponse.EsPersonelBilgi.Sirketkodu;
                payrollPersonelInfo.Company = response.ZhrReadPayrollResultResponse.EsPersonelBilgi.Firma;
                payrollPersonelInfo.Department = response.ZhrReadPayrollResultResponse.EsPersonelBilgi.Departman;
                payrollPersonelInfo.RegistrationNumber = response.ZhrReadPayrollResultResponse.EsPersonelBilgi.Sirketsicilno;
                payrollPersonelInfo.FirstNameAndLastName = response.ZhrReadPayrollResultResponse.EsPersonelBilgi.Adsoyad;
                payrollPersonelInfo.PersonelNo = response.ZhrReadPayrollResultResponse.EsPersonelBilgi.Personelno;
                payrollPersonelInfo.TCIdentificationNumber = response.ZhrReadPayrollResultResponse.EsPersonelBilgi.Tckimlikno;
                payrollPersonelInfo.InsuranceRegistrationNumber = response.ZhrReadPayrollResultResponse.EsPersonelBilgi.Sigortano;
                payrollPersonelInfo.StartDateOfWork = response.ZhrReadPayrollResultResponse.EsPersonelBilgi.Isegiris;
                payrollPersonelInfo.SskStatu = response.ZhrReadPayrollResultResponse.EsPersonelBilgi.Sskstatu;
                payrollPersonelInfo.Period = response.ZhrReadPayrollResultResponse.EsPersonelBilgi.Donem;
                payrollPersonelInfo.Task = response.ZhrReadPayrollResultResponse.EsPersonelBilgi.Gorev;
                payrollPersonelInfo.AccountNumber = response.ZhrReadPayrollResultResponse.EsPersonelBilgi.Hesapno;
                payrollPersonelInfo.TypeOfFee = response.ZhrReadPayrollResultResponse.EsPersonelBilgi.Ucretcinsi;
                payrollPersonelInfo.FeeAmount = response.ZhrReadPayrollResultResponse.EsPersonelBilgi.Ucrettutari;
                payrollPersonelInfo.FeePb = response.ZhrReadPayrollResultResponse.EsPersonelBilgi.Ucretpb;

                payrollPersonelInfoList.Add(payrollPersonelInfo);

                var payrollAllPayment = new PayrollAllPaymentDto();

                //Kesintiler
                payrollAllPayment.InsuranceDay = response.ZhrReadPayrollResultResponse.EsYasalKesintiler.Sigortagunu;
                payrollAllPayment.InsuranceBase = response.ZhrReadPayrollResultResponse.EsYasalKesintiler.Sigortamatrahi;
                payrollAllPayment.IncomeTaxBase = response.ZhrReadPayrollResultResponse.EsYasalKesintiler.Gelirvergisimatrahi;
                payrollAllPayment.DisabilityAllowance = response.ZhrReadPayrollResultResponse.EsYasalKesintiler.Sakatlikind;
                payrollAllPayment.OhterDiscount = response.ZhrReadPayrollResultResponse.EsYasalKesintiler.Digerind;
                payrollAllPayment.TaxableBase = response.ZhrReadPayrollResultResponse.EsYasalKesintiler.Vergiyetabimatrah;
                payrollAllPayment.CumulativeBase = response.ZhrReadPayrollResultResponse.EsYasalKesintiler.Kumulatifmatrah;
                payrollAllPayment.CumulativeTax = response.ZhrReadPayrollResultResponse.EsYasalKesintiler.Kumulatifvergi;
                payrollAllPayment.StampTaxBase = response.ZhrReadPayrollResultResponse.EsYasalKesintiler.Damgavergisimatrahi;
                payrollAllPayment.SskWorkerShare = response.ZhrReadPayrollResultResponse.EsYasalKesintiler.Sskiscipayi;
                payrollAllPayment.UnemploymentWorkerShare = response.ZhrReadPayrollResultResponse.EsYasalKesintiler.Issizlikiscipayi;
                payrollAllPayment.IncomeTax = response.ZhrReadPayrollResultResponse.EsYasalKesintiler.Gelirvergisi;
                payrollAllPayment.StampDuty = response.ZhrReadPayrollResultResponse.EsYasalKesintiler.Damgavergisi;
                payrollAllPayment.SskEmployerShare = response.ZhrReadPayrollResultResponse.EsYasalKesintiler.Isverensskpayi;
                payrollAllPayment.TotalOfLegalDeductions = response.ZhrReadPayrollResultResponse.EsYasalKesintiler.Yasalkesintilertoplami;
                payrollAllPayment.EmployerShareOfUnemployment = response.ZhrReadPayrollResultResponse.EsYasalKesintiler.Issizlikisverenpayi;
                payrollAllPayment.LawEncouragement4857 = response.ZhrReadPayrollResultResponse.EsYasalKesintiler._34857kanuntesviki;

                //Kazançlar
                payrollAllPayment.NormalWorkingDay = response.ZhrReadPayrollResultResponse.EsKazanclar.Normalcalismagun;
                payrollAllPayment.NormalWorkingHour = response.ZhrReadPayrollResultResponse.EsKazanclar.Normalcalismasaat;
                payrollAllPayment.NormalWorkingAmount = response.ZhrReadPayrollResultResponse.EsKazanclar.Normalcalismatutar;
                payrollAllPayment.WeekendDay = response.ZhrReadPayrollResultResponse.EsKazanclar.Haftatatiligun;
                payrollAllPayment.WeekendHour = response.ZhrReadPayrollResultResponse.EsKazanclar.Haftatatilisaat;
                payrollAllPayment.WeekendAmount = response.ZhrReadPayrollResultResponse.EsKazanclar.Haftatatilitutar;
                payrollAllPayment.PublicHoliday = response.ZhrReadPayrollResultResponse.EsKazanclar.Resmitatilgun;
                payrollAllPayment.PublicHolidayHour = response.ZhrReadPayrollResultResponse.EsKazanclar.Resmitatilsaat;
                payrollAllPayment.PublicHolidayAmount = response.ZhrReadPayrollResultResponse.EsKazanclar.Resmitatiltutar;
                payrollAllPayment.Fm50Hour = response.ZhrReadPayrollResultResponse.EsKazanclar.Fm50saat;
                payrollAllPayment.Fm50Amount = response.ZhrReadPayrollResultResponse.EsKazanclar.Fm50tutar;
                payrollAllPayment.Fm100Hour = response.ZhrReadPayrollResultResponse.EsKazanclar.Fm100saat;
                payrollAllPayment.Fm100Amount = response.ZhrReadPayrollResultResponse.EsKazanclar.Fm100tutar;
                payrollAllPayment.FmTotalHour = response.ZhrReadPayrollResultResponse.EsKazanclar.Fmtoplamisaat;
                payrollAllPayment.FmTotalAmount = response.ZhrReadPayrollResultResponse.EsKazanclar.Fmtoplamitutar;
                payrollAllPayment.SumOfAllGain = response.ZhrReadPayrollResultResponse.EsKazanclar.Tumkazanclartoplami;
                payrollAllPayment.SumOfAllDeduction = response.ZhrReadPayrollResultResponse.EsKazanclar.Tumkesintilertoplami;
                payrollAllPayment.MinimumLivingAllowance = response.ZhrReadPayrollResultResponse.EsKazanclar.Asgarigecimindirimi;
                payrollAllPayment.NetTotalPaid = response.ZhrReadPayrollResultResponse.EsKazanclar.Netodenentoplami;


                //Ek Ödemeler
                payrollAllPayment.TotalOfAdditionalPayments = response.ZhrReadPayrollResultResponse.EsEkodemeler.Ekodemelertoplami;
                payrollAllPayment.MarriageBenefitNet = response.ZhrReadPayrollResultResponse.EsEkodemeler.Netevlilik;
                payrollAllPayment.MarriageBenefitNb = response.ZhrReadPayrollResultResponse.EsEkodemeler.Nbevlilik;
                payrollAllPayment.MarriageBenefitNk = response.ZhrReadPayrollResultResponse.EsEkodemeler.Nkevlilik;
                payrollAllPayment.MaternityAllowanceNet = response.ZhrReadPayrollResultResponse.EsEkodemeler.Netdogum;
                payrollAllPayment.MaternityAllowanceNb = response.ZhrReadPayrollResultResponse.EsEkodemeler.Nbdogum;
                payrollAllPayment.MaternityAllowanceNk = response.ZhrReadPayrollResultResponse.EsEkodemeler.Nkdogum;
                payrollAllPayment.ChildBenefit = response.ZhrReadPayrollResultResponse.EsEkodemeler.Cocuk;
                payrollAllPayment.GoldenHandshake = response.ZhrReadPayrollResultResponse.EsEkodemeler.Kidem;
                payrollAllPayment.TerminationBenefits = response.ZhrReadPayrollResultResponse.EsEkodemeler.Ihbar;
                payrollAllPayment.Premium = response.ZhrReadPayrollResultResponse.EsEkodemeler.Prim;
                payrollAllPayment.AdvancePayment = response.ZhrReadPayrollResultResponse.EsEkodemeler.Avans;
                payrollAllPayment.DeathAidNet = response.ZhrReadPayrollResultResponse.EsEkodemeler.Netolum;
                payrollAllPayment.DeathAidNb = response.ZhrReadPayrollResultResponse.EsEkodemeler.Nbolum;
                payrollAllPayment.DeathAidNk = response.ZhrReadPayrollResultResponse.EsEkodemeler.Nkolum;
                payrollAllPayment.CollectibleNet = response.ZhrReadPayrollResultResponse.EsEkodemeler.Nettahsil;
                payrollAllPayment.CollectibleNb = response.ZhrReadPayrollResultResponse.EsEkodemeler.Nbtahsil;
                payrollAllPayment.CollectibleNk = response.ZhrReadPayrollResultResponse.EsEkodemeler.Nktahsil;
                payrollAllPayment.FoodAllowanceNet = response.ZhrReadPayrollResultResponse.EsEkodemeler.Netyemek;
                payrollAllPayment.FoodAllowanceNb = response.ZhrReadPayrollResultResponse.EsEkodemeler.Nbyemek;
                payrollAllPayment.FoodAllowanceNk = response.ZhrReadPayrollResultResponse.EsEkodemeler.Nkyemek;
                payrollAllPayment.TravelAllowance = response.ZhrReadPayrollResultResponse.EsEkodemeler.Yolparasibrut;


                //Ek Özel Kesintiler
                payrollAllPayment.AnnualPermit = response.ZhrReadPayrollResultResponse.EsOzelKesintiler.Yillikizin;
                payrollAllPayment.TrafficPenalty = response.ZhrReadPayrollResultResponse.EsOzelKesintiler.Trafikhgsceza;
                payrollAllPayment.PhoneDataUsage = response.ZhrReadPayrollResultResponse.EsOzelKesintiler.Telefondata;
                payrollAllPayment.PrivateHealthInsurance = response.ZhrReadPayrollResultResponse.EsOzelKesintiler.Ozelsigorta;
                payrollAllPayment.PremiumDeduction = response.ZhrReadPayrollResultResponse.EsOzelKesintiler.Netprim;
                payrollAllPayment.AdvanceDeduction = response.ZhrReadPayrollResultResponse.EsOzelKesintiler.Avans;
                payrollAllPayment.Bes = response.ZhrReadPayrollResultResponse.EsOzelKesintiler.Bes;
                payrollAllPayment.SpecialDeductionsTotal = response.ZhrReadPayrollResultResponse.EsOzelKesintiler.Ozelkesintilertoplami;
                payrollAllPaymentList.Add(payrollAllPayment);

                var getPayroll = new PayrollListDto
                {
                    PayrollPersonelInfoList = payrollPersonelInfoList,
                    PayrollAllPaymentList = payrollAllPaymentList
                };

                return new DataResult<PayrollListDto>(Core.Utilities.Results.ComplexTypes.ResultStatus.Success, data: getPayroll, message: "İlgili kişiye ait bordro bilgileri başarıyla getirilmiştir");

            }
            catch (Exception exception)
            {
                return new DataResult<PayrollListDto>(Core.Utilities.Results.ComplexTypes.ResultStatus.Error, data: null, message: $"{exception.Message}", exception: exception);

            }







        }


    }
}
