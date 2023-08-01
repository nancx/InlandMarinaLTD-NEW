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
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password must have a minimum of 10 characters and contain at least two of the following: uppercase letters, lowercase letters, numbers, and symbols.")]
        [MaxLength(30)]
        public string Password { get; set; } = string.Empty;

        // Navigation property
        [Required]
        public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();

        public static List<Customer> GetSlips(InlandMarinaContext db)
        {
            List<Customer> customer = db.Customers.ToList();

            return customer;
        }
        
        
    }
}
