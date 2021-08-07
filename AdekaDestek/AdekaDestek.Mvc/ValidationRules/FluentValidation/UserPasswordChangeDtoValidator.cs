using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Entities.Dtos;
using FluentValidation;

namespace AdekaDestek.Mvc.ValidationRules.FluentValidation
{
    public class UserPasswordChangeDtoValidator : AbstractValidator<UserPasswordChangeDto>
    {
        public UserPasswordChangeDtoValidator()
        {
            RuleFor(x => x.CurrentPassword).NotNull().WithMessage("Şu anki şifre alanı boş olmamalıdır.");
            RuleFor(x => x.CurrentPassword).MaximumLength(30).WithMessage("Şu anki şifre alanı 30 karakterden büyük olmamalıdır.");
            RuleFor(x => x.CurrentPassword).MinimumLength(6).WithMessage("Şu anki şifre alanı 6 karakterden küçük olmamalıdır.");

            RuleFor(x => x.NewPassword).NotNull().WithMessage("Yeni şifreniz alanı boş olmamalıdır.");
            RuleFor(x => x.NewPassword).MaximumLength(30).WithMessage("Yeni şifreniz  alanı 30 karakterden büyük olmamalıdır.");
            RuleFor(x => x.NewPassword).MinimumLength(6).WithMessage("Yeni şifreniz  alanı 6 karakterden küçük olmamalıdır.");

            RuleFor(x => x.RepeatPassword).NotNull().WithMessage("Yeni şifreniz tekrarı alanı boş olmamalıdır.");
            RuleFor(x => x.RepeatPassword).MaximumLength(30).WithMessage("Yeni şifreniz tekrarı 30 karakterden büyük olmamalıdır.");
            RuleFor(x => x.RepeatPassword).MinimumLength(6).WithMessage("Yeni şifreniz tekrarı 6 karakterden küçük olmamalıdır.");
        }
    }
}
