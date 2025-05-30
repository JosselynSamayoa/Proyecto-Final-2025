using System.Collections.Generic;
using Proyecto_Final_Sistema_Bancario.EstructurasDatos.Arboles;

namespace Proyecto_Final_Sistema_Bancario.Models
{
    public class Cliente : Comparador 
    {
        public Cliente() { }

        public Cliente(string clienteID, string nombre,
            string apellido, DateTime fechaNacimiento,
            string email, string telefono, string direccion,
            string calle, string ciudad, string codigoPostal) 
        {
            ClienteID = clienteID;
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;
            Email = email;
            Telefono = telefono;
            Direccion = direccion;
            Calle = calle;
            Ciudad = ciudad;
            CodigoPostal = codigoPostal;
        }

        public string ClienteID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Calle { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }

        public bool igualQue(object q)
        {
            Cliente C2 = (Cliente)q;
            return ClienteID.CompareTo(C2.ClienteID) == 0;
        }

        public bool mayorQue(object q)
        {
            Cliente C2 = (Cliente)q;
            return ClienteID.CompareTo(C2.ClienteID) > 0;
        }

        public bool menorQue(object q)
        {
            Cliente C2 = (Cliente)q;
            return ClienteID.CompareTo(C2.ClienteID) < 0;
        }
    }
}
