using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

using UniquenessCheckELMA.Application.DTOs;
using UniquenessCheckELMA.Domain.Entities;
using UniquenessCheckELMA.Infrastructure;

namespace UniquenessCheckELMA.Application.Controllers
{
    [Authorize, ApiController, Route("[controller]/[action]")]
    public class ProcessInstanceController : ControllerBase
    {
        private readonly DataContext _context;

        public ProcessInstanceController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/ProcessInstance
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        /*public*/ async Task<ActionResult<IEnumerable<ProcessInstanceDTO>>> GetProcessInstances()
        {
            return await _context.ProcessInstances.Select(p => (ProcessInstanceDTO)p).ToListAsync();
        }

        /// <summary>
        /// Метод для получения информации о кредите.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessInstanceDTO>> GetProcessInstance(long id)
        {
            var entity = await _context.ProcessInstances.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return (ProcessInstanceDTO)entity;
        }

        /// <summary>
        /// Метод для изменения статуса кредита
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcessInstance(long id, ProcessInstanceDTO dto)
        {
            var entity = await _context.ProcessInstances.FindAsync(id);
            if (entity is null)
                return NotFound();

            entity.IsActive = dto.IsActive;

            if(_context.TrySaveChangesAsync(this, out var result))
                return result;

            return NoContent();
        }

        /// <summary>
        /// Метод для создания нового кредита
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ProcessInstanceDTO>> PostProcessInstance(ProcessInstanceDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Name is required");
            _context.ProcessInstances.Add(dto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProcessInstance", new { id = dto.Id }, dto);
        }

        /// <summary>
        /// DELETE: api/ProcessInstance/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpDelete("{id}")]
        /*public*/ async Task<IActionResult> DeleteProcessInstance(long id)
        {
            var entity = await _context.ProcessInstances.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.ProcessInstances.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProcessInstanceExists(long id)
        {
            return _context.ProcessInstances.Any(e => e.Id == id);
        }
    }
}
