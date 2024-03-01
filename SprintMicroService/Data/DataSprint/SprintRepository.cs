using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SprintMicroService.Data;
using SprintMicroService.Entites;
using SprintMicroService.Entities;
using SprintMicroService.Models.ModelSprint;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SprintMicroService.Data
{
    public class SprintRepository : ISprintRepository
    {
        private readonly SprintContext context;
        private readonly IMapper mapper;

        public SprintRepository(SprintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public List<Sprint> GetAllSprints()
        {
            try
            {
                var obj = context.Sprints.ToList();
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

        public Sprint GetSprintById(Guid sprintId)
        {
            var sprint = context.Sprints.FirstOrDefault(s => s.SprintId == sprintId);

            if (sprint == null)
            {
                // Dodajte odgovarajući tretman kada Sprint nije pronađen
                // Na primer, možete baciti izuzetak:
                throw new KeyNotFoundException($"Sprint sa ID {sprintId} nije pronađen.");
            }

            return sprint;
        }
        public SprintConfirmationDto CreateSprint(Sprint sprint)
        {
            var createdEntity = context.Add(sprint);
            return mapper.Map<SprintConfirmationDto>(createdEntity.Entity);
        }

        public void UpdateSprint(Sprint sprint)
        {
            // Nije potrebna implementacija jer EF Core prati entitet koji smo izvukli iz baze
            // i kada promenimo taj objekat i odradimo SaveChanges, sve izmene će biti perzistirane
        }
        public void DeleteSprint(Guid sprintId)
        {
            var sprint = GetSprintById(sprintId);
            context.Remove(sprint);
        }


    }
}
