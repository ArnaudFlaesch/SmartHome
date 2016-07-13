using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SmartHome.view
{
    public class ObjectValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "Le champ ne peux pas être vide.");
            else
            {
                if (value.ToString().Length < 3)
                    return new ValidationResult(false, "L'objet doit faire au minimum 3 charactères.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
