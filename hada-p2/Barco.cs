using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Barco
    {

        //Declaracion de Eventos Tocado y Hundido
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

            CoordenadasBarco = new Dictionary<Coordenada,String>();

            Coordenada aux;

            //Orientacion horizontal
            if (orientacion == 'h')
            {
                try
                {
                    for (int i = coordenadaInicio.Columna; i < coordenadaInicio.Columna + longitud; i++)
                    {
                        aux = new Coordenada(coordenadaInicio.Fila, i);

                        if (!CoordenadasBarco.ContainsKey(aux))
                        {
                            CoordenadasBarco.Add(aux, Nombre);  
                        }
                        else
                        {
                            CoordenadasBarco[aux] = Nombre;
                        }
                    }
                }
                catch(ArgumentException)
                {
                    throw new Exception("Coordenadas ocupadas");
                }
                
            }
            //Orientación vertical
            else if(orientacion == 'v')
            {
                try
                {
                    for (int i = coordenadaInicio.Fila; i < coordenadaInicio.Fila + longitud; i++)
                    {
                        aux = new Coordenada(i, coordenadaInicio.Columna);


                        if (!CoordenadasBarco.ContainsKey(aux))
                        {
                            CoordenadasBarco.Add(aux, Nombre);
                        }
                        else
                        {
                            CoordenadasBarco[aux] = Nombre;
                        }
                    }
                }
                catch (ArgumentException)
                {
                    throw new Exception("Coordenadas ocupadas");
                }
            }
            else
            {
                throw new Exception("Orientacion invalida, introduzca h o v");
            }
        }

        //Metodo de disparo
        public void Disparo(Coordenada Boom)
        {

            if ((CoordenadasBarco.ContainsKey(Boom))&&(CoordenadasBarco[Boom] != Nombre + "_T"))
            {
                //Le añadimos a la etiqueta el _T
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
            //Suponemos que todos los estan
            bool hundido = true;

            //Comprobamos si alguno de los barcos NO esta hundido
            foreach (var b in CoordenadasBarco)
            {
                if(b.Value == Nombre)
                {
                    hundido = false; //Si alguno no esta hundido, significa que no lo estan todos
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
                dictouput = dictouput + "[" + c + " : " + CoordenadasBarco[c] + "] ";
            }

            output = "[" + Nombre + "] - DAÑOS: [" + NumDanyos + "] - HUNDIDO: [" + hundido() + "] COORDENADAS: " + dictouput ; 

            return output;
        }

    }
}
