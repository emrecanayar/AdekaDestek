using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Entities.Dtos;
using FluentValidation;

namespace AdekaDestek.Mvc.ValidationRules.FluentValidation
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("Email adresi veya Infini kullanıcı adı boş geçilmemelidir.");
            RuleFor(x => x.Email).MaximumLength(100).WithMessage("E-posta alanı 100 karakterden küçük olmalıdır.");
            RuleFor(x => x.Email).MinimumLength(10).WithMessage("E-posta alanı 10 karakterden büyük olmalıdır.");

            RuleFor(x => x.Password).NotNull().WithMessage("Şifre boş geçilmemelidir.");
            RuleFor(x => x.Password).MaximumLength(30).WithMessage("Şifre 30 karakterden küçük olmalıdır.");
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("Şifre 6 karakterden büyük olmalıdır.");
        }
    }
}
