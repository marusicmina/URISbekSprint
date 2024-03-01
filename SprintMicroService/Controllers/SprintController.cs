using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using SprintMicroService.Data;
using SprintMicroService.Entites;
using SprintMicroService.Entities;
using SprintMicroService.Models;
using SprintMicroService.Models.ModelSprint;
using SprintMicroService.Services.Logger;
using System;
using System.Collections.Generic;

namespace SprintMicroService.Controllers
{
    [ApiController]
    [Route("api/sprint")]
    [Produces("application/json", "application/xml")]
    //[Authorize]
    public class SprintController : ControllerBase
    {
        private readonly ISprintRepository sprintRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        public SprintController(ISprintRepository sprintRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.sprintRepository = sprintRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<SprintDto>> GetSprints()
        {
            var sprints = sprintRepository.GetAllSprints();

            if (sprints == null || sprints.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAllSprints", "Sprintovi nisu pronadjeni.");

                return NoContent();
            }
            loggerService.Log(LogLevel.Information, "GetAllSprints", "Sprintovi su pronadjeni");

            return Ok(mapper.Map<List<SprintDto>>(sprints));
        }

        [HttpGet("{sprintId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<SprintDto> GetSprint(Guid sprintId)
        {
            try
            {
                Sprint sprint = sprintRepository.GetSprintById(sprintId);
                loggerService.Log(LogLevel.Information, "GetSprintById", "Sprint je uspešno vraćen");

                // Sprint je pronađen, vrati Ok rezultat
                return Ok(mapper.Map<SprintDto>(sprint));
            }
            catch (KeyNotFoundException ex)
            {
                loggerService.Log(LogLevel.Warning, "GetSprintById", "Sprint ne postoji");

                // Sprint nije pronađen, vrati NotFound rezultat sa porukom
                return NotFound($"Sprint nije pronađen. Detalji: {ex.Message}");
            }
        }


        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<SprintDto> UpdateSprint(SprintUpdateDto sprint)
        {
            try
            {
                var oldSprint = sprintRepository.GetSprintById(sprint.SprintId);
                if (oldSprint == null)
                {
                    loggerService.Log(LogLevel.Warning, "UpdateSprint", "Sprint ne postoji");

                    return NotFound($"Sprint sa ID {sprint.SprintId} nije pronađen.");
                }

                Sprint sprintEntity = mapper.Map<Sprint>(sprint);
                mapper.Map(sprintEntity, oldSprint);
                loggerService.Log(LogLevel.Information, "UpdateSprint", "Faza je uspešno izmenjena");

                sprintRepository.SaveChanges();
                return Ok(mapper.Map<SprintDto>(oldSprint));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Sprint nije pronađen. Detalji: {ex.Message}");
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "UpdateSprint", "Sprint " + sprint.SprintId + " ne moze da se izmeni.", ex);

                // Dodajte dodatne informacije o grešci kako biste olakšali dijagnostiku problema
                return StatusCode(StatusCodes.Status500InternalServerError, $"Greška prilikom ažuriranja sprinta. Detalji: {ex.Message}");
            }
        }
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<SprintDto> CreateSprint([FromBody] SprintCreationDto sprint)
        {
            try
            {
                Sprint sprintEntity = mapper.Map<Sprint>(sprint);
                var confirmation = sprintRepository.CreateSprint(sprintEntity);
                sprintRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetSprint", "Sprint", new { sprintId = confirmation.SprintId });
                loggerService.Log(LogLevel.Information, "CreateSprint", "Sprint je uspesno kreiran.");

                return Created(location, mapper.Map<SprintConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                loggerService.Log(LogLevel.Error, "CreateSprint", "Sprint ne moze da se kreira.", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }




        [HttpDelete("{sprintId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteSprint(Guid sprintId)
        {
            try
            {
                var sprint = sprintRepository.GetSprintById(sprintId);

                if (sprint == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteSprint", "Sprint ne postoji");

                    return NotFound();
                }

                sprintRepository.DeleteSprint(sprintId);
                sprintRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeleteSprint", "Sprint je uspesno obrisan");

                return NoContent();
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "DeleteSprint", "Sprint " + sprintId + " nw mozw da se obrise", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }


    }
}