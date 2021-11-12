using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using FluentValidation;
using TrueHomeApplication.Models.Enum;

namespace TrueHomeApplication.Models
{
    [ExcludeFromCodeCoverage]
    [DataContract(Namespace = "")]
    public class ActivityRequest 
    {     

        [Required]
        [DataMember]
        public DateTime Schedule { get; set; }

        [Required]
        [DataMember]
        public string Title { get; set; }

        [Required]
        [DataMember]
        public string Status { get; set; }

        [Required]
        [DataMember]
        public int Property_Id { get; set; }

    }

    public class ActivityRequestValidator : AbstractValidator<ActivityRequest>
    {
        public ActivityRequestValidator()
        {
            RuleFor(a => a.Schedule).GreaterThan(DateTime.Today).WithMessage("Schedule debe ser mayor a la fecha actual");
        }
    }
}
