using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SmartHome.view
{
    class MailValidator : ValidationRule
    {

        private string _regex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "Un destinataire doit être renseigné.");
            else
            {
                if (!Regex.IsMatch(value.ToString(), _regex))
                    return new ValidationResult(false, "Saisir un mail sous la forme : xxx@yyy.zzz");
            }
            return ValidationResult.ValidResult;
        }
    }
}
