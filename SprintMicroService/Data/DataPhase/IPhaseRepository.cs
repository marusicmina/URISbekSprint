
using System;
using System.Collections.Generic;
using SprintMicroService.Entites;
using SprintMicroService.Models.ModelPhase;


namespace SprintMicroService.Data.DataPhase
{
    public interface IPhaseRepository
    {
        List<Phase> GetAllPhases();

        Phase GetPhaseById(Guid phaseId);

        void UpdatePhase(Phase phase);

        PhaseConfirmationDto CreatePhase(Phase phase);
        void DeletePhase(Guid phaseId);


        bool SaveChanges();
        //vraca sve faze za odredjeni sprint

        List<Phase> GetPhaseBySprintId(Guid sprintId);
    }
}
