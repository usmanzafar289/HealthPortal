using CarePortal.Data.Models;
using System.Collections.Generic;

namespace CarePortal.Data.ViewModels
{
    public class DoctorsViewModel
    {
        public List<ApplicationUserListViewModel> UserList { get; set; }
        public int Status { get; set; }
        public string Email { get; set; }
    }
}
