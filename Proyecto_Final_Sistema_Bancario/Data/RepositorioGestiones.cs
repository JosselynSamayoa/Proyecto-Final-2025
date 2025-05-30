using System;
using System.Linq;
using System.Collections.Generic;
using Proyecto_Final_Sistema_Bancario.EstructurasDatos.Arboles;
using Proyecto_Final_Sistema_Bancario.EstructurasDatos.Colas;
using Proyecto_Final_Sistema_Bancario.Models;

namespace Proyecto_Final_Sistema_Bancario.Data
{
    public static class RepositorioGestiones
    {
        // 1) BST por GestionID
        public static ArbolBinarioBusqueda ArbolGestiones { get; }
            = new ArbolBinarioBusqueda();

        // 2) Lista simple para todas las gestiones
        public static List<Gestion> Gestiones { get; }
            = new List<Gestion>();

        // 3) Cola: pendientes de autorización
        public static Cola ColaPendientes { get; }
            = new Cola();

        public static void AddGestion(Gestion g)
        {
            Gestiones.Add(g);
            ArbolGestiones.insertar(g);
            // encolar sólo si está pend
            if (g.Estado == EstadoGestion.Pendiente)
                ColaPendientes.Encolar(int.Parse(g.GestionID.GetHashCode().ToString()));
        }

        // Aprobar o rechazar:
        public static bool UpdateEstado(string id, EstadoGestion nuevoEstado)
        {
            var g = Gestiones.FirstOrDefault(x => x.GestionID == id);
            if (g == null || g.Estado != EstadoGestion.Pendiente) return false;
            g.Estado = nuevoEstado;
            return true;
        }
    }
}