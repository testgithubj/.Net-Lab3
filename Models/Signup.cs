using form_submission.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace form_submission.Models
{
    public class Signup
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z\s\.\-\/']{4,50}$",
         ErrorMessage = "Name must be 4 to 50 characters long and can only contain letters," +
            " spaces, '.', '-', and '/'")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[^\s\.]{4,12}$",
         ErrorMessage = "UserId must be 4 to 12 characters long and cannot contain spaces or dots.")]
        public string UserId { get; set; }
        [Required]
        [PasswordValidation(ErrorMessage = "Password must be at least" +
            " 8 characters with 1 uppercase, 2 lowercase in first, and 4 alphabets/special characters/numbers.")]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"^\s*[1-9][0-9]-[0-9][0-9][0-9][0-9][0-9]-[1-3]\s*$",
         ErrorMessage = "Must be in this format: XX-XXXXX-X")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailFormat(ErrorMessage = "Email should be in the format 'id@student.aiub.edu'")]
        public string Email { get; set; }

        [Required]
        [DateOfBirthValidation(ErrorMessage = "Should be at least 18 years old.")]
        public string Dob { get; set; }

    }

    public class PasswordValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || !(value is string))
            {
                return false;
            }

            string password = (string)value;

            if (password.Length < 8)
            {
                return false;
            }


            string alphabets = password.Substring(0, 4);
            if (Regex.Matches(alphabets, "[A-Z]").Count < 1 ||
                Regex.Matches(alphabets, "[a-z]").Count < 2)
            {
                return false;
            }


            string specialAndNumbers = password.Substring(4, 4);
            if (!Regex.IsMatch(specialAndNumbers, @"^[!@#$%^&*0-9a-zA-Z]+$"))
            {
                return false;
            }

            return true;
        }
    }
    public class DateOfBirthValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || !(value is string))
            {
                return false;
            }

            string dobString = (string)value;
            DateTime dob;

            if (!DateTime.TryParse(dobString, out dob))
            {
                return false; 
            }

          
            int age = DateTime.Now.Year - dob.Year;

            
            if (dob.AddYears(age) > DateTime.Now)
            {
                age--;
            }

            return age >= 18;
        }
    }

    public class EmailFormatAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string email = value.ToString();
                string id = validationContext.ObjectType.GetProperty("Id").GetValue(validationContext.ObjectInstance, null) as string;

                if (email == id + "@student.aiub.edu")
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Email should be in the format 'id@aiub.edu'");
                }
            }

            return ValidationResult.Success;
        }
    }

}


