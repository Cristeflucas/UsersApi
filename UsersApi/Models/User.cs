using Microsoft.AspNetCore.Identity;

namespace UsersApi.Models
{
    public class User : IdentityUser
    {
        public DateTime BirthdayDate { get; set; }
        public User() : base()
        {
            
        }
    }
}
