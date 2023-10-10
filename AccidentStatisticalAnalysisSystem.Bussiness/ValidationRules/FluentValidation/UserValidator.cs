using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.Bussiness.ValidationRules.FluentValidation
{
    public class UserValidator:AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.EMail).NotEmpty().WithMessage("Email adresi boş geçilemez.");
            RuleFor(p => p.UserName).NotEmpty().WithMessage("Kullanıcı ismi boş geçilemez.");
            RuleFor(p => p.PhoneNumber).NotEmpty().WithMessage("Telefon numarası boş geçilemez.");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre boş geçilemez.");

            RuleFor(p => p.PhoneNumber).Must(NumericControl).WithMessage("Telefon numarasi sayilardan oluşmalıdır.");
            RuleFor(p => p.Password).Must(PasswordControl).WithMessage("Şifre büyük harf,küçük harf,sayi ve özel karakter içermelidir.");
            RuleFor(p => p.EMail).EmailAddress().WithMessage("EMail adresi email formatına uymamaktadır.");


            RuleFor(p => p.Password).MinimumLength(8).WithMessage("Şifre en az 8 karakterden oluşmalıdır.");
            RuleFor(p => p.UserName).MaximumLength(15).WithMessage("Kullanıcı ismi en az 15 karakterden oluşmalıdır.");

        }
        private bool NumericControl(string arg)
        {
            bool result = false;
            Regex numericControl = new Regex("^(?=.*?[0-9]).{11,}$");
            if (numericControl.IsMatch(arg) == true)
            {
                result = true;
            }
            return result;
        }
        public bool PasswordControl(string input)
        {
            bool result = false;

            Regex PasswordControl = new Regex("^(?=.*?[A-ZÖÜİŞĞÇ])(?=.*?[a-zöüığşç])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9ÖöÜüŞşıİĞğÇç]).{8,}$");
            if (PasswordControl.IsMatch(input.ToString()) == true)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;

        }
    }
}
