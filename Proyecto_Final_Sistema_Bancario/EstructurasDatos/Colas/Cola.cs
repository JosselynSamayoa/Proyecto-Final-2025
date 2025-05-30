using Proyecto_Final_Sistema_Bancario.EstructurasDatos.Colas;

namespace Proyecto_Final_Sistema_Bancario.EstructurasDatos.Colas
{
    public class Cola
    {
        private NodoCola frente, Final;

        public Cola()
        {
            frente = Final = null;
        }

        public bool EstaVacia() 
        {
            return frente == null;
        }

        public void Encolar(int dato) 
        {
            NodoCola nuevo = new NodoCola(dato);
            if (EstaVacia())
                frente = Final = nuevo;
            else
            {
                Final.Siguiente = nuevo;
                Final = nuevo;
            }
        }

        public int Desencolar()
        {
            if (EstaVacia()) throw new InvalidOperationException("La cola esta vacia");

            int valor = frente.Dato;
            frente = frente.Siguiente;
            if (frente == null) Final = null;

            return valor;
        }

        public void Vaciar()
        {
            frente = Final = null;
        }

        public string Mostrar()
        {
            NodoCola actual = frente;
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
