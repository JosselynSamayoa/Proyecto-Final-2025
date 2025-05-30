using System;
using System.Collections.Generic;
using Proyecto_Final_Sistema_Bancario.EstructurasDatos.Arboles;
using Proyecto_Final_Sistema_Bancario.Models;

namespace Proyecto_Final_Sistema_Bancario.Data
{
    public static class RepositorioClientes
    {
        // 1) Hash: acceso O(1) por ClienteID
        public static Dictionary<string, Cliente> Clientes { get; }
            = new();

        // 2) BST: búsquedas ordenadas por ClienteID
        public static ArbolBinarioBusqueda ArbolClientes { get; }
            = new ArbolBinarioBusqueda();

        // 3) AVL: balanceo para, p.ej., activos
        public static ArbolAVL AvlClientes { get; }
            = new ArbolAVL();

        // Inserta en las tres estructuras
        public static void AddCliente(Cliente c)
        {
            Clientes[c.ClienteID] = c;
            ArbolClientes.insertar(c);
            AvlClientes.insertar(c);
        }

        // Ejemplo de búsqueda en BST
        public static Cliente? BuscarPorArbol(string id)
        {
            var nodo = ArbolClientes.buscar(new Cliente { ClienteID = id });
            return nodo is null ? null : (Cliente)nodo.valorNodo();
        }
    }
}