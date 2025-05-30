using System;
using System.Collections.Generic;
using Proyecto_Final_Sistema_Bancario.EstructurasDatos.Arboles;
using Proyecto_Final_Sistema_Bancario.EstructurasDatos.TablasHash;
using Proyecto_Final_Sistema_Bancario.Models;

namespace Proyecto_Final_Sistema_Bancario.Data
{
    public static class RepositorioTarjetas
    {
        // 1) Para tu controller, mantenemos el Dictionary
        public static Dictionary<string, TarjetaDeCredito> Tarjetas { get; }
            = new Dictionary<string, TarjetaDeCredito>();

        // 2) Estructuras avanzadas
        public static TablaHashGenerica TablaTarjetas { get; }
            = new TablaHashGenerica(100);

        public static ArbolBinarioBusqueda ArbolTarjetas { get; }
            = new ArbolBinarioBusqueda();

        // Método unificado para agregar
        public static void AddTarjeta(TarjetaDeCredito t)
        {
            // 1) Mantén el diccionario para el controller
            Tarjetas[t.NumeroTarjeta] = t;

            // 2) Inserta en tu hash y BST
            int clave = t.NumeroTarjeta.GetHashCode();
            TablaTarjetas.Insertar(clave, t);
            ArbolTarjetas.insertar(t);
        }

        // (Opcional) Método de búsqueda por hash
        public static TarjetaDeCredito? BuscarPorHash(string numeroTarjeta)
        {
            int clave = numeroTarjeta.GetHashCode();
            return TablaTarjetas.Buscar(clave) as TarjetaDeCredito;
        }
    }
}