using Proyecto_Final_Sistema_Bancario.EstructurasDatos.Arboles;
using Proyecto_Final_Sistema_Bancario.Models;

namespace Proyecto_Final_Sistema_Bancario.Models
{
    public enum TipoGestion
    {
        Bloqueo,
        AumentoLimite,
        Remision,
        CambioPIN
    }

    public enum EstadoGestion
    {
        Pendiente,
        Aprobado,
        Rechazado
    }

    public class Gestion : Comparador
    {
        public string GestionID { get; set; }
        public string NumeroTarjeta { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public TipoGestion TipoGestion { get; set; }
        public EstadoGestion Estado { get; set; }
        public string Comentarios { get; set; }
        public int AñosRenovacion { get; set; }
        public string NuevoPin { get; set; }

        public Gestion() { }

        public Gestion(string gestionID, string numeroTarjeta, DateTime fechaSolicitud,
                       TipoGestion tipoGestion, EstadoGestion estado, string comentarios,
                       int añosRenovacion, string nuevoPin)
        {
            GestionID = gestionID;
            NumeroTarjeta = numeroTarjeta;
            FechaSolicitud = fechaSolicitud;
            TipoGestion = tipoGestion;
            Estado = estado;
            Comentarios = comentarios;
            AñosRenovacion = añosRenovacion;
            NuevoPin = nuevoPin;
        }

        public bool igualQue(object q)
        {
            Gestion g2 = (Gestion)q;
            return GestionID.CompareTo(g2.GestionID) == 0;
        }

        public bool menorQue(object q)
        {
            Gestion g2 = (Gestion)q;
            return GestionID.CompareTo(g2.GestionID) < 0;
        }

        public bool mayorQue(object q)
        {
            Gestion g2 = (Gestion)q;
            return GestionID.CompareTo(g2.GestionID) > 0;
        }
    }
}
