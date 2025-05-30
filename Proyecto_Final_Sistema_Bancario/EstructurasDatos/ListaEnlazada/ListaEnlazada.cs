using Proyecto_Final_Sistema_Bancario.EstructurasDatos.ListaEnlazada;

namespace Proyecto_Final_Sistema_Bancario.EstructurasDatos.ListaEnlazada
{
    public class ListaEnlazada
    {
        private NodoLista cabeza;

        public ListaEnlazada()
        {
            cabeza = null;
        }

        public void InsertarFinal(params int[] datos) 
        {
            foreach (int dato in datos)
            {
                NodoLista nuevo = new NodoLista(dato);
                if (cabeza == null)
                    cabeza = nuevo;
                else
                {
                    NodoLista actual = cabeza;
                    while (actual.Siguiente != null)
                        actual = actual.Siguiente;
                    actual.Siguiente = nuevo;
                }
            }
        }

        public int Sumar()
        {
            int suma = 0;
            NodoLista actual = cabeza;
            while (actual != null)
            {
                suma += actual.Dato;
                actual = actual.Siguiente;
            }
            return suma;
        }

        public string Mostrar()
        {
            NodoLista actual = cabeza;
            string resultado = "";
            while (actual != null)
            {
                resultado += actual.Dato + " ";
                actual = actual.Siguiente;
            }
            return resultado.Trim();
        }

        public string[] ObtenerArray()
        {
            List<string> lista = new List<string>();
            NodoLista actual = cabeza;
            while (actual != null)
            {
                lista.Add(actual.Dato.ToString());
                actual = actual.Siguiente;
            }
            return lista.ToArray();
        }
    }
}
