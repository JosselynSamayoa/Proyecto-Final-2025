namespace Proyecto_Final_Sistema_Bancario.EstructurasDatos.ListaEnlazada
{
    public class NodoLista
    {
        public int Dato;
        public NodoLista Siguiente;

        public NodoLista(int dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }
}
