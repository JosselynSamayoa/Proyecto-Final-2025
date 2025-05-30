namespace Proyecto_Final_Sistema_Bancario.EstructurasDatos.TablasHash
{
    public class TablaHashGenerica
    {
        private readonly ListaHash[] buckets;
        private readonly int M;

        public TablaHashGenerica(int tamanio = 10)
        {
            M = tamanio;
            buckets = new ListaHash[M];
        }

        private int DispersionMod(int clave) =>
            Math.Abs(clave) % M;

        // Inserta o actualiza:
        public void Insertar(int clave, object valor)
        {
            int pos = DispersionMod(clave);
            if (buckets[pos] == null) buckets[pos] = new ListaHash();

            // Si ya existe, actualiza; si no, inserta al frente
            if (!buckets[pos].Actualizar(clave, valor))
                buckets[pos].Insertar(new DatoHash(clave, valor));
        }

        public bool Eliminar(int clave)
        {
            int pos = DispersionMod(clave);
            return buckets[pos]?.Eliminar(clave) ?? false;
        }

        public bool Actualizar(int clave, object valor)
        {
            int pos = DispersionMod(clave);
            return buckets[pos]?.Actualizar(clave, valor) ?? false;
        }

        public object Buscar(int clave)
        {
            int pos = DispersionMod(clave);
            return buckets[pos]?.Buscar(clave);
        }
    }
}
