using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SprintMicroService.Models.ModelSprint
{
    public class SprintCreationDto
    {
        /*[Required(ErrorMessage = "Id je obavezan")]
        public Guid SprintId { get; set; }*/

        [Required(ErrorMessage = "Naziv sprinta je obavezan")]
        public string SprintName { get; set; }


        [Required(ErrorMessage = "Ciljevi sprinta su obavezni")]
        public string SprintGoals { get; set; }


        [Required(ErrorMessage = "Opis sprinta je obavezan")]
        public string SprintDescription { get; set; }

        [Required(ErrorMessage = "Datum početka sprinta je obavezan")]
        public DateTimeOffset? SprintStartDate { get; set; }

        [Required(ErrorMessage = "Datum završetka sprinta je obavezan")]
        public DateTimeOffset? SprintEndDate { get; set; }

        [StringLength(1000, ErrorMessage = "Maksimalna dužina sugestija za sledeći sprint je 1000 karaktera")]
        public string SuggestionsForNextSprint { get; set; }

        /// <summary>
        /// Napomene o sprintu.
        /// </summary>
        [StringLength(500, ErrorMessage = "Maksimalna dužina napomena o sprintu je 500 karaktera")]
        public string SprintNotes { get; set; }

        /// <summary>
        /// Da li su svi ciljevi postignuti.
        /// </summary>
        [Required(ErrorMessage = "Polje GoalsAchieved je obavezno")]
        public bool GoalsAchieved { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (SprintStartDate > SprintEndDate)
            {
                yield return new ValidationResult(
                    "Datum početka sprinta ne može biti nakon datuma završetka sprinta.",
                    new[] { nameof(SprintStartDate), nameof(SprintEndDate) });
            }
        }
    }
}