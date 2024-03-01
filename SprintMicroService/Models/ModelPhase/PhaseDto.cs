using System;

namespace SprintMicroService.Models.ModelPhase
{
    public class PhaseDto
    {

        public string phaseId { get; set; }

    public string PhaseName { get; set; }

        public string PhaseDescription { get; set; }

        public DateTimeOffset? PhaseStartDate { get; set; }

        public DateTimeOffset? PhaseEndDate { get; set; }

        public string PhaseNotes { get; set; }

        public bool PhaseGoalsAchieved { get; set; }

        public Guid SprintId { get; set; }
    }
}
