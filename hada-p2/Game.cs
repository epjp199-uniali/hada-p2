using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Game
    {
        //Atributos
        private bool finPartida = false;

        //Constructor
        public Game()
        {
            gameLoop();
        }

        //Metodo que controlara el evento fin de partida
        private void eventoFinPartida(object sender, EventArgs e)
        {
            finPartida = true;
        }

        private void gameLoop()
        {

            String jugadortext;

            bool coordenadacorrect = false;

            List<Barco> barcos = new List<Barco>();
            
            //Añadimos varios barcos a la lista de barcos

            barcos.Add(new Barco("PEPE", 3, 'v', new Coordenada(0, 0)));
            barcos.Add(new Barco("JUAN", 1, 'h', new Coordenada(7, 6)));
            barcos.Add(new Barco(">:'3", 2, 'v', new Coordenada(0, 6)));


           
           Tablero table = new Tablero(9, barcos);

            //Suscribimos nuestro evento controlador al evento de tablero "eventoFinPartida"
            table.eventoFinPartida += eventoFinPartida;

            while (!finPartida)
            {
                coordenadacorrect = false;

                while (!coordenadacorrect)
                {
                    Console.WriteLine("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para Salir): ");
                    jugadortext = Console.ReadLine();

                    if ((jugadortext == "s") || (jugadortext == "S")) 
                    { 
                        finPartida = true;
                        coordenadacorrect = true;
                        break;
                    }
                    else if (jugadortext.Length == 3)
                    {
                        if ((jugadortext[1] == ',') && (Char.IsNumber(jugadortext[0])) && (Char.IsNumber(jugadortext[2])))
                        {
                            table.Disparar(new Coordenada((jugadortext[0] - '0'), (jugadortext[2] - '0')));

                            coordenadacorrect = true;

                            Console.WriteLine(table);
                        }
                    }

                    
                }
                

            }


        }
    }
}
