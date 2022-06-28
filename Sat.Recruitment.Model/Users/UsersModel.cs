using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Model.Users
{
    public class UsersModel
    {
        [Required (ErrorMessage = "The name is required")]
        public string Name { get; set; }

        [Required (ErrorMessage = "The email is required")]
        [EmailAddress (ErrorMessage = "The email format is invalid")]
        public string Email { get; set; }

        [Required (ErrorMessage = "The address is required")]
        public string Address { get; set; }

        [Required (ErrorMessage = "The phone is required")]
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal Money { get; set; }
    }
}
