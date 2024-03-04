using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Coordenada
    {

        //Atributos


        public int Fila;
        public int Columna;

        //Constructor por defecto
        public Coordenada()
        {
            Fila = -1;
            Columna = -1;
        }

        //Constructor con parametros int fila, int columna
        public Coordenada(int fila, int columna)
        {
            Fila = fila;
            Columna = columna;
        }

        //Constructor con parametros string fila, string columna
        public Coordenada(string fila, string columna)
        {
            try
            {
                Fila = int.Parse(fila);
                Columna = int.Parse(columna);
            }
            catch (Exception e)
            {
                throw new Exception("Datos de entrada incorrectos");
            }

        }

        //Constructor de copia
        public Coordenada(Coordenada a)
        {
            Fila = a.Fila;
            Columna = a.Columna;
        }
        
        //Metodo ToString(Define como se muestran los datos por pantalla)
        public override string ToString()
        {
            String output;

            output = "(" + Fila + "," + Columna + ")";

            return output;
        }

        //Metodo GetHashCode(codigo unico)
        public override int GetHashCode()
        {
            return this.Fila.GetHashCode() ^ this.Columna.GetHashCode();
        }

        //Metodo de comparacion con un objeto generico
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            return Equals((Coordenada)obj);
        }

        //Metodo de comparacion con un objeto coordenada
        public bool Equals(Coordenada obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (this.GetHashCode() != obj.GetHashCode())
            {
                return false;
            }

            System.Diagnostics.Debug.Assert(
            base.GetType() != typeof(object));

            if (!base.Equals(obj))
            {
                return false;
            }

            return ((this.Fila.Equals(obj.Fila)) && (this.Columna.Equals(obj.Columna)));
        }
    }
}
