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

namespace UniquenessCheckELMA.Application.Controllers
{
    [Authorize, ApiController, Route("[controller]/[action]")]
    public class PhysicalPersonController : ControllerBase
    {
        private readonly DataContext _context;

        public PhysicalPersonController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/PhysicalPerson
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        /*public*/ async Task<ActionResult<IEnumerable<PhysicalPersonDTO>>> GetPhysicalPersons()
        {
            return await _context.PhysicalPersons.Select(u => (PhysicalPersonDTO)u).ToListAsync();
        }

        /// <summary>
        /// GET: api/PhysicalPerson/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpGet("{id}")]
        /*public*/ async Task<ActionResult<PhysicalPersonDTO>> GetPhysicalPerson(long id)
        {
            var entity = await _context.PhysicalPersons.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return (PhysicalPersonDTO)entity;
        }

        /// <summary>
        /// PUT: api/PhysicalPerson/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        //[HttpPut("{id}")]
        /*public*/ async Task<IActionResult> PutPhysicalPerson(long id, PhysicalPersonDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            _context.Entry((PhysicalPerson)dto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhysicalPersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// POST: api/PhysicalPerson
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        //[HttpPost]
        /*public*/ async Task<ActionResult<PhysicalPersonDTO>> PostPhysicalPerson(PhysicalPersonDTO dto)
        {
            _context.PhysicalPersons.Add(dto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhysicalPerson", new { id = dto.Id }, dto);
        }

        /// <summary>
        /// DELETE: api/PhysicalPerson/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpDelete("{id}")]
        /*public*/ async Task<IActionResult> DeletePhysicalPerson(long id)
        {
            var entity = await _context.PhysicalPersons.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.PhysicalPersons.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhysicalPersonExists(long id)
        {
            return _context.PhysicalPersons.Any(e => e.Id == id);
        }



        /// <summary>
        /// Uniqueness - Основной метод, для проверки клиента в процессах
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Uniqueness(long id)
        {
            var entity = await _context.PhysicalPersons
                .Include(p => p.Applications)
                .SingleOrDefaultAsync(p => p.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity.Applications.Select(x => (ApplicationDTO)x));
        }

    }
}
