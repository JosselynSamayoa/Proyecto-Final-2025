namespace Proyecto_Final_Sistema_Bancario.EstructurasDatos.TablasHash
{
    public class NodoHash
    {
        public DatoHash Dato;
        public NodoHash Enlace;

        public NodoHash(DatoHash dato)
        {
            Dato = dato;
            Enlace = null;
        }
    }
}
