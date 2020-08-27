using CarePortal.Data.Models;
using System.Collections.Generic;

namespace CarePortal.Data.ViewModels
{
    public class ProfileViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }

        public bool canUpdate { get; set; }

        public string base64Picture { get; set; }

        public List<Department> listDepartment { get; set; }
        public List<Department> UserSelectedDepartments { get; set; }
        public int[] UserSelectedDepartmentsIds { get; set; }
    }
}
