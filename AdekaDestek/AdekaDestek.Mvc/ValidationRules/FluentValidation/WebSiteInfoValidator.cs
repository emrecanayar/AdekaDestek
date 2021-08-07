using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Entities.Concrete;
using FluentValidation;

namespace AdekaDestek.Mvc.ValidationRules.FluentValidation
{
    public class WebSiteInfoValidator : AbstractValidator<WebSiteInfo>
    {
        public WebSiteInfoValidator()
        {
            RuleFor(x => x.Title).NotNull().WithMessage("Site Başlığı boş geçilmemelidir.");
            RuleFor(x => x.Title).MaximumLength(100).WithMessage("Site Başlığı 100 karakterden büyük olmamalıdır.");
            RuleFor(x => x.Title).MinimumLength(5).WithMessage("Site Başlığı 5 karakterden küçük olmamalıdır.");

            RuleFor(x => x.MenuTitle).NotNull().WithMessage("Menü üzerindeki site başlığı boş geçilmemelidir.");
            RuleFor(x => x.MenuTitle).MaximumLength(100).WithMessage("Menü üzerindeki site başlığı 100 karakterden büyük olmamalıdır.");
            RuleFor(x => x.MenuTitle).MinimumLength(5).WithMessage("Menü üzerindeki site başlığı 5 karakterden küçük olmamalıdır.");

            RuleFor(x => x.SeoDescription).NotNull().WithMessage("Seo açıklama boş geçilmemelidir.");
            RuleFor(x => x.SeoDescription).MaximumLength(100).WithMessage("Seo açıklama 100 karakterden büyük olmamalıdır.");
            RuleFor(x => x.SeoDescription).MinimumLength(5).WithMessage("Seo açıklama 5 karakterden küçük olmamalıdır.");

            RuleFor(x => x.SeoTags).NotNull().WithMessage("Seo etiketleri boş geçilmemelidir.");
            RuleFor(x => x.SeoTags).MaximumLength(100).WithMessage("Seo etiketleri 100 karakterden büyük olmamalıdır.");
            RuleFor(x => x.SeoTags).MinimumLength(5).WithMessage("Seo etiketleri 5 karakterden küçük olmamalıdır.");

            RuleFor(x => x.SeoAuthor).NotNull().WithMessage("Seo yazar boş geçilmemelidir.");
            RuleFor(x => x.SeoAuthor).MaximumLength(60).WithMessage("Seo yazar 60 karakterden büyük olmamalıdır.");
            RuleFor(x => x.SeoAuthor).MinimumLength(5).WithMessage("Seo yazar 5 karakterden küçük olmamalıdır.");
        }
    }
}
