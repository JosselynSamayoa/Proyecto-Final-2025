using Proyecto_Final_Sistema_Bancario.EstructurasDatos.Arboles;

namespace Proyecto_Final_Sistema_Bancario.Models
{
    public class Transaccion : Comparador
    {
        public string TransaccionID { get; set; }
        public string NumeroTarjeta { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }

        public Transaccion() { }

        public Transaccion(string transaccionID,
                           string numeroTarjeta,
                           DateTime fecha,
                           string tipo,
                           decimal monto,
                           string descripcion,
                           string categoria)
        {
            TransaccionID = transaccionID;
            NumeroTarjeta = numeroTarjeta;
            Fecha = fecha;
            Tipo = tipo;
            Monto = monto;
            Descripcion = descripcion;
            Categoria = categoria;
        }

        public bool mayorQue(object q)
        {
            var t2 = (Transaccion)q;
            return TransaccionID.CompareTo(t2.TransaccionID) > 0;
        }

        public bool menorQue(object q)
        {
            var t2 = (Transaccion)q;
            return TransaccionID.CompareTo(t2.TransaccionID) < 0;
        }

        public bool igualQue(object q)
        {
            var t2 = (Transaccion)q;
            return TransaccionID.CompareTo(t2.TransaccionID) == 0;
        }
    }
}
