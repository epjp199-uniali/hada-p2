using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Barco
    {
        public Dictionary<Coordenada, String> CoordenadasBarco
        {
            get;
            private set;
        }

        public string Nombre;

        int NumDanyos;

        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
        {

            Nombre = nombre;

            Coordenada aux;
            if (orientacion == 'h')
            {
                for(int i = coordenadaInicio.Columna;  i < coordenadaInicio.Columna + longitud; i++)
                {
                    aux = new Coordenada(coordenadaInicio.Fila, i);

                    CoordenadasBarco.Add(aux, Nombre);
                }
            }
            else if(orientacion == 'v')
            {
                for (int i = coordenadaInicio.Fila; i < coordenadaInicio.Fila + longitud; i++)
                {
                    aux = new Coordenada(i,coordenadaInicio.Columna);

                    CoordenadasBarco.Add(aux, Nombre);
                }
            }
            else
            {
                Console.WriteLine("Orientacion invalida, introduzca h o v");
            }
        }



    }
}
