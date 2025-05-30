using Proyecto_Final_Sistema_Bancario.EstructurasDatos.Pila;

namespace Proyecto_Final_Sistema_Bancario.EstructurasDatos.Pila
{
    public class Pila
    {
        private NodoPila Tope;

        public Pila()
        {
            Tope = null;
        }

        public bool EstaVacia()
        {
            return Tope == null;
        }

        public void Apilar(int dato)
        {
            NodoPila nuevo = new NodoPila(dato);
            nuevo.Siguiente = Tope;
            Tope = nuevo;
        }

        public int Desapilar()
        {
            if (EstaVacia()) throw new InvalidOperationException("La pila esta vacia");

            int valor = Tope.Dato;
            Tope = Tope.Siguiente;
            return valor;
        }

        public void Vaciar()
        {
            Tope = null;
        }

        public string Mostrar()
        {
            NodoPila actual = Tope;
            string resultado = "";
            while (actual != null)
            {
                resultado += actual.Dato + " ";
                actual = actual.Siguiente;
            }
            return resultado.Trim();
        }
    }
}
