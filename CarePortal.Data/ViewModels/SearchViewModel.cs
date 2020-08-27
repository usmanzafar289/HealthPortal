using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarePortal.Data.ViewModels
{
    public class SearchViewModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string ImageURL { get; set; }
        public string SearchText { get; set; }
    }
}
