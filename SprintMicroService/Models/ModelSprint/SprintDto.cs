using System;

namespace SprintMicroService.Models
{
    /// <summary>
    /// Vrednosni objekat koji sadrži informacije o Sprintu.
    /// </summary>
    public class SprintDto
    {
        public string SprintId { get; set; }
        public string SprintName { get; set; }
        public string SprintGoals { get; set; }
        public string SprintDescription { get; set; }
        public DateTimeOffset? SprintStartDate { get; set; }
        public DateTimeOffset? SprintEndDate { get; set; }
        public string SuggestionsForNextSprint { get; set; }
        public string SprintNotes { get; set; }
        public bool GoalsAchieved { get; set; }
    }
}

