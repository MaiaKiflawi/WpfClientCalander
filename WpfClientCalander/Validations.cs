using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

    public class ValidationName : ValidationRule //first name, fist name, 
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
            try
            {
                string password = value.ToString();
                string symbols = "_@#$%&~-?!";
                bool lower = false, upper = false, digit = false, sym = false;
                if (password.Length < 6) // password too short
                    return new ValidationResult(false, "Too short");
                if (password.Length > 16) // password too long
                    return new ValidationResult(false, "Too long");
                for (int i = 0; i < password.Length; i++)
                {
                    if (!Char.IsLetterOrDigit(password[i]) && symbols.IndexOf(password[i]) == -1) //doesnt contain necessities
                        return new ValidationResult(false, "Password must contain letters, digits and " + symbols);
                    if (char.IsUpper(password[i])) upper = true;
                    if (char.IsLower(password[i])) lower = true;
                    if (char.IsDigit(password[i])) digit = true;
                    if (symbols.IndexOf(password[i]) != -1) sym = true;
                }
                if (!(upper && lower && digit && sym)) //doesnt contain necessities
                    throw new Exception("Password must contain atleast one capital letter, one lower letter, a number and a symbol");

            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Password not valid: " + ex.Message);
            }
            return ValidationResult.ValidResult;

        }
    }

    public class ValidationPhoneNumber : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string phone = value.ToString();
                if (!phone.Any(char.IsDigit))
                    return new ValidationResult(false, "Invalid format");
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Phone number not valid: " + ex.Message);
            }

            return ValidationResult.ValidResult;
        }
    }

    public class ValidationEmail : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string email = value.ToString();
                // Use a regular expression to validate the email format
                string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
                Regex regex = new Regex(emailPattern); //ביטוי רגולרי - יוצר תבנית שאפשר להשוות אליה
                if (!regex.IsMatch(email))
                    return new ValidationResult(false, "Invalid format");
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Email not valid: " + ex.Message);
            }

            return ValidationResult.ValidResult;

        }
    }

    public class ValidationBDate: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                DateTime bday = DateTime.Parse(value.ToString());
                if (bday>DateTime.Today.AddYears(-12))
                    return new ValidationResult(false, "too young");
                if (bday <DateTime.Today.AddYears(-120))
                    return new ValidationResult(false, "not possible");

            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Date not valid: " + ex.Message);
            }

            return ValidationResult.ValidResult;

        }
    }

}
