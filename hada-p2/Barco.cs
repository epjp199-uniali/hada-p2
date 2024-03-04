using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Barco
    {

        //Comentado para que compile

        //public event EventHandler<string g> eventoTocado;
        //public event EventHandler<HundidoArgs> eventoHundido;

        //Declaracion de Diccionario
        public Dictionary<Coordenada, String> CoordenadasBarco
        {
            get;
            private set;

            //Aqui van eventos
        }

        //Nombre del barco
        public string Nombre;

        //Numero de daño
        public int NumDanyos;

        //Constructor
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

        //Metodo de disparo
        public void Disparo(Coordenada Boom)
        {
            if (CoordenadasBarco.ContainsKey(Boom))
            {
                CoordenadasBarco[Boom] = CoordenadasBarco[Boom] + "_T";

                //Aqui va un evento de tocado

                NumDanyos++;

                //Comprobar si barco hundido, si lo esta, evento hundido
            }
        }

        //Metodo de hundido
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

        public override string ToString()
        {
            String output;
            String dictouput = "";

            foreach (Coordenada c in CoordenadasBarco.Keys)
            {
                dictouput = dictouput + "[" + c + " :" + CoordenadasBarco[c] + "] ";
            }

            output = "[" + Nombre + "] - DAÑOS: [" + NumDanyos + "] - HUNDIDO: [" + hundido() + "] COORDENADAS: [" + CoordenadasBarco + "]"; 

            return output;
        }

    }
}
