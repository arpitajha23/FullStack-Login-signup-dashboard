using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Arpita
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
        public string Name { get; set; } 


        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(50, ErrorMessage = "Email can't be longer than 50 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [StringLength(12, ErrorMessage = "Phone number can't be longer than 13 characters.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Active status is required.")]
        public int IsActive { get; set; } = 1;



        public Arpita()
        {
            Name = "";
            Email = "";
            PhoneNumber = "";
            Password = "";
            Role = "";
        }
    }
}
