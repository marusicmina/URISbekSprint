
//nema potrebe da se potvrdjuje sprint
using SprintMicroService.Entites;
using System;
using System.Collections.Generic;

namespace SprintMicroService.Models.ModelSprint
{
    public class SprintConfirmationDto
    {
        public Guid SprintId { get; internal set; }
        public string SprintName { get; set; }
        public string SprintGoals { get; set; }
        public string SprintDescription { get; set; }
        public DateTimeOffset? SprintStartDate { get; set; }
        public DateTimeOffset? SprintEndDate { get; set; }
        public string SuggestionsForNextSprint { get; set; }
        public string SprintNotes { get; set; }
        public bool GoalsAchieved { get; set; }
        public virtual ICollection<Phase>? Phases { get; set; }
    }
}
