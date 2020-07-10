using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MisaWebApi.Models
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public string Role { get; set; }
        public string Id { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
