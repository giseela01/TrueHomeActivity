using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrueHomeApplication.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public int Activity_Id { get; set; }
        public string Answer { get; set; }
        public DateTime Created_at { get; set; }
    }
}
