using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrueHomeApplication.Models
{
    public class Survey
    {
        public int Id { get; set; }

        [ForeignKey("Activity_Id")]
        public virtual Activity activity { get; set; }
        public string Answer { get; set; }
        public DateTime Created_at { get; set; }
    }
}
