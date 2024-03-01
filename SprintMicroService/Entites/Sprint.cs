using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace SprintMicroService.Entites
{
    public class Sprint
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SprintId { get; set; }

        [Required]
        [StringLength(255)]
        public string SprintName { get; set; }

        [Required]
        [StringLength(500)]
        public string SprintGoals { get; set; }

        [Required]
        [StringLength(255)]
        public string SprintDescription { get; set; }

        [Required]
        public DateTimeOffset? SprintStartDate { get; set; }
        [Required]
        public DateTimeOffset? SprintEndDate { get; set; }

        [StringLength(1000)]
        public string SuggestionsForNextSprint { get; set; }

        [StringLength(500)]
        public string SprintNotes { get; set; }

        [Required]
        public bool GoalsAchieved { get; set; }

        public virtual ICollection<Phase>? Phases { get; set; }

    }
}