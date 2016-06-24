using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationUserCreateViewModel
    {
        public enum Account
        {
            Mod,
            Admin
        }

        [Required]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi zawierać conamniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasła się różnią")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Typ użytkownika")]
        [Required]
        public string Type { get; set; }
    }

    public class ApplicationUserIndexViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Typ konta")]
        public string Roles { get; set; }

        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}