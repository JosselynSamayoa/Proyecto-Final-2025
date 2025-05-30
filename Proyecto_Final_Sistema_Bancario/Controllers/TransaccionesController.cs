using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Proyecto_Final_Sistema_Bancario.Data;
using Proyecto_Final_Sistema_Bancario.Models;

namespace Proyecto_Final_Sistema_Bancario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransaccionesController : ControllerBase
    {
        // GET api/transacciones
        [HttpGet]
        public ActionResult<IEnumerable<Transaccion>> GetAll()
        {
            return Ok(RepositorioTransacciones.Transacciones);
        }

        // GET api/transacciones/{id}
        [HttpGet("{id}")]
        public ActionResult<Transaccion> GetById(string id)
        {
            var txn = RepositorioTransacciones
                          .Transacciones
                          .FirstOrDefault(t => t.TransaccionID == id);
            if (txn == null)
                return NotFound($"Transacción con ID = {id} no encontrada.");
            return Ok(txn);
        }

        // GET api/transacciones/tarjeta/{numeroTarjeta}
        [HttpGet("tarjeta/{numeroTarjeta}")]
        public ActionResult<IEnumerable<Transaccion>> GetByTarjeta(string numeroTarjeta)
        {
            var lista = RepositorioTransacciones
                            .Transacciones
                            .Where(t => t.NumeroTarjeta == numeroTarjeta)
                            .ToList();
            return Ok(lista);
        }
    }
}