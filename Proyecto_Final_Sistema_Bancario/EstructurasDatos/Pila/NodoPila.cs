namespace Proyecto_Final_Sistema_Bancario.EstructurasDatos.Pila
{
    public class NodoPila
    {
        public int Dato;
        public NodoPila Siguiente;

        public NodoPila(int dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }
}
