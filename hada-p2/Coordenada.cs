using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Coordenada
    {
        public int Fila;
        public int Columna;


        public Coordenada()
        {
            Fila = -1;
            Columna = -1;
        }

        public Coordenada(int fila, int columna)
        {
            Fila = fila;
            Columna = columna;
        }

        public Coordenada(string fila, string columna)
        {
            Fila = int.Parse(fila);
            Columna = int.Parse(columna);
        }

        public Coordenada(Coordenada a)
        {
            Fila = a.Fila;
            Columna = a.Columna;
        }
    }
}
