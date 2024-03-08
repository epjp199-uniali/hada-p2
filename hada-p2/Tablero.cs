using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Tablero
    {
        //Declaracion de Eventos FinPartida
        public event EventHandler<EventArgs> eventoFinPartida;

        //Campo de respaldo
        private int TableroDimension;

        //Atributos
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

        //Constructor
        public Tablero(int tamTablero, List<Barco> barcos)
        {
            TamTablero = tamTablero;

            this.barcos = barcos;

            casillasTablero = new Dictionary<Coordenada, string>();

            inicializaCasillasTablero();

            //Este proceso sirve para suscribirse a los eventos de los barcos
            foreach (Barco barco in barcos)
            {
                barco.eventoTocado += cuandoEventoTocado;
                barco.eventoHundido += cuandoEventoHundido;
            }

        }

        //Metodo para inicializar las casillas del tablero, incluido la posicion de los barcos en este mismo
        private void inicializaCasillasTablero()
        {
            Coordenada aux;
            bool barcofound = false;

            //Doble bucle para recorrer cada coordenada del tablero
            for (int i = 0; i < TamTablero; i++)
            {
                for (int j = 0; j < TamTablero; j++)
                {

                    aux = new Coordenada(i, j);

                    //Con este doble bucle recorreremos todas las coordenadas ocupadas por barcos
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

                    //La variable barcofound nos permite saber si hay un barco en esa posicion
                    //asi evitaremos sobreescribir

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

        //Metodo que se usara para ver si se ha acertado a algun barco
        public void Disparar(Coordenada c)
        {
                        
            if(!casillasTablero.ContainsKey(c))
            {
                Console.WriteLine("La coordenada " + c + " está fuera de las dimensiones del tablero. \n");
            }
            else
            {
                coordenadasDisparadas.Add(c);
                
                //Pasamos por cada barco buscando si alguno ha recibido el disparo
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

        //CONTROL DE EVENTOS

        //Metodo que controla el evento Tocado
        private void cuandoEventoTocado(object sender, TocadoArgs e)
        {
            coordenadasTocadas.Add(e.coordenadaImpacto);

            casillasTablero[e.coordenadaImpacto] = e.etiqueta;

            Console.WriteLine("\nTABLERO: Barco " + e.nombre + " tocado en Coordenada: " + "[" + e.coordenadaImpacto + "] \n");
        }

        //Metodo que controla el evento Hundido
        private void cuandoEventoHundido(object sender, HundidoArgs e)
        {
            //Suponemos que todos estan hundidos, luego veremos si es cierto
            bool todoshundidos = true;

            foreach(Barco barco in barcos)
            {
                if(barco.Nombre == e.nombre)
                {
                    barcosEliminados.Add(barco);
                }
            }
            
            Console.WriteLine("\nTABLERO: Barco " + e.nombre + " hundido!! \n");

            //Esta ultima parte se usa para comprobar si todos los barcos estan hundidos

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
