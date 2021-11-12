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
    public class ModifyActivityRequest 
    {
        [Required]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime? Schedule { get; set; }
      

        [DataMember]
        public string Status { get; set; }

    }

    public class ModifyActivityRequestValidator : AbstractValidator<ModifyActivityRequest>
    {
        public ModifyActivityRequestValidator()
        {
            RuleFor(a => a.Id).NotEmpty().WithMessage("Es necesario ingresar el Id de la actividad a modificar");
            RuleFor(a => a.Schedule).GreaterThan(DateTime.Today).WithMessage("Schedule debe ser mayor a la fecha actual");
        }
    }
}
