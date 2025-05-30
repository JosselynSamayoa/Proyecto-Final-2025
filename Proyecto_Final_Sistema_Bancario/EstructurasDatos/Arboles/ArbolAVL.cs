using Proyecto_Final_Sistema_Bancario.EstructurasDatos.Arboles;

namespace Proyecto_Final_Sistema_Bancario.EstructurasDatos.Arboles
{
    public class ArbolAVL
    {
        protected NodoAVL raiz;

        public ArbolAVL()
        {
            raiz = null;
        }

        public NodoAVL raizArbol()
        {
            return raiz;
        }

        private NodoAVL rotacionII(NodoAVL n, NodoAVL n1)
        {
            n.ramaIzdo(n1.subarbolDcho());
            n1.ramaDcho(n);
            // actualización de los factores de equilibrio
            if (n1.fe == -1) // se cumple en la inserción
            {
                n.fe = 0;
                n1.fe = 0;
            }
            else
            {
                n.fe = -1;
                n1.fe = 1;
            }
            return n1;
        }

        private NodoAVL rotacionDD(NodoAVL n, NodoAVL n1)
        {
            n.ramaDcho(n1.subarbolIzdo());
            n1.ramaIzdo(n);
            // actualización de los factores de equilibrio
            if (n1.fe == +1) // se cumple en la inserción
            {
                n.fe = 0;
                n1.fe = 0;
            }
            else
            {
                n.fe = +1;
                n1.fe = -1;
            }
            return n1;
        }


        private NodoAVL rotacionID(NodoAVL n, NodoAVL n1)
        {
            NodoAVL n2;
            n2 = (NodoAVL)n1.subarbolDcho();
            n.ramaIzdo(n2.subarbolDcho());
            n2.ramaDcho(n);
            n1.ramaDcho(n2.subarbolIzdo());
            n2.ramaIzdo(n1);
            // actualización de los factores de equilibrio
            if (n2.fe == +1)
                n1.fe = -1;
            else
                n1.fe = 0;
            if (n2.fe == -1)
                n.fe = 1;
            else
                n.fe = 0;
            n2.fe = 0;
            return n2;
        }

        private NodoAVL rotacionDI(NodoAVL n, NodoAVL n1)
        {
            NodoAVL n2;
            n2 = (NodoAVL)n1.subarbolIzdo();
            n.ramaDcho(n2.subarbolIzdo());
            n2.ramaIzdo(n);
            n1.ramaIzdo(n2.subarbolDcho());
            n2.ramaDcho(n1);
            // actualización de los factores de equilibrio
            if (n2.fe == +1)
                n.fe = -1;
            else
                n.fe = 0;
            if (n2.fe == -1)
                n1.fe = 1;
            else
                n1.fe = 0;
            n2.fe = 0;
            return n2;
        }



        public void insertar(Object valor)//throws Exception
        {
            Comparador dato;
            Logical h = new Logical(false); // intercambia un valor booleano
            dato = (Comparador)valor;
            raiz = insertarAvl(raiz, dato, h);
        }

        private NodoAVL insertarAvl(NodoAVL raiz, Comparador dt, Logical h)
        //throws Exception
        {
            NodoAVL n1;
            if (raiz == null)
            {
                raiz = new NodoAVL(dt);
                h.setLogical(true);
            }
            else if (dt.menorQue(raiz.valorNodo()))
            {
                NodoAVL iz;
                iz = insertarAvl((NodoAVL)raiz.subarbolIzdo(), dt, h);
                raiz.ramaIzdo(iz);
                // regreso por los nodos del camino de búsqueda
                if (h.booleanValue())
                {
                    // decrementa el fe por aumentar la altura de rama izquierda
                    switch (raiz.fe)
                    {
                        case 1:
                            raiz.fe = 0;
                            h.setLogical(false);
                            break;
                        case 0:
                            raiz.fe = -1;
                            break;
                        case -1: // aplicar rotación a la izquierda
                            n1 = (NodoAVL)raiz.subarbolIzdo();
                            if (n1.fe == -1)
                                raiz = rotacionII(raiz, n1);
                            else
                                raiz = rotacionID(raiz, n1);
                            h.setLogical(false);
                            break;
                    }
                }
            }
            else if (dt.mayorQue(raiz.valorNodo()))
            {
                NodoAVL dr;
                dr = insertarAvl((NodoAVL)raiz.subarbolDcho(), dt, h);
                raiz.ramaDcho(dr);
                // regreso por los nodos del camino de búsqueda
                if (h.booleanValue())
                {
                    // incrementa el fe por aumentar la altura de rama izquierda
                    switch (raiz.fe)
                    {
                        case 1: // aplicar rotación a la derecha
                            n1 = (NodoAVL)raiz.subarbolDcho();
                            if (n1.fe == +1)
                                raiz = rotacionDD(raiz, n1);
                            else
                                raiz = rotacionDI(raiz, n1);
                            h.setLogical(false);
                            break;
                        case 0:
                            raiz.fe = +1;
                            break;
                        case -1:
                            raiz.fe = 0;
                            h.setLogical(false);
                            break;
                    }
                }
            }
            else
                throw new Exception("No puede haber claves repetidas ");
            return raiz;
        }

        public void eliminar(Object valor) //throws Exception
        {
            Comparador dato;
            dato = (Comparador)valor;
            Logical flag = new Logical(false);
            raiz = borrarAvl(raiz, dato, flag);
        }

        private NodoAVL borrarAvl(NodoAVL r, Comparador clave, Logical cambiaAltura) //throws Exception
        {
            if (r == null)
            {
                throw new Exception(" Nodo no encontrado ");
            }
            else if (clave.menorQue(r.valorNodo()))
            {
                NodoAVL iz;
                iz = borrarAvl((NodoAVL)r.subarbolIzdo(), clave, cambiaAltura);
                r.ramaIzdo(iz);
                if (cambiaAltura.booleanValue())
                    r = equilibrar1(r, cambiaAltura);
            }
            else if (clave.mayorQue(r.valorNodo()))
            {
                NodoAVL dr;
                dr = borrarAvl((NodoAVL)r.subarbolDcho(), clave, cambiaAltura);
                r.ramaDcho(dr);
                if (cambiaAltura.booleanValue())
                    r = equilibrar2(r, cambiaAltura);
            }
            else // Nodo encontrado
            {
                NodoAVL q;
                q = r; // nodo a quitar del árbol
                if (q.subarbolIzdo() == null)
                {
                    r = (NodoAVL)q.subarbolDcho();
                    cambiaAltura.setLogical(true);
                }
                else if (q.subarbolDcho() == null)
                {
                    r = (NodoAVL)q.subarbolIzdo();
                    cambiaAltura.setLogical(true);
                }
                else
                { // tiene rama izquierda y derecha
                    NodoAVL iz;
                    iz = reemplazar(r, (NodoAVL)r.subarbolIzdo(), cambiaAltura);
                    r.ramaIzdo(iz);
                    if (cambiaAltura.booleanValue())
                        r = equilibrar1(r, cambiaAltura);
                }
                q = null;
            }
            return r;
        }

        private NodoAVL reemplazar(NodoAVL n, NodoAVL act, Logical cambiaAltura)
        {
            if (act.subarbolDcho() != null)
            {
                NodoAVL d;
                d = reemplazar(n, (NodoAVL)act.subarbolDcho(), cambiaAltura);
                act.ramaDcho(d);
                if (cambiaAltura.booleanValue())
                    act = equilibrar2(act, cambiaAltura);
            }
            else
            {
                n.nuevoValor(act.valorNodo());
                n = act;
                act = (NodoAVL)act.subarbolIzdo();
                n = null;
                cambiaAltura.setLogical(true);
            }
            return act;
        }

        private NodoAVL equilibrar1(NodoAVL n, Logical cambiaAltura)
        {
            NodoAVL n1;
            switch (n.fe)
            {
                case -1:
                    n.fe = 0;
                    break;
                case 0:
                    n.fe = 1;
                    cambiaAltura.setLogical(false);
                    break;
                case +1: //se aplicar un tipo de rotación derecha
                    n1 = (NodoAVL)n.subarbolDcho();
                    if (n1.fe >= 0)
                    {
                        if (n1.fe == 0) //la altura no vuelve a disminuir
                            cambiaAltura.setLogical(false);
                        n = rotacionDD(n, n1);
                    }
                    else
                        n = rotacionDI(n, n1);
                    break;
            }
            return n;
        }

        private NodoAVL equilibrar2(NodoAVL n, Logical cambiaAltura)
        {
            NodoAVL n1;
            switch (n.fe)
            {
                case -1: // Se aplica un tipo de rotación izquierda
                    n1 = (NodoAVL)n.subarbolIzdo();
                    if (n1.fe <= 0)
                    {
                        if (n1.fe == 0)
                            cambiaAltura.setLogical(false);
                        n = rotacionII(n, n1);
                    }
                    else
                        n = rotacionID(n, n1);
                    break;
                case 0:
                    n.fe = -1;
                    cambiaAltura.setLogical(false);
                    break;
                case +1:
                    n.fe = 0;
                    break;
            }
            return n;
        }

        /////////////////////////////////
        /// <summary>
        /// Comprueba el estatus del árbol
        /// </summary>
        /// <returns></returns>
        bool esVacio()
        {
            return raiz == null;
        }

        //public static NodoAvl nuevoArbol(NodoAvl ramaIzqda, Object dato, NodoAvl ramaDrcha)
        //{
        //    return new NodoAvl(dato,ramaIzqda, ramaDrcha);
        //}


        //binario en preorden
        public static string preorden(Nodo r)
        {
            if (r != null)
            {
                return r.visitar() + preorden(r.subarbolIzdo()) + preorden(r.subarbolDcho());
            }
            return "";
        }

        // Recorrido de un árbol binario en inorden
        public static string inorden(Nodo r)
        {
            if (r == null) return "";
            return inorden(r.subarbolIzdo())
                 + r.visitar()
                 + Environment.NewLine
                 + inorden(r.subarbolDcho());
        }

        // Recorrido de un árbol binario en postorden
        public static string postorden(Nodo r)
        {
            if (r != null)
            {
                return postorden(r.subarbolIzdo()) + postorden(r.subarbolDcho()) + r.visitar();
            }
            return "";
        }

        //Devuelve el número de nodos que tiene el árbol
        public static int numNodos(Nodo raiz)
        {
            if (raiz == null)
                return 0;
            else
                return 1 + numNodos(raiz.subarbolIzdo()) +
                numNodos(raiz.subarbolDcho());
        }
    }
}
