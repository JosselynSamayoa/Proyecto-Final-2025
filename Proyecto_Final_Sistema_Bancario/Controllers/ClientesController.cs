using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Proyecto_Final_Sistema_Bancario.Models;
using Proyecto_Final_Sistema_Bancario.Data;

namespace Proyecto_Final_Sistema_Bancario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        // GET api/clientes
        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> GetAll()
        {
            return Ok(RepositorioClientes.Clientes.Values);
        }

        // GET api/clientes/{id}
        [HttpGet("{id}")]
        public ActionResult<Cliente> Get(string id)
        {
            if (RepositorioClientes.Clientes.TryGetValue(id, out var cliente))
                return Ok(cliente);
            return NotFound($"Cliente con ID = {id} no encontrado.");
        }

        // POST api/clientes
        [HttpPost]
        public ActionResult<Cliente> Create([FromBody] Cliente cliente)
        {
            if (RepositorioClientes.Clientes.ContainsKey(cliente.ClienteID))
                return Conflict($"Ya existe un cliente con ID = {cliente.ClienteID}.");

            RepositorioClientes.Clientes[cliente.ClienteID] = cliente;
            return CreatedAtAction(nameof(Get), new { id = cliente.ClienteID }, cliente);
        }

        // PUT api/clientes/{id}
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Cliente cliente)
        {
            if (id != cliente.ClienteID)
                return BadRequest("El ID de la URL no coincide con el del cuerpo de la petición.");

            if (!RepositorioClientes.Clientes.ContainsKey(id))
                return NotFound($"Cliente con ID = {id} no encontrado.");

            RepositorioClientes.Clientes[id] = cliente;
            return NoContent();
        }

        // DELETE api/clientes/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (!RepositorioClientes.Clientes.Remove(id))
                return NotFound($"Cliente con ID = {id} no encontrado.");

            return NoContent();
        }
    }
}