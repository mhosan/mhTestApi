using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mhTestApi.Models;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Linq;

namespace mhTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly DBmhtestContext _baseDatos;

        public TareasController(DBmhtestContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var listaTareas = await _baseDatos.Tarea.ToListAsync();
            return Ok(listaTareas);
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Agregar([FromBody] Tarea request) { 
            await _baseDatos.Tarea.AddAsync(request);
            await _baseDatos.SaveChangesAsync();
            return Ok(request);
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id) {
            var tareaEliminar = await _baseDatos.Tarea.FindAsync(id);
            if (tareaEliminar == null) return BadRequest("No existe la tarea");
            _baseDatos.Tarea.Remove(tareaEliminar);
            await _baseDatos.SaveChangesAsync();
            return Ok(tareaEliminar);
        }
    }
}
