using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfClientCalander
{
    public class ValidationUserName : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string username = value.ToString();
                if (username.Length < 6) // username too short
                    return new ValidationResult(false, "Too short");
                if (username.Length > 15) // username too long
                    return new ValidationResult(false, "Too long");
                if (!username.Any(char.IsDigit)) // must contain at least one number
                    return new ValidationResult(false, "Must contain at least one number");
                foreach (char c in username) //can only contain letters, numbers, peried and underscore
                    if (!Char.IsLetterOrDigit(c) && c != '_' && c != '.')
                        return new ValidationResult(false, "Can only contain letters, numbers, peried and underscore");
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Username not valid: " + ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }

    public class ValidationName : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string name = value.ToString();
                if (name.Length < 2) // name too short
                    return new ValidationResult(false, "Too short");
                if (name.Length > 15) // name too long
                    return new ValidationResult(false, "Too long");
                foreach (char c in name)
                    if (!Char.IsLetter(c) && c != ' ') //name only has letters ir spaces
                        return new ValidationResult(false, "Only letters and spaces");
                if (!Char.IsUpper(name[0])) //first letter not capital
                    return new ValidationResult(false, "Name must start with capital letter");
                if (!Char.IsLetter(name[name.Length - 1])) //can't end with space
                    return new ValidationResult(false, "Name can't end with space");
                if (name.IndexOf("  ") != -1) //can't include double space
                    return new ValidationResult(false, "First name must not include more than one space at a time");
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Name not valid: " + ex.Message);
            }
            return ValidationResult.ValidResult;
        }

    }

    public class ValidationPassword : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }

    public class ValidationEmail : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }

}
