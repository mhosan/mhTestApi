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


        /// <summary>
        /// Get traer todo el listado de tareas.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<Tarea>>> GetTarea()
        {
            return await _context.Tarea.ToListAsync();
        }


        /// <summary>
        /// Get por id de tarea
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Put: api/Tareas/edit/id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tarea"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("edit/{id}")]
        public async Task<IActionResult> PutTarea(int id, Tarea tarea)
        {
            if (id != tarea.IdTarea)
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


        /// <summary>
        /// POST: api/Tareas/add
        /// </summary>
        /// <param name="tarea"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<Tarea>> PostTarea(Tarea tarea)
        {
            _context.Tarea.Add(tarea);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTarea", new { id = tarea.IdTarea }, tarea);
        }


        /// <summary>
        /// DELETE: api/Tareas/delete/5 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Buscar tarea a ver si existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool TareaExists(int id)
        {
            return _context.Tarea.Any(e => e.IdTarea == id);
        }
    }
}
