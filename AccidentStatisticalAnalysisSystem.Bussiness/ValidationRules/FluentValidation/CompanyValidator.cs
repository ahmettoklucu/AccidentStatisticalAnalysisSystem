using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Bussiness.ValidationRules.FluentValidation
{
    public class CompanyValidator:AbstractValidator<Company>
    {
        public CompanyValidator() 
        { 
            RuleFor(p=>p.CompanyName).NotEmpty().WithMessage("Şirket unvanı boş geçilemez.");
            RuleFor(p => p.CompanyTypeId).NotEmpty().WithMessage("Kuruluş Çeşidi boş geçilemez.");
            RuleFor(p => p.NaceId).NotEmpty().WithMessage("Nace kodu boş geçilemez.");
            RuleFor(p => p.CityId).NotEmpty().WithMessage("Şehir bilgisi boş geçilemez.");
            RuleFor(p => p.Image).NotEmpty().WithMessage("Resim boş geçilemez.");
            RuleFor(p => p.UserId).NotEmpty().WithMessage("Kullanıcı boş geçilemez.");

            RuleFor(p => p.NumberOfWorkers).GreaterThanOrEqualTo(Convert.ToUInt16(0)).WithMessage("İşçi sayısı 0'dan küçük olamaz.");


        }
    }
}
