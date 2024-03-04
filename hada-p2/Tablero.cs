using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Tablero
    {
        private int TableroDimension;

        public int TamTablero
        {
            get { return TableroDimension; }

            set
            {
                if ((value > 3) && (value < 10))
                {
                    TableroDimension = value;
                }
                else
                {
                    throw new Exception("Datos de entrada incorrectos");
                }
            }
        }

        private List<Coordenada> coordenadasDisparadas;

        private List<Coordenada> coordenadasTocadas;

        private List<Barco> barcos;

        private List<Barco> barcosEliminados;

        private Dictionary<Coordenada, string> casillasTablero;


        public Tablero(int tamTablero, List<Barco> barcos)
        {
            TamTablero = tamTablero;

            this.barcos = barcos;

            inicializaCasillasTablero();

        }

        private void inicializaCasillasTablero()
        {
            Coordenada aux;
            bool barcofound = false;

            for (int i = 0; i < TamTablero; i++)
            {
                for (int j = 0; j < TamTablero; j++)
                {
                    aux = new Coordenada(i, j);

                    foreach (Barco baux in barcos)
                    {
                        foreach (Coordenada caux in baux.CoordenadasBarco.Keys)
                        {
                            if ((aux == caux) && (barcofound == false))
                            {
                                casillasTablero.Add(aux, baux.CoordenadasBarco[aux]);
                                barcofound = true;
                            }
                        }

                        if (barcofound == false) { casillasTablero.Add(aux, "AGUA"); }

                        barcofound = false;
                    }
                }
            }
        }

        public void Disparar(Coordenada c)
        {
            //Solucion a lo bruto
            //if ((c.Fila > TamTablero) || (c.Columna > TamTablero) || (c.Fila < 0) || (c.Columna < 0))

            if(casillasTablero.ContainsKey(c))
            {
                Console.WriteLine("La coordenada" + c + "está fuera de las dimensiones del tablero.");
            }
            else
            {
                coordenadasDisparadas.Add(c);

                foreach (Barco baux in barcos)
                {
                    baux.Disparo(c);
                }
            }
        }

        public string DibujarTablero()
        {
            string output = "CASILLAS TABLERO \n -------------- \n";

            Coordenada aux;

            for (int i = 0; i < TamTablero; i++)
            {
                for (int j = 0; j < TamTablero; j++)
                {
                    aux = new Coordenada(i, j);

                    output += "[" + casillasTablero[aux] + "]";
                }

                output += "\n";
            }

            return output;
        }

        public override string ToString()
        {
            string output = "";

            foreach (Barco baux in barcos)
            {
                output += baux;

                output += "\n";
            }

            output += "\n";

            output += "Coordenadas disparadas: ";

            foreach(Coordenada c in coordenadasDisparadas)
            {
                output += c;
            }

            output += "Coordenadas tocadas: ";

            foreach (Coordenada c in coordenadasTocadas)
            {
                output += c;
            }

            output += "\n\n\n";

            output += DibujarTablero();

            return output;
        }
    }
}
