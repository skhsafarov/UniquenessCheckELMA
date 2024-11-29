using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using UniquenessCheckELMA.Application.DTOs;
using UniquenessCheckELMA.Domain.Entities;
using UniquenessCheckELMA.Infrastructure;

using ApplicationEntity = UniquenessCheckELMA.Domain.Entities.Application;

namespace UniquenessCheckELMA.Application.Controllers
{
    [Authorize, ApiController, Route("[controller]/[action]")]
    public class ApplicationController : ControllerBase
    {
        private readonly DataContext _context;

        public ApplicationController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/Application
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        /*public*/
        async Task<ActionResult<IEnumerable<ApplicationDTO>>> GetApplications()
        {
            return await _context.Applications.Select(a => (ApplicationDTO)a).ToListAsync();
        }

        /// <summary>
        /// Метод для получения информации о статусе заявки
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationDTO>> GetApplication(long id)
        {
            var entity = await _context.Applications.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return (ApplicationDTO)entity;
        }

        /// <summary>
        /// Метод для изменения статуса заявки
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication(long id, ApplicationDTO dto)
        {
            var entity = await _context.Applications.FindAsync(id);
            if (entity is null)
                return NotFound();
            entity.Status = dto.Status;
            entity.IsActive = dto.IsActive;

            if (_context.TrySaveChangesAsync(this, out var result))
                return result;

            return NoContent();
        }

        /// <summary>
        /// Метод для создания заявки
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostApplication(ApplicationDTO dto)
        {
            //if (!(dto.PhysicalPersonId > 9999999999999) && (dto.ProcessInstanceId < 100000000000000))
            //    return BadRequest("Охуевший?");
            var physicalPerson = await _context.PhysicalPersons.FindAsync(dto.PhysicalPersonId);
            var processInstance = await _context.ProcessInstances.FindAsync(dto.ProcessInstanceId);
            var entity = (ApplicationEntity)dto;
            if (physicalPerson is null)
            {
                entity.PhysicalPerson = new() { Id = dto.PhysicalPersonId };
            }

            if (processInstance is null)
                entity.ProcessInstance = new() { Id = dto.ProcessInstanceId };

            _context.Applications.Add(entity);
            if (!_context.TrySaveChangesAsync(this, out var result))
                return result;
            return CreatedAtAction("GetApplication", new { id = dto.Id }, entity);
        }

        /// <summary>
        /// DELETE: api/Application/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpDelete("{id}")]
        /*public*/
        async Task<IActionResult> DeleteApplication(long id)
        {
            var entity = await _context.Applications.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Applications.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplicationExists(long id)
        {
            return _context.Applications.Any(e => e.Id == id);
        }
    }
}
