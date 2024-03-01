using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SprintMicroService.Data;
using SprintMicroService.Entites;
using SprintMicroService.Entities;
using SprintMicroService.Models.ModelPhase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SprintMicroService.Data.DataPhase
{
    public class PhaseRepository : IPhaseRepository
    {
        private readonly SprintContext context;
        private readonly IMapper mapper;

        public PhaseRepository(SprintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public List<Phase> GetAllPhases()
        {
            try
            {
                var obj = context.Phases.ToList();
                if (obj != null)
                {

                    return obj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;

            }
        }

        public Phase GetPhaseById(Guid phaseId)
        {
            var phase = context.Phases.FirstOrDefault(s => s.PhaseId == phaseId);

            if (phase == null)
            {
                // Dodajte odgovarajući tretman kada Sprint nije pronađen
                // Na primer, možete baciti izuzetak:
                throw new KeyNotFoundException($"Faza sa ID {phaseId} nije pronađen.");
            }

            return phase;
        }

        public PhaseConfirmationDto CreatePhase(Phase phase)
        {
            var createdEntity = context.Add(phase);
            return mapper.Map<PhaseConfirmationDto>(createdEntity.Entity);
        }
        public void UpdatePhase(Phase phase)
        {

        }
        public void DeletePhase(Guid phaseId)
        {
            var phase = GetPhaseById(phaseId);
            context.Remove(phase);
        }

        public List<Phase> GetPhaseBySprintId(Guid sprintId)
        {
            //var pha = GetPhaseBySprintId(sprintId);
            return (from phase in context.Phases
                    where phase.SprintId == sprintId
                    select phase)
                   .Include(p => p.Sprint)
                   .ToList();
        }


    }
}
