using System;

namespace Softblue
{
    class Program
    {
        static void Main()
        {
            // Cria um vetor e atribui as coordenadas.
            Vector v = new Vector();
            v['X'] = 5;
            v['Y'] = 7;
            int x = v['X'];
            int y = v['Y'];
            Console.WriteLine("X = " + x + ", Y = " + y);

            // Faz o casting para string.
            string s = v;

            Console.WriteLine(s);
        }
    }

    public struct Vector
    {
        // Coordenadas.
        private int x;
        private int y;

        public Vector(int x, int y)
            : this()
        {
            this.x = x;
            this.y = y;
        }

        // Soma de vetores.
        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.x + v2.x, v1.y + v2.y);
        }

        // Multiplicação de um vetor por um escalar.
        public static Vector operator *(Vector v, int m)
        {
            return new Vector(v.x * m, v.y * m);
        }

        // Indexador com base em 'X' ou 'Y'.
        public int this[char coord]
        {
            get
            {
                if (coord == 'X')
                {
                    return x;
                }
                else if (coord == 'Y')
                {
                    return y;
                }

                throw new ArgumentException("Coordenada inválida");
            }
            set
            {
                if (coord == 'X')
                {
                    x = value;
                }
                else if (coord == 'Y')
                {
                    y = value;
                }
                else
                {
                    throw new ArgumentException("Coordenada inválida");
                }
            }
        }

        // Casting implícito customizado para converter de Vector para string.
        public static implicit operator string(Vector v)
        {
            return string.Format("({0}, {1})", v.x, v.y);
        }
    }
}
