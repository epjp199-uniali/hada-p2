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

            //Aqui van eventos
        }

        public string Nombre;

        int NumDanyos;

        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
        {

            Nombre = nombre;
            NumDanyos = 0;

            Coordenada aux;
            if (orientacion == 'h')
            {
                try
                {
                    for (int i = coordenadaInicio.Columna; i < coordenadaInicio.Columna + longitud; i++)
                    {
                        aux = new Coordenada(coordenadaInicio.Fila, i);

                        CoordenadasBarco.Add(aux, Nombre);
                    }
                }
                catch(ArgumentException)
                {
                    Console.WriteLine("Coordenadas ocupadas");
                }
                
            }
            else if(orientacion == 'v')
            {
                try
                {
                    for (int i = coordenadaInicio.Fila; i < coordenadaInicio.Fila + longitud; i++)
                    {
                        aux = new Coordenada(i, coordenadaInicio.Columna);

                        CoordenadasBarco.Add(aux, Nombre);
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Coordenadas ocupadas");
                }
            }
            else
            {
                Console.WriteLine("Orientacion invalida, introduzca h o v");
            }
        }

        public void Disparo(Coordenada Boom)
        {
            if (CoordenadasBarco.ContainsKey(Boom))
            {
                CoordenadasBarco[Boom] = CoordenadasBarco[Boom] + "_T";

                //Aqui va un evento de disparo

                NumDanyos++;

            }
        }

        public bool hundido()
        {
            bool hundido = true;

            foreach (var b in CoordenadasBarco)
            {
                if(b.Value == Nombre)
                {
                    hundido = true;
                }
            }


            return hundido;
        }



    }
}
