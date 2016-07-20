using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SmartHome
{
    public class SeuilValidator : ValidationRule
    {
        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            if (value.GetType() != typeof(float) || value.GetType() != typeof(int))
            {
                result = new ValidationResult(false, this.ErrorMessage);
            }
            return result;
        }
    }
}
