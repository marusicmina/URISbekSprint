using System;
using System.Collections.Generic;
using SprintMicroService.Entites;
using SprintMicroService.Models.ModelSprint;

namespace SprintMicroService.Data
{
    public interface ISprintRepository
    {
        List<Sprint> GetAllSprints();

        Sprint GetSprintById(Guid sprintId);

        void UpdateSprint(Sprint sprint);

        SprintConfirmationDto CreateSprint(Sprint sprint);

        void DeleteSprint(Guid sprintId);


        bool SaveChanges();
    }
}
