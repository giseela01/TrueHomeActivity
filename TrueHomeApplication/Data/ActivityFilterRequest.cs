using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TrueHomeApplication.Models
{
    [ExcludeFromCodeCoverage]
    [DataContract(Namespace = "")]
    public class ActivityFiltersRequest 
    {

        [DataMember]
        public DateTime StartDate { get; set; }

        [Required]
        [DataMember]
        public DateTime EndDate { get; set; }

        [Required]
        [DataMember]
        public string Status { get; set; }

    }
}
