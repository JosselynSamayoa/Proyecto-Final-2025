namespace Proyecto_Final_Sistema_Bancario.EstructurasDatos.Colas
{
    public class NodoCola
    {
        public int Dato;
        public NodoCola Siguiente;

        public NodoCola(int dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }
}
