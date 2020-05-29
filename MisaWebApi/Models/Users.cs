using System.ComponentModel.DataAnnotations;

namespace MisaWebApi.Models
{
    public class Users
    {
        [Key]
        public int userId { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
