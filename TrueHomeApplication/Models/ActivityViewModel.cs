using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrueHomeApplication.Models
{
    public class ActivityViewModel
    {
        public int Id { get; set; }

        public DateTime Schedule { get; set; }
        public string Title { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Status { get; set; }
        public string Condition { get; set; }
        public string TitleProperty { get; set; }
        public string AddressProperty { get; set; }

        public int Property_Id { get; set; }
        public virtual Property property { get; set; }
    }
}
