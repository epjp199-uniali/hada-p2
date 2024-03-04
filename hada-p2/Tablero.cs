using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Tablero
    {
        public int TamTablero; // Debe de ser >= 4 y <= 9

        private List<Coordenada> coordenadasDisparadas;

        private List<Coordenada> coordenadasTocadas;

        private List<Barco> barcos; 

        private List<Barco> barcosEliminados;

        private Dictionary<Coordenada, string> casillasTablero;


        public Tablero(int tamTablero,List<Barco> barcos)
        {

        }

        private void inicializaCasillasTablero()
        {

        }

        public void Disparar(Coordenada c)
        {

        }

        //public string DibujarTablero()
        //{

        //}
 
        //public string ToString()
        //{

        //}

        private void cuandoEventoTocado() // Cuando se invoca se actualizan las casillas del tablero y se imprime el siguiente texto: 
        {                                 // TABLERO: Barco [NOMBRE_BARCO] tocado en Coordenada: [(FILA,COLUMNA)]


        }

        private void cuandoEventoHundido() // Cuando se invoca se imprime el texto: 'TABLERO: barco [NOMBRE_BARCO] hundido !!'
        {

        }
        public event EventHandler<EventArgs> eventoFinPartida;
    }
}
