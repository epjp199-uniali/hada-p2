using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Tablero
    {

        public event EventHandler<EventArgs> eventoFinPartida;


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

        private List<Coordenada> coordenadasDisparadas = new List<Coordenada>();

        private List<Coordenada> coordenadasTocadas = new List<Coordenada>();

        private List<Barco> barcos;

        private List<Barco> barcosEliminados = new List<Barco>();

        private Dictionary<Coordenada, string> casillasTablero;


        public Tablero(int tamTablero, List<Barco> barcos)
        {
            TamTablero = tamTablero;

            this.barcos = barcos;

            casillasTablero = new Dictionary<Coordenada, string>();

            inicializaCasillasTablero();

            foreach (Barco barco in barcos)
            {
                barco.eventoTocado += cuandoEventoTocado;
                barco.eventoHundido += cuandoEventoHundido;
            }

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
                            if ((aux.Equals(caux)) && (barcofound == false))
                            {
                                if (!casillasTablero.ContainsKey(aux))
                                {
                                    casillasTablero.Add(aux, baux.CoordenadasBarco[aux]);
                                }
                                else
                                {
                                    casillasTablero[aux] = baux.CoordenadasBarco[aux];
                                }

                                barcofound = true;
                            }
                        }
                    }

                    if (barcofound == false) 
                    {
                        if (!casillasTablero.ContainsKey(aux))
                        {
                            casillasTablero.Add(aux, "AGUA");
                        }
                        else
                        {
                            casillasTablero[aux] = "AGUA";
                        }
   
                    }

                    barcofound = false;

                }
            }

 
        }

        public void Disparar(Coordenada c)
        {
                        
            if(!casillasTablero.ContainsKey(c))
            {
                Console.WriteLine("La coordenada " + c + " está fuera de las dimensiones del tablero. \n");
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

            output += "\n";

            output += "Coordenadas tocadas: ";

            foreach (Coordenada c in coordenadasTocadas)
            {
                output += c;
            }

            output += "\n";

            output += "\n\n\n";

            output += DibujarTablero();

            return output;
        }


        private void cuandoEventoTocado(object sender, TocadoArgs e)
        {
            coordenadasTocadas.Add(e.coordenadaImpacto);

            casillasTablero[e.coordenadaImpacto] = e.etiqueta;

            Console.WriteLine("\nTABLERO: Barco " + e.nombre + " tocado en Coordenada: " + "[" + e.coordenadaImpacto + "] \n");
        }

        private void cuandoEventoHundido(object sender, HundidoArgs e)
        {
            bool todoshundidos = true;

            foreach(Barco barco in barcos)
            {
                if(barco.Nombre == e.nombre)
                {
                    barcosEliminados.Add(barco);
                }
            }
            
            Console.WriteLine("\nTABLERO: Barco " + e.nombre + " hundido!! \n");

            foreach (Barco baux in barcos)
            {
                if (!baux.hundido()){ todoshundidos = false; }
            }

            if(todoshundidos == true)
            {
                eventoFinPartida(this, new EventArgs());
            }
        }
    }
}
