using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Proyecto_Final_Sistema_Bancario.Data;
using Proyecto_Final_Sistema_Bancario.Models;

namespace Proyecto_Final_Sistema_Bancario.Utils
{
    public static class CargaInicial
    {
        public static void CargarClientes(string rutaJson)
        {
            var texto = File.ReadAllText(rutaJson);
            var listaClientes = JsonSerializer.Deserialize<List<Cliente>>(texto)
                                ?? new List<Cliente>();

            // Aquí usamos el método AddCliente que actualiza Hash, BST y AVL
            foreach (var cliente in listaClientes)
            {
                RepositorioClientes.AddCliente(cliente);
            }
        }

        public static void CargarTarjetas(string rutaJson)
        {
            var texto = File.ReadAllText(rutaJson);
            var listaTarjetas = JsonSerializer.Deserialize<List<TarjetaDeCredito>>(texto)
                                 ?? new List<TarjetaDeCredito>();

            // Aquí usamos AddTarjeta que actualiza Dictionary, Hash y BST
            foreach (var tarjeta in listaTarjetas)
            {
                RepositorioTarjetas.AddTarjeta(tarjeta);
            }
        }

        public static void CargarTransacciones(string rutaJson)
        {
            var texto = File.ReadAllText(rutaJson);
            var listaTransacciones = JsonSerializer.Deserialize<List<Transaccion>>(texto)
                                      ?? new List<Transaccion>();

            // Aquí usamos AddTransaccion que actualiza BST, lista enlazada, pila y cola
            foreach (var transaccion in listaTransacciones)
            {
                RepositorioTransacciones.AddTransaccion(transaccion);
            }
        }

        public static void CargarGestiones(string rutaJson)
        {
            var texto = File.ReadAllText(rutaJson);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new JsonStringEnumConverter());

            var listaGestiones = JsonSerializer
                .Deserialize<List<Gestion>>(texto, options)
                               ?? new List<Gestion>();

            // Aquí usamos AddGestion que actualiza lista simple, BST y cola de pendientes
            foreach (var gestion in listaGestiones)
            {
                RepositorioGestiones.AddGestion(gestion);
            }
        }
    }
}
