using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Entities.Dtos;
using FluentValidation;

namespace AdekaDestek.Mvc.ValidationRules.FluentValidation
{
    public class EmailSendDtoValidator : AbstractValidator<EmailSendDto>
    {
        public EmailSendDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Ad alanı boş geçilemez.");
            RuleFor(x => x.Name).MaximumLength(60).WithMessage("Ad alanı 60 karakterden büyük olmamalıdır.");
            RuleFor(x => x.Name).MinimumLength(5).WithMessage("Ad alanı 5 karakterden küçük olmamalıdır.");

            RuleFor(x => x.Email).NotNull().WithMessage("E-posta alanı boş geçilemez.");
            RuleFor(x => x.Email).MaximumLength(100).WithMessage("E-posta alanı 100 karakterden büyük olmamalıdır.");
            RuleFor(x => x.Email).MinimumLength(10).WithMessage("E-posta alanı 10 karakterden küçük olmamalıdır.");

            RuleFor(x => x.Subject).NotNull().WithMessage("Konu alanı boş geçilemez.");
            RuleFor(x => x.Subject).MaximumLength(125).WithMessage("Konu alanı 125 karakterden büyük olmamalıdır.");
            RuleFor(x => x.Subject).MinimumLength(5).WithMessage("Konu alanı 5 karakterden küçük olmamalıdır.");

            RuleFor(x => x.Message).NotNull().WithMessage("Mesaj alanı boş geçilemez.");
            RuleFor(x => x.Message).MaximumLength(2500).WithMessage("Mesaj alanı 2500 karakterden büyük olmamalıdır.");
            RuleFor(x => x.Message).MinimumLength(20).WithMessage("Mesaj alanı 20 karakterden küçük olmamalıdır.");
        }
    }
}
