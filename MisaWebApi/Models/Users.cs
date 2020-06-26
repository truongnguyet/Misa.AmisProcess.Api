using System;
using System.Collections.Generic;

namespace MisaWebApi.Models
{
    public partial class Users
    {
        public Users()
        {
            UsersHasProcess = new HashSet<UsersHasProcess>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public string Role { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }

        public virtual ICollection<UsersHasProcess> UsersHasProcess { get; set; }
    }
}
