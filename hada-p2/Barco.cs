using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Barco
    {

        public event EventHandler<TocadoArgs> eventoTocado;
        public event EventHandler<HundidoArgs> eventoHundido;

        //Declaracion de Diccionario
        public Dictionary<Coordenada, String> CoordenadasBarco
        {
            get;
            private set;
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

            if ((CoordenadasBarco.ContainsKey(Boom))&&(CoordenadasBarco[Boom] == Nombre + "_T"))
            {
                CoordenadasBarco[Boom] = Nombre + "_T";

                //Aqui va un evento de tocado
                eventoTocado(this, new TocadoArgs(Nombre, Boom, CoordenadasBarco[Boom]));

                NumDanyos++;

                //Comprobar si barco hundido, si lo esta, evento hundido
                if (hundido())
                {
                    eventoHundido(this, new HundidoArgs(Nombre));
                }
               
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
                    hundido = false;
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
