using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        
        [Required]
        [MinLength(8,ErrorMessage ="Password must be at least 8 characters")]
        public string password { get; set; }
    }
}
