using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrueHomeApplication.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public DateTime Disabled_at { get; set; }
        public string Status { get; set; }

    }
}
