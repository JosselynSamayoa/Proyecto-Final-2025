using Microsoft.AspNetCore.Mvc;
using Proyecto_Final_Sistema_Bancario.Data;
using Proyecto_Final_Sistema_Bancario.Models;
using System;
using System.Collections.Generic;

namespace Proyecto_Final_Sistema_Bancario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarjetasController : ControllerBase
    {
        // DTO para consumo/pago
        public class MovimientoRequest
        {
            public decimal Monto { get; set; }
            public string Descripcion { get; set; }
            public string Categoria { get; set; }
        }

        // DTO para renovar
        public class RenovarRequest
        {
            // Por ejemplo, cuántos años quieres añadir
            public int Años { get; set; } = 1;
        }

        // DTO para cambio de PIN
        public class PinRequest
        {
            public string NuevoPin { get; set; }
        }

        // GET api/tarjetas
        [HttpGet]
        public ActionResult<IEnumerable<TarjetaDeCredito>> GetAll()
            => Ok(RepositorioTarjetas.Tarjetas.Values);

        // GET api/tarjetas/{numero}
        [HttpGet("{numeroTarjeta}")]
        public ActionResult<TarjetaDeCredito> Get(string numeroTarjeta)
        {
            if (!RepositorioTarjetas.Tarjetas.TryGetValue(numeroTarjeta, out var tarjeta))
                return NotFound($"Tarjeta {numeroTarjeta} no encontrada.");
            return Ok(tarjeta);
        }

        // GET api/tarjetas/{numero}/saldo
        [HttpGet("{numeroTarjeta}/saldo")]
        public ActionResult<decimal> GetSaldo(string numeroTarjeta)
        {
            if (!RepositorioTarjetas.Tarjetas.TryGetValue(numeroTarjeta, out var tarjeta))
                return NotFound($"Tarjeta {numeroTarjeta} no encontrada.");
            return Ok(tarjeta.SaldoActual);
        }

        // POST api/tarjetas/{numero}/consumos
        [HttpPost("{numeroTarjeta}/consumos")]
        public ActionResult Consumo(string numeroTarjeta,
                                    [FromBody] MovimientoRequest req)
        {
            if (!RepositorioTarjetas.Tarjetas.TryGetValue(numeroTarjeta, out var tarjeta))
                return NotFound($"Tarjeta {numeroTarjeta} no encontrada.");

            // Verificar límite
            if (tarjeta.SaldoActual + req.Monto > tarjeta.LimiteCredito)
                return BadRequest("Monto excede el límite de crédito.");

            // Actualizar saldo (aumenta el consumo)
            tarjeta.SaldoActual += req.Monto;

            // Registrar transacción
            var txn = new Transaccion(
                Guid.NewGuid().ToString(),
                numeroTarjeta,
                DateTime.Now,
                "Consumo",
                req.Monto,
                req.Descripcion,
                req.Categoria
            );
            RepositorioTransacciones.Transacciones.Add(txn);

            return CreatedAtAction(
                nameof(GetSaldo),
                new { numeroTarjeta },
                new { Saldo = tarjeta.SaldoActual }
            );
        }

        // POST api/tarjetas/{numero}/pagos
        [HttpPost("{numeroTarjeta}/pagos")]
        public ActionResult Pago(string numeroTarjeta,
                                 [FromBody] MovimientoRequest req)
        {
            if (!RepositorioTarjetas.Tarjetas.TryGetValue(numeroTarjeta, out var tarjeta))
                return NotFound($"Tarjeta {numeroTarjeta} no encontrada.");

            // El pago disminuye el saldo actual
            tarjeta.SaldoActual = Math.Max(0, tarjeta.SaldoActual - req.Monto);

            // Registrar transacción
            var txn = new Transaccion(
                Guid.NewGuid().ToString(),
                numeroTarjeta,
                DateTime.Now,
                "Pago",
                req.Monto,
                req.Descripcion,
                req.Categoria
            );
            RepositorioTransacciones.Transacciones.Add(txn);

            return CreatedAtAction(
                nameof(GetSaldo),
                new { numeroTarjeta },
                new { Saldo = tarjeta.SaldoActual }
            );
        }

        // POST api/tarjetas/{numeroTarjeta}/solicitar-renovacion
        [HttpPost("{numeroTarjeta}/solicitar-renovacion")]
        public ActionResult SolicitarRenovacion(string numeroTarjeta,
                                                [FromBody] RenovarRequest req)
        {
            if (!RepositorioTarjetas.Tarjetas.TryGetValue(numeroTarjeta, out var tarjeta))
                return NotFound();

            var g = new Gestion
            {
                GestionID = Guid.NewGuid().ToString(),
                NumeroTarjeta = numeroTarjeta,
                FechaSolicitud = DateTime.Now,
                TipoGestion = TipoGestion.Remision,   // o usa Renovacion si agregas
                Estado = EstadoGestion.Pendiente,
                AñosRenovacion = req.Años,
                Comentarios = $"Solicita +{req.Años} año(s)"
            };
            RepositorioGestiones.AddGestion(g);

            return CreatedAtAction(nameof(GestionesController.GetById),
                                   "Gestiones",
                                   new { id = g.GestionID },
                                   g);
        }

        // POST api/tarjetas/{numeroTarjeta}/solicitar-pin
        [HttpPost("{numeroTarjeta}/solicitar-pin")]
        public ActionResult SolicitarCambioPin(string numeroTarjeta,
                                               [FromBody] PinRequest req)
        {
            if (!RepositorioTarjetas.Tarjetas.TryGetValue(numeroTarjeta, out var tarjeta))
                return NotFound();

            var g = new Gestion
            {
                GestionID = Guid.NewGuid().ToString(),
                NumeroTarjeta = numeroTarjeta,
                FechaSolicitud = DateTime.Now,
                TipoGestion = TipoGestion.CambioPIN,
                Estado = EstadoGestion.Pendiente,
                NuevoPin = req.NuevoPin,
                Comentarios = "Solicita cambio de PIN"
            };
            RepositorioGestiones.AddGestion(g);

            return CreatedAtAction(nameof(GestionesController.GetById),
                                   "Gestiones",
                                   new { id = g.GestionID },
                                   g);
        }
    }
}