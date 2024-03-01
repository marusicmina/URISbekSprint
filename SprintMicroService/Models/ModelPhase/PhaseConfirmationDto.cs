using SprintMicroService.Entites;
using System;
using System.Collections.Generic;

namespace SprintMicroService.Models.ModelPhase
{
    public class PhaseConfirmationDto
    {
        public Guid PhaseId { get; set; }

        public string PhaseName { get; set; }

        public string PhaseDescription { get; set; }

        public DateTimeOffset? PhaseStartDate { get; set; }

        public DateTimeOffset? PhaseEndDate { get; set; }

        public string PhaseNotes { get; set; }

        public bool PhaseGoalsAchieved { get; set; }

        public Guid SprintId { get; set; }
    }
}
