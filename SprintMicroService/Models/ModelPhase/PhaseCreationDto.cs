using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SprintMicroService.Models.ModelPhase
{
    public class PhaseCreationDto
    {

        [Required(ErrorMessage = "Naziv faze je obavezan")]
        public string PhaseName { get; set; }


        [StringLength(500, ErrorMessage = "Maksimalna dužina napomena o sprintu je 500 karaktera")]
        public string PhaseDescription { get; set; }


        [Required(ErrorMessage = "Datum pocetka faze je obavezan")]
        public DateTimeOffset? PhaseStartDate { get; set; }


        [Required(ErrorMessage = "Naziv kraja faze je obavezan")]
        public DateTimeOffset? PhaseEndDate { get; set; }


        [StringLength(500, ErrorMessage = "Maksimalna dužina napomena o sprintu je 500 karaktera")]
        public string PhaseNotes { get; set; }


        [Required(ErrorMessage = "Obavezno je napisati da li je faza bila uspesna")]
        public bool PhaseGoalsAchieved { get; set; }

       [Required(ErrorMessage = "Obavezno je napisati kom sprintu pripada ")]
        public Guid SprintId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PhaseStartDate > PhaseEndDate)
            {
                yield return new ValidationResult(
                    "Datum početka faze ne može biti nakon datuma završetka faze.",
                    new[] { nameof(PhaseStartDate), nameof(PhaseEndDate) });
            }
        }

    }
}
