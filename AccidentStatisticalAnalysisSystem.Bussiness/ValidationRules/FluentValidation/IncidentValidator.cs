using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Bussiness.ValidationRules.FluentValidation
{
    public class IncidentValidator:AbstractValidator<Incident>
    {
        public IncidentValidator() 
        { 
            RuleFor(p=>p.CompanyId).NotEmpty().WithMessage("Şirket bilgisi boş geçilemez.");
            RuleFor(p => p.IncidentDescription).NotEmpty().WithMessage("Kaza Açıklaması boş geçilemez.");
            RuleFor(p => p.EmployerTypeId).NotEmpty().WithMessage("İşveren Türü boş geçilemez.");
            RuleFor(p => p.CityId).NotEmpty().WithMessage("Şehir Bilgisi Boşgeçilemez.");
            RuleFor(p => p.NotificationId).NotEmpty().WithMessage("Bildirim Türü boş geçilemez.");
            RuleFor(p => p.OperatingModesId).NotEmpty().WithMessage("işletmen modu boş geçilemez.");
            RuleFor(p => p.Image).NotEmpty().WithMessage("Resim boş geçilemez.");

            RuleFor(p => p.Deaths).GreaterThanOrEqualTo(Convert.ToInt16(0)).WithMessage("Ölü sayısı 0'dan küçük olamaz.");
            RuleFor(p => p.Injured).GreaterThanOrEqualTo(Convert.ToInt16(0)).WithMessage("Yaralı sayısı 0'dan küçük olamaz.");
            RuleFor(p => p.Evacuation).GreaterThanOrEqualTo(Convert.ToInt16(0)).WithMessage("Tahliye sayısı 0'dan küçük olamaz.");
            RuleFor(p => p.PropertyDamagerty).GreaterThanOrEqualTo(Convert.ToDouble(0)).WithMessage("Tahliye sayısı 0'dan küçük olamaz.");
            RuleFor(p => p.Hour).GreaterThanOrEqualTo(Convert.ToUInt16(0)).WithMessage("Saat 0'dan küçük olamaz.");
            RuleFor(p => p.Minute).GreaterThanOrEqualTo(Convert.ToUInt16(0)).WithMessage("Dakika 0'dan küçük olamaz.");

            RuleFor(p => p.Hour).LessThanOrEqualTo(Convert.ToUInt16(23)).WithMessage("Saat 23'den büyük olamaz.");
            RuleFor(p => p.Minute).LessThanOrEqualTo(Convert.ToUInt16(59)).WithMessage("Dakika 59'dan büyük olamaz.");





        }
    }
}
