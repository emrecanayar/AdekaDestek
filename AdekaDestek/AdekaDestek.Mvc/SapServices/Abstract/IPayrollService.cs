using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Core.Utilities.Results.Abstract;
using AdekaDestek.Entities.Dtos;

namespace AdekaDestek.Mvc.SapServices.Abstract
{
    public interface IPayrollService
    {
        Task <IDataResult<PayrollListDto>> GetPayrollDetailsAsync(string period, string sapPersonelNo);
    }
}
