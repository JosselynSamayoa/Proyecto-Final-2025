using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Proyecto_Final_Sistema_Bancario.Data;
using Proyecto_Final_Sistema_Bancario.Models;

namespace Proyecto_Final_Sistema_Bancario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GestionesController : ControllerBase
    {
        // DTO para crear gestión
        public class NuevaGestionRequest
        {
            public string NumeroTarjeta { get; set; }
            public TipoGestion TipoGestion { get; set; }
            public string Comentarios { get; set; }
            public int AniosRenovacion { get; set; }   // solo si TipoGestion == Remision
            public string NuevoPin { get; set; }   // solo si TipoGestion == CambioPIN
        }

        // DTO para actualizar estado
        public class EstadoRequest
        {
            public EstadoGestion Estado { get; set; }
        }

        // GET api/gestiones
        [HttpGet]
        public ActionResult<IEnumerable<Gestion>> GetAll() =>
            Ok(RepositorioGestiones.Gestiones);

        // GET api/gestiones/{id}
        [HttpGet("{id}")]
        public ActionResult<Gestion> GetById(string id)
        {
            var g = RepositorioGestiones.Gestiones
                        .FirstOrDefault(x => x.GestionID == id);
            if (g == null) return NotFound();
            return Ok(g);
        }

        // GET api/gestiones/tarjeta/{numeroTarjeta}
        [HttpGet("tarjeta/{numeroTarjeta}")]
        public ActionResult<IEnumerable<Gestion>> GetByTarjeta(string numeroTarjeta)
        {
            var lista = RepositorioGestiones.Gestiones
                          .Where(x => x.NumeroTarjeta == numeroTarjeta)
                          .ToList();
            return Ok(lista);
        }

        // POST api/gestiones
        [HttpPost]
        public ActionResult<Gestion> Create([FromBody] NuevaGestionRequest req)
        {
            if (!RepositorioTarjetas.Tarjetas.ContainsKey(req.NumeroTarjeta))
                return BadRequest($"Tarjeta {req.NumeroTarjeta} no existe.");

            // Construimos la gestión incluyendo los datos específicos
            var gestion = new Gestion
            {
                GestionID = Guid.NewGuid().ToString(),
                NumeroTarjeta = req.NumeroTarjeta,
                FechaSolicitud = DateTime.Now,
                TipoGestion = req.TipoGestion,
                Estado = EstadoGestion.Pendiente,
                Comentarios = req.Comentarios,
                AñosRenovacion = req.TipoGestion == TipoGestion.Remision
                                      ? req.AniosRenovacion
                                      : 0,
                NuevoPin = req.TipoGestion == TipoGestion.CambioPIN
                                      ? req.NuevoPin
                                      : null
            };

            // Usamos AddGestion para poblar lista, árbol y cola
            RepositorioGestiones.AddGestion(gestion);

            return CreatedAtAction(
                nameof(GetById),
                new { id = gestion.GestionID },
                gestion
            );
        }

        // PUT api/gestiones/{id}/estado
        [HttpPut("{id}/estado")]
        public ActionResult UpdateEstado(string id, [FromBody] EstadoRequest req)
        {
            var gestion = RepositorioGestiones.Gestiones
                              .FirstOrDefault(x => x.GestionID == id);
            if (gestion == null)
                return NotFound();

            if (gestion.Estado != EstadoGestion.Pendiente)
                return BadRequest("Solo se pueden actualizar gestiones en estado Pendiente.");

            // 1) Cambiamos el estado
            gestion.Estado = req.Estado;

            // 2) Si se aprueba, aplicamos el cambio a la tarjeta
            if (req.Estado == EstadoGestion.Aprobado)
            {
                var tarjeta = RepositorioTarjetas.Tarjetas[gestion.NumeroTarjeta];
                switch (gestion.TipoGestion)
                {
                    case TipoGestion.Remision:
                        tarjeta.FechaVencimiento =
                            tarjeta.FechaVencimiento.AddYears(gestion.AñosRenovacion);
                        break;
                    case TipoGestion.CambioPIN:
                        tarjeta.Pin = gestion.NuevoPin;
                        break;
                        // puedes manejar otros tipos aquí…
                }
            }

            return NoContent();
        }
    }
}