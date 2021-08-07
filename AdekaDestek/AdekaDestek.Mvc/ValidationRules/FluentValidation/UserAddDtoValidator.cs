using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Entities.Dtos;
using FluentValidation;

namespace AdekaDestek.Mvc.ValidationRules.FluentValidation
{
    public class UserAddDtoValidator : AbstractValidator<UserAddDto>
    {
        public UserAddDtoValidator()
        {
            RuleFor(x => x.UserName).NotNull().WithMessage("Kullanıcı adı boş geçilemez");
            RuleFor(x => x.UserName).MaximumLength(50).WithMessage("Kullıcı adı alanı 50 karakterden küçük olmalıdır.");
            RuleFor(x => x.UserName).MinimumLength(3).WithMessage("Kullıcı adı alanı 3 karakterden büyük olmalıdır.");

            RuleFor(x => x.Email).NotNull().WithMessage("E-posta adresi boş geçilmemelidir.");
            RuleFor(x => x.Email).MaximumLength(100).WithMessage("E-posta alanı 100 karakterden küçük olmalıdır.");
            RuleFor(x => x.Email).MinimumLength(10).WithMessage("E-posta alanı 10 karakterden büyük olmalıdır.");

            RuleFor(x => x.Password).NotNull().WithMessage("Şifre boş geçilmemelidir.");
            RuleFor(x => x.Password).MaximumLength(30).WithMessage("Şifre 30 karakterden küçük olmalıdır.");
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("Şifre 6 karakterden büyük olmalıdır.");

        
            RuleFor(x => x.PhoneNumber).MaximumLength(13).WithMessage("Telefon numarası 13 karakterden büyük olmamalıdır.");
            RuleFor(x => x.PhoneNumber).MinimumLength(13).WithMessage("Telefon numarası 13 karakterden küçük olmamalıdır.");

            RuleFor(x => x.FirstName).NotNull().WithMessage("Adı alanı boş geçilmemelidir.");
            RuleFor(x => x.FirstName).MaximumLength(30).WithMessage("Adı alanı 30 karakterden küçük olmalıdır.");
            RuleFor(x => x.FirstName).MinimumLength(2).WithMessage("Adı alanı 2 karakterden büyük olmalıdır.");

            RuleFor(x => x.LastName).NotNull().WithMessage("Soyadı alanı boş geçilmemelidir.");
            RuleFor(x => x.LastName).MaximumLength(30).WithMessage("Soyadı alanı 30 karakterden küçük olmalıdır.");
            RuleFor(x => x.LastName).MinimumLength(1).WithMessage("Soyadı alanı 1 karakterden büyük olmalıdır.");

            RuleFor(x => x.SapUserName).NotNull().WithMessage("Sap kullanıcı adı alanı boş geçilmemelidir.");
            RuleFor(x => x.SapUserName).MaximumLength(30).WithMessage("Sap kullanıcı adı alanı 30 karakterden küçük olmalıdır.");
            RuleFor(x => x.SapUserName).MinimumLength(3).WithMessage("Sap kullanıcı adı alanı 3 karakterden büyük olmalıdır.");

            RuleFor(x => x.InfiniUserName).NotNull().WithMessage("İnfini kullanıcı adı alanı boş geçilmemelidir.");
            RuleFor(x => x.InfiniUserName).MaximumLength(30).WithMessage("İnfini kullanıcı adı alanı 30 karakterden küçük olmalıdır.");
            RuleFor(x => x.InfiniUserName).MinimumLength(3).WithMessage("İnfini kullanıcı adı alanı 3 karakterden büyük olmalıdır.");

            RuleFor(x => x.SapEmployeeNo).NotNull().WithMessage("Sap personel numarası alanı boş geçilmemelidir.");
            RuleFor(x => x.SapEmployeeNo).MaximumLength(30).WithMessage("Sap personel numarası alanı 30 karakterden küçük olmalıdır.");
            RuleFor(x => x.SapEmployeeNo).MinimumLength(2).WithMessage("Sap personel numarası 2 karakterden büyük olmalıdır.");


        }
    }
}
