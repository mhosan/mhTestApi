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
        /// Este get no recibe parametros.
        /// GET: acadUtil/clientMachine/list
        /// </summary>
        /// <returns> una lista con los equipos ciente </returns>
        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<ClientMachine>>> GetClientMachine()
        {
            return await _context.ClientMachine.ToListAsync();
        }


        /// <summary>
        /// Este get recibe un parametro (No es query param)
        /// GET: acadUtil/ClientMachines/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ClientMachine</returns>
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
        /// Este get recibe un query param.
        /// GET: acadUtil/ClientMachine/validateLicense?hexMacAddress=876969786
        /// Recibir macAddress, validar licencia y devolver true o false
        /// </summary>
        /// <param name="hexMacAddress"></param>
        /// <returns> boolean </returns>
        [HttpGet]
        [Route("validateLicense")]
        public bool ValidateLicense(string hexMacAddress)
        {
            var placaRed = hexMacAddress;
            // Unica validación de licencia por ahora: Validar si el valor hexadecimal es válido para una dirección MAC
            bool isValid = IsValidMacAddress(hexMacAddress);

            // Devolver el resultado booleano
            return isValid;
        }


        /// <summary>
        /// ================
        /// Validar licencia
        /// ================
        /// </summary>
        /// <param name="hexMacAddress"></param>
        /// <returns> bool </returns>
        private bool IsValidMacAddress(string hexMacAddress)
        {
            // Lógica para validar si la licencia es válida
            bool isValid = false;

            // Verificar si el valor tiene una longitud válida para una dirección MAC: 12 caracteres
            if (hexMacAddress.Length == 12)
            {
                isValid = true;
            }
            return isValid;
        }


        /// <summary>
        /// PUT: acadUtil/ClientMachine/edit/4
        /// </summary>
        /// <param name="id">recibe este parametro en la url</param>
        /// en el body:
        /// {
        ///     "idClientMachine": 4,
        ///     "macAddress": "45087599038"
        /// }
        /// <returns>ClientMachine actualizado</returns>
        [HttpPut]
        [Route("edit/{id}")]
        public async Task<ActionResult<ClientMachine>> PutClientMachine(int id, ClientMachine clientMachine)
        {
            if (id != clientMachine.IdClientMachine)
            {
                return BadRequest("El ID proporcionado no coincide con un ID de ClientMachine.");
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
                    return NotFound("No se encontró el ClientMachine con el ID proporcionado.");
                }
                else
                {
                    return StatusCode(500, "Se produjo un error al intentar actualizar el ClientMachine.");
                }
            }
            return clientMachine;
        }


        /// <summary>
        /// POST: /acadUtil/clientMachine/add
        /// </summary>
        /// <param name=un objeto "ClientMachine" sin el id y sin la fecha.></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<ClientMachine>> PostClientMachine(ClientMachine clientMachine)
        {
            try
            {
                _context.ClientMachine.Add(clientMachine);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetClientMachine", new { id = clientMachine.IdClientMachine }, clientMachine);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Se produjo un error al intentar agregar el ClientMachine: {ex.Message}");
            }
        }


        /// <summary>
        /// DELETE: acadUtil/clientMachine/delete/7
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult<ClientMachine>> DeleteClientMachine(int id)
        {
            var clientMachine = await _context.ClientMachine.FindAsync(id);
            if (clientMachine == null)
            {
                return NotFound("No se encontró el ClientMachine con el ID proporcionado.");
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
