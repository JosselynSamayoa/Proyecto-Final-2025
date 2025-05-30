using Proyecto_Final_Sistema_Bancario.EstructurasDatos.Arboles;

namespace Proyecto_Final_Sistema_Bancario.EstructurasDatos.Arboles
{
    public class NodoAVL : Nodo
    {
        public int fe;
        public NodoAVL(Object valor) : base(valor)
        {
            fe = 0;
        }

        public NodoAVL(Object valor, NodoAVL ramaIzdo, NodoAVL ramaDcho) : base(ramaIzdo, valor, ramaDcho)
        {
            fe = 0;
        }
    }
}
