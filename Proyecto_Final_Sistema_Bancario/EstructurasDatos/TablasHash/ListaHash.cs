namespace Proyecto_Final_Sistema_Bancario.EstructurasDatos.TablasHash
{
    internal class ListaHash
    {
        private NodoHash primero;

        public void Insertar(DatoHash dato)
        {
            var nuevo = new NodoHash(dato) { Enlace = primero };
            primero = nuevo;
        }

        public bool Eliminar(int clave)
        {
            NodoHash actual = primero, anterior = null;
            while (actual != null)
            {
                if (actual.Dato.Clave == clave)
                {
                    if (anterior == null) primero = actual.Enlace;
                    else anterior.Enlace = actual.Enlace;
                    return true;
                }
                anterior = actual;
                actual = actual.Enlace;
            }
            return false;
        }

        public bool Actualizar(int clave, object valor)
        {
            for (var actual = primero; actual != null; actual = actual.Enlace)
            {
                if (actual.Dato.Clave == clave)
                {
                    actual.Dato.Valor = valor;
                    return true;
                }
            }
            return false;
        }

        public object Buscar(int clave)
        {
            for (var actual = primero; actual != null; actual = actual.Enlace)
                if (actual.Dato.Clave == clave)
                    return actual.Dato.Valor;
            return null;
        }
    }
}
