using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InlandMarinaClasses
{
    [Table("Customer")]
    public class Customer
    {
        public int ID { get; set; }


        [Required]
        [StringLength(30)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string LastName { get; set; } = string.Empty;

        [Required]
		[RegularExpression(@"^\(\d{3}\) \d{3}-\d{4}$", ErrorMessage = "Invalid phone format. Please use (000) 000-0000.")] // Adding regular expression to annotate phone format
		[StringLength(15)]
        public string Phone { get; set; } = string.Empty;
        // regular expression annotations

        [Required]
        [StringLength(30)]
        public string City { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please enter a valid username")]
        [MaxLength(30)]
        [UniqueUsername]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password must have a minimum of 10 characters and contain at least two of the following: uppercase letters, lowercase letters, numbers, and symbols.")]
        [MaxLength(30)]
        [PasswordValidation]
        public string Password { get; set; } = string.Empty;


        // Navigation property
        [Required]
        public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();

        public static List<Customer> GetSlips(InlandMarinaContext db)
        {
            List<Customer> customer = db.Customers.ToList();

            return customer;
        }
        
        // Password Validations
        public class PasswordValidationAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (value is string password)
                {
                    // Check if the password meets the specified criteria
                    // At least 10 characters, containing at least two of the following: uppercase letters, lowercase letters, numbers, and symbols.
                    var UPPERCASE = password.Count(char.IsUpper);
                    var lowercase = password.Count(char.IsLower);
                    var digits = password.Count(char.IsDigit);
                    var symbols = password.Count(c => !char.IsLetterOrDigit(c));

                    if (password.Length >= 10 &&
                        (UPPERCASE + lowercase >= 2) &&
                        (digits + symbols >= 2))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        // Username Validations
        public class UniqueUsernameAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is string username && validationContext.ObjectInstance is Customer customer)
                {
                    var dbContext = (InlandMarinaContext)validationContext.GetService(typeof(InlandMarinaContext));

                    // Check if the username is unique in the database
                    if (dbContext.Customers.Any(c => c.Username == username && c.ID != customer.ID))
                    {
                        return new ValidationResult("Username is already taken.");
                    }
                }

                return ValidationResult.Success;
            }
        }
    }
}
