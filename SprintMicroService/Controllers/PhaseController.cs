using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using SprintMicroService.Data;
using SprintMicroService.Data.DataPhase;
using SprintMicroService.Entites;
using SprintMicroService.Entities;
using SprintMicroService.Models;
using SprintMicroService.Models.ModelPhase;
using SprintMicroService.Models.ModelSprint;
using SprintMicroService.Services.Logger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhaseMicroService.Controllers
{
    [ApiController]
    [Route("api/phases")]
    [Produces("application/json", "application/xml")]
    //[Authorize]
    public class PhaseController : ControllerBase
    {
        private readonly IPhaseRepository phaseRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        public PhaseController(IPhaseRepository phaseRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.phaseRepository = phaseRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<PhaseDto>> GetPhases()
        {
            var phases = phaseRepository.GetAllPhases();

            if (phases == null || phases.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAllPhases", "Faza ne postoji.");

                return NoContent();
            }
            loggerService.Log(LogLevel.Information, "GetAllPhases", "Faza je uspesno pronadjena.");

            return Ok(mapper.Map<List<PhaseDto>>(phases));
        }

        [HttpGet("{phaseId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<PhaseDto> GetPhase(Guid phaseId)
        {
            try
            {
                Phase phase = phaseRepository.GetPhaseById(phaseId);

                loggerService.Log(LogLevel.Information, "GetPhaseById", "Faza je uspesno pronadjena");

                // Sprint je pronađen, vrati Ok rezultat
                return Ok(mapper.Map<PhaseDto>(phase));
            }
            catch (KeyNotFoundException ex)
            {
                loggerService.Log(LogLevel.Warning, "GetPhaseById", "Faza ne postoji.");

                // Sprint nije pronađen, vrati NotFound rezultat sa porukom
                return NotFound($"Faza nije pronađen. Detalji: {ex.Message}");
            }
        }


        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PhaseDto> UpdatePhase(PhaseUpdateDto phase)
        {
            try
            {
                var oldPhase = phaseRepository.GetPhaseById(phase.PhaseId);
                if (oldPhase == null)
                {
                    return NotFound($"Faza sa ID {phase.PhaseId} nije pronađena.");
                }

                Phase phaseEntity = mapper.Map<Phase>(phase);
                mapper.Map(phaseEntity, oldPhase);

                phaseRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "UpdatePhase", "Faza je uspesno izmenjena.");

                return Ok(mapper.Map<PhaseDto>(oldPhase));
            }
            catch (KeyNotFoundException ex)
            {
                loggerService.Log(LogLevel.Warning, "UpdatePhase", "Faza ne postoji");

                return NotFound($"Faza nije pronađena. Detalji: {ex.Message}");
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "UpdatePhase", "Faza " + phase.PhaseId + " ne moze da se promeni", ex);

                // Dodajte dodatne informacije o grešci kako biste olakšali dijagnostiku problema
                return StatusCode(StatusCodes.Status500InternalServerError, $"Greška prilikom ažuriranja faze. Detalji: {ex.Message}");
            }
        }


        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PhaseDto> CreatePhase([FromBody] PhaseCreationDto phase)
        {
            try
            {
                Phase phaseEntity = mapper.Map<Phase>(phase);
                var confirmation = phaseRepository.CreatePhase(phaseEntity);
                phaseRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetPhase", "Phase", new { phaseId = confirmation.PhaseId });

                loggerService.Log(LogLevel.Information, "CreatePhase", "Faza je uspesno kreirana.");

                return Created(location, mapper.Map<PhaseConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                loggerService.Log(LogLevel.Error, "CreatePhase", "Faza ne moze da se kreira.", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }

        }


        [HttpDelete("{phaseId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePhase(Guid phaseId)
        {
            try
            {
                var phase = phaseRepository.GetPhaseById(phaseId);

                if (phase == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeletePhase", "Faza ne postoji.");

                    return NotFound();
                }

                phaseRepository.DeletePhase(phaseId);
                phaseRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeletePhase", "Faza je uspesno obrisana.");

                return NoContent();
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "DeletePhase", "faza " + phaseId + " ne moze da se obrise.", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpGet("sprint/{sprintId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<PhaseDto>> GetPhaseBySprintId(Guid sprintId)
        {
            try
            {
                var phases = phaseRepository.GetPhaseBySprintId(sprintId);

                if (phases == null || phases.Count == 0)
                {
                    loggerService.Log(LogLevel.Warning, "GetPhaseBySprintId", "faza ne postoji.");

                    // Sprint nije pronađen, vrati NotFound rezultat sa odgovarajućom porukom
                    return NotFound($"Faze nisu pronađene za SprintId: {sprintId}");
                }

                loggerService.Log(LogLevel.Information, "GetPhaseBySprintId", "faza je uspesno pronadjena.");

                // Faze su pronađene, vrati Ok rezultat sa mapiranim fazama
                return Ok(phases.Select(phase => mapper.Map<PhaseDto>(phase)).ToList());
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Information, "GetPhaseBySprintId", "greska");

                // Drugi tipovi grešaka koje želite obraditi, npr. baza podataka, neka se obrade ovde
                return StatusCode(StatusCodes.Status500InternalServerError, $"Greška prilikom obrade zahteva: {ex.Message}");
            }
        }




    }
}
