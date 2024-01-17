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
    [Route("acadUtil/clientMachine")]
    [ApiController]
    public class AcadUtils : ControllerBase
    {
        private readonly DBmhtestContext _context;

        public AcadUtils(DBmhtestContext context)
        {
            _context = context;
        }


        /// <summary>
        /// GET: acadUtil/clientMachine
        /// </summary>
        /// <returns> una lista </returns>
        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<ClientMachine>>> GetClientMachine()
        {
            return await _context.ClientMachine.ToListAsync();
        }


        // GET: api/ClientMachines/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ClientMachine>> GetClientMachine(int id)
        {
            var clientMachine = await _context.ClientMachine.FindAsync(id);
            if (clientMachine == null)
            {
                return NotFound();
            }
            return clientMachine;
        }


        /// <summary>
        /// Recibir macAddress, validar licencia y devolver true o false
        /// </summary>
        /// <param name="hexMacAddress"></param>
        /// <returns> boolean </returns>
        [HttpGet]
        [Route("validateLicense")]
        public bool ValidateLicense(string hexMacAddress)
        {
            var placaRed = hexMacAddress;
            // Validar si el valor hexadecimal es válido para una dirección MAC
            bool isValid = IsValidMacAddress(hexMacAddress);

            // Devolver el resultado booleano
            return isValid;
        }


        /// <summary>
        /// Validar licencia
        /// </summary>
        /// <param name="hexMacAddress"></param>
        /// <returns> bool </returns>
        private bool IsValidMacAddress(string hexMacAddress)
        {
            // Lógica para validar si el valor hexadecimal es una dirección MAC válida
            // Puedes implementar tu propia lógica de validación aquí, por ejemplo, asegurándote de que tenga la longitud y el formato correctos.
            // Este es solo un ejemplo básico, puedes personalizarlo según tus necesidades.

            bool isValid = false;

            // Verificar si el valor tiene una longitud válida para una dirección MAC (por ejemplo, 12 caracteres)
            if (hexMacAddress.Length == 12)
            {
                // Puedes realizar más validaciones según el formato de direcciones MAC
                // En este ejemplo, simplemente asumiremos que cualquier cadena de 12 caracteres es válida
                isValid = true;
            }

            return isValid;
        }


        // PUT: api/ClientMachines/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Route("edit/{id}")]
        public async Task<IActionResult> PutClientMachine(int id, ClientMachine clientMachine)
        {
            if (id != clientMachine.IdClientMachine)
            {
                return BadRequest();
            }

            _context.Entry(clientMachine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientMachineExists(id))
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


        // POST: api/ClientMachines
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<ClientMachine>> PostClientMachine(ClientMachine clientMachine)
        {
            _context.ClientMachine.Add(clientMachine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientMachine", new { id = clientMachine.IdClientMachine }, clientMachine);
        }


        // DELETE: api/ClientMachines/5
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult<ClientMachine>> DeleteClientMachine(int id)
        {
            var clientMachine = await _context.ClientMachine.FindAsync(id);
            if (clientMachine == null)
            {
                return NotFound();
            }

            _context.ClientMachine.Remove(clientMachine);
            await _context.SaveChangesAsync();

            return clientMachine;
        }

        private bool ClientMachineExists(int id)
        {
            return _context.ClientMachine.Any(e => e.IdClientMachine == id);
        }
    }
}
