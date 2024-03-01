using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace SprintMicroService.Entites
{
    public class Phase
    {

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PhaseId { get; set; }

        [Required]
        [StringLength(255)]
        public string PhaseName { get; set; }


        [StringLength(500)]
        public string PhaseDescription { get; set; }

        [Required]
        public DateTimeOffset? PhaseStartDate { get; set; }

        [Required]
        public DateTimeOffset? PhaseEndDate { get; set; }


        [StringLength(500)]
        public string PhaseNotes { get; set; }

        [Required]
        public bool PhaseGoalsAchieved { get; set; }
        [Required]
        public Guid SprintId { get; set; }

        public virtual Sprint? Sprint { get; set; }
    }
}
