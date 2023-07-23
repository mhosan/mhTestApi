using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mhTestApi.Models;

namespace mhTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly DBmhtestContext _context;  //_baseDato para mi

        public TareasController(DBmhtestContext context)
        {
            _context = context;
        }

        // GET: api/Tareas/list
        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<Tarea>>> GetTarea()
        {
            return await _context.Tarea.ToListAsync();
        }

        // GET: api/Tareas/list/5
        [HttpGet]
        [Route("list/{id}")]
        public async Task<ActionResult<Tarea>> GetTarea(int id)
        {
            var tarea = await _context.Tarea.FindAsync(id);

            if (tarea == null)
            {
                return NotFound();
            }

            return tarea;
        }

        // PUT: api/Tareas/edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Route("edit/{id}")]
        public async Task<IActionResult> PutTarea(int id, Tarea tarea)
        {
            if (id != tarea.idTarea)
            {
                return BadRequest();
            }

            _context.Entry(tarea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TareaExists(id))
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

        // POST: api/Tareas/add
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<Tarea>> PostTarea(Tarea tarea)
        {
            _context.Tarea.Add(tarea);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTarea", new { id = tarea.idTarea }, tarea);
        }

        // DELETE: api/Tareas/delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult<Tarea>> DeleteTarea(int id)
        {
            var tarea = await _context.Tarea.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }

            _context.Tarea.Remove(tarea);
            await _context.SaveChangesAsync();

            return tarea;
        }

        private bool TareaExists(int id)
        {
            return _context.Tarea.Any(e => e.idTarea == id);
        }
    }
}
