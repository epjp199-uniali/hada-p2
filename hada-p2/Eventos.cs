using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class TocadoArgs : EventArgs
    {
        public string nombre;

        public Coordenada coordenadaImpacto;

        public string etiqueta;

        public TocadoArgs(string n, Coordenada b, string e)
        {
            nombre = n;
            coordenadaImpacto = b;
            etiqueta = e;
        }
    }

    public class HundidoArgs : EventArgs
    {
        public string nombre;

        public HundidoArgs(string n)
        {
            nombre = n;
        }
    }
}
