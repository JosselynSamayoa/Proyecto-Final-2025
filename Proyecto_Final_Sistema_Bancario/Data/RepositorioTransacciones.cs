using System;
using System.Collections.Generic;
using Proyecto_Final_Sistema_Bancario.EstructurasDatos.Arboles;
using Proyecto_Final_Sistema_Bancario.EstructurasDatos.ListaEnlazada;
using Proyecto_Final_Sistema_Bancario.EstructurasDatos.Pila;
using Proyecto_Final_Sistema_Bancario.EstructurasDatos.Colas;
using Proyecto_Final_Sistema_Bancario.Models;

namespace Proyecto_Final_Sistema_Bancario.Data
{
    public static class RepositorioTransacciones
    {
        // 1) Para el controller, mantenemos esta lista
        public static List<Transaccion> Transacciones { get; }
            = new List<Transaccion>();

        // 2) Estructuras avanzadas
        public static ArbolBinarioBusqueda ArbolTransacciones { get; }
            = new ArbolBinarioBusqueda();

        public static ListaEnlazada ListaHistorial { get; }
            = new ListaEnlazada();

        public static Pila PilaReciente { get; }
            = new Pila();

        public static Cola ColaBatch { get; }
            = new Cola();

        public static void AddTransaccion(Transaccion t)
        {
            // 1) Lista para el controller
            Transacciones.Add(t);

            // 2) Inserta en BST
            ArbolTransacciones.insertar(t);

            // 3) Lista enlazada, pila y cola
            ListaHistorial.InsertarFinal(int.Parse(t.TransaccionID.GetHashCode().ToString()));
            PilaReciente.Apilar(t.TransaccionID.GetHashCode());
            ColaBatch.Encolar(t.TransaccionID.GetHashCode());
        }

        // (Opcional) Búsqueda en BST
        public static Transaccion? BuscarPorArbol(string id)
        {
            var nodo = ArbolTransacciones.buscar(new Transaccion { TransaccionID = id });
            return nodo is null ? null : (Transaccion)nodo.valorNodo();
        }
    }
}