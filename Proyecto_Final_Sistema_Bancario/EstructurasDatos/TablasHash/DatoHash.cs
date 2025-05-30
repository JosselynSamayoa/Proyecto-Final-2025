namespace Proyecto_Final_Sistema_Bancario.EstructurasDatos.TablasHash
{
    public class DatoHash
    {
        public int Clave { get; }
        public object Valor { get; set; }

        public DatoHash(int clave, object valor)
        {
            Clave = clave;
            Valor = valor;
        }
        public override bool Equals(object obj) =>
            obj is DatoHash d && d.Clave == Clave;
        public override int GetHashCode() =>
            Clave.GetHashCode();
    }
}
