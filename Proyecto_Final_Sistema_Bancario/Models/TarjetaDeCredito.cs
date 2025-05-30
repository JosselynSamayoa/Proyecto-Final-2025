using Proyecto_Final_Sistema_Bancario.EstructurasDatos.Arboles;

namespace Proyecto_Final_Sistema_Bancario.Models
{
    public class TarjetaDeCredito : Comparador
    {
        public string NumeroTarjeta { get; set; }
        public string TipoTarjeta { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal SaldoActual { get; set; }
        public string ClienteID { get; set; }
        public string Pin { get; set; } 

        public TarjetaDeCredito() { }
        
        public TarjetaDeCredito(string numeroTarjeta,
                                string tipoTarjeta,
                                DateTime fechaVencimiento,
                                decimal limiteCredito,
                                decimal saldoActual,
                                string clienteID,
                                string pin)
        {
            NumeroTarjeta = numeroTarjeta;
            TipoTarjeta = tipoTarjeta;
            FechaVencimiento = fechaVencimiento;
            LimiteCredito = limiteCredito;
            SaldoActual = saldoActual;
            ClienteID = clienteID;
            Pin = pin;
        }

        public bool mayorQue(object q)
        {
            var t2 = (TarjetaDeCredito)q;
            return NumeroTarjeta.CompareTo(t2.NumeroTarjeta) > 0;
        }

        public bool menorQue(object q)
        {
            var t2 = (TarjetaDeCredito)q;
            return NumeroTarjeta.CompareTo(t2.NumeroTarjeta) < 0;
        }

        public bool igualQue(object q)
        {
            var t2 = (TarjetaDeCredito)q;
            return NumeroTarjeta.CompareTo(t2.NumeroTarjeta) == 0;
        }
    }
}
