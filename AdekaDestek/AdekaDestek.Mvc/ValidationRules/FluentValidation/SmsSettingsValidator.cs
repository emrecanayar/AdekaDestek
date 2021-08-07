using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Entities.Concrete;
using FluentValidation;

namespace AdekaDestek.Mvc.ValidationRules.FluentValidation
{
    public class SmsSettingsValidator : AbstractValidator<SmsSettings>
    {
        public SmsSettingsValidator()
        {
            RuleFor(x => x.Username).NotNull().WithMessage("Kullanıcı adı boş geçilemez");
            RuleFor(x => x.Username).MaximumLength(10).WithMessage("Kullanıcı adı 10 haneden büyük olmamalıdır.");
            RuleFor(x => x.Username).MinimumLength(10).WithMessage("Kullanıcı adı 10 haneden küçük olmamalıdır.");


        }
    }
}
