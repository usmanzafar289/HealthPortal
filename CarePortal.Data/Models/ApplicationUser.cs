using System;
using Microsoft.AspNetCore.Identity;

namespace CarePortal.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IsApproved { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
    }

    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IsApproved { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedEmail { get; set; }
        public string Email { get; set; }
        public string NormalizedUserName { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public string Role { get; set; }

        public UserModel Set(ApplicationUser applicationUser, string role)
        {
            UserModel userModel = new UserModel();

            userModel.FirstName = applicationUser.FirstName;
            userModel.LastName = applicationUser.LastName;
            userModel.IsApproved = applicationUser.IsApproved;
            userModel.Picture = applicationUser.Picture;
            userModel.Description = applicationUser.Description;

            userModel.Id = applicationUser.Id;
            userModel.UserName = applicationUser.UserName;
            userModel.NormalizedEmail = applicationUser.NormalizedEmail;
            userModel.Email = applicationUser.Email;
            userModel.NormalizedUserName = applicationUser.NormalizedUserName;
            userModel.EmailConfirmed = applicationUser.EmailConfirmed;
            userModel.PasswordHash = applicationUser.PasswordHash;
            userModel.SecurityStamp = applicationUser.SecurityStamp;
            userModel.ConcurrencyStamp = applicationUser.ConcurrencyStamp;
            userModel.PhoneNumber = applicationUser.PhoneNumber;
            userModel.PhoneNumberConfirmed = applicationUser.PhoneNumberConfirmed;
            userModel.TwoFactorEnabled = applicationUser.TwoFactorEnabled;
            userModel.LockoutEnd = applicationUser.LockoutEnd;
            userModel.LockoutEnabled = applicationUser.LockoutEnabled;
            userModel.AccessFailedCount = applicationUser.AccessFailedCount;

            userModel.Role = role;

            return userModel;
        }
    }
}
