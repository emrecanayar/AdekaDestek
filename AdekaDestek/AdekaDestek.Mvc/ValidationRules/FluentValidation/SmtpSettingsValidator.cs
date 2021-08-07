using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Entities.Concrete;
using FluentValidation;

namespace AdekaDestek.Mvc.ValidationRules.FluentValidation
{
    public class SmtpSettingsValidator : AbstractValidator<SmtpSettings>
    {
        public SmtpSettingsValidator()
        {
            RuleFor(x => x.Server).NotNull().WithMessage("Sunucu alanı boş geçilemez.");
            RuleFor(x => x.Server).MaximumLength(100).WithMessage("Sunucu alanı 100 karakterden büyük olmamalıdır.");
            RuleFor(x => x.Server).MinimumLength(5).WithMessage("Sunucu alanı 5 karakterden küçük olmamalıdır.");

            RuleFor(x => x.Port).NotNull().WithMessage("Port alanı boş geçilemez.");
            RuleFor(x => x.Port).GreaterThan(0).WithMessage("Port alanı 9999 ile 0 değerleri arasında olmalıdır.");
            RuleFor(x => x.Port).LessThan(9999).WithMessage("Port alanı 9999 ile 0 değerleri arasında olmalıdır.");

            RuleFor(x => x.SenderName).NotNull().WithMessage("Gönderen alanı boş geçilemez.");
            RuleFor(x => x.SenderName).MaximumLength(100).WithMessage("Gönderen alanı 100 karakterden büyük olmamalıdır.");
            RuleFor(x => x.SenderName).MinimumLength(2).WithMessage("Gönderen alanı 5 karakterden küçük olmamalıdır.");

            RuleFor(x => x.SenderEmail).MaximumLength(100).WithMessage("E-posta alanı 100 karakterden büyük olmamalıdır.");
            RuleFor(x => x.SenderEmail).MinimumLength(10).WithMessage("E-posta alanı 10 karakterden küçük olmamalıdır.");

            RuleFor(x => x.Username).NotNull().WithMessage("Kullanıcı adı boş geçilemez.");
            RuleFor(x => x.Username).MaximumLength(100).WithMessage("Kullanıcı adı 100 karakterden büyük olmamalıdır.");
            RuleFor(x => x.Username).MinimumLength(1).WithMessage("Kullanıcı adı 1 karakterden küçük olmamalıdır.");

            RuleFor(x => x.Username).MaximumLength(100).WithMessage("Şifre 100 karakterden büyük olmamalıdır.");
            RuleFor(x => x.Username).MinimumLength(5).WithMessage("Şifre 1 karakterden küçük olmamalıdır.");
        }
    }
}
