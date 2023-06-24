using System;

namespace Softblue
{
    class Program
    {
        static void Main()
        {
            // Cria 2 vetores.
            Vector v1 = new Vector(2, 3);
            Vector v2 = new Vector(4, 5);
            
            // Soma os vetores.
            Vector v3 = v1 + v2;

            // Multiplica por 3.
            Vector v4 = v3 * 3;

            // Exibe as coordenadas.
            Console.WriteLine(v4);
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
        public static Vector operator+(Vector v1, Vector v2)
        {
            return new Vector(v1.x + v2.x, v1.y + v2.y);
        }

        // Multiplicação de um vetor por um escalar.
        public static Vector operator*(Vector v, int m)
        {
            return new Vector(v.x * m, v.y * m);
        }

        // Mostra as coordenadas do vetor
        public override string ToString()
        {
            return string.Format("({0}, {1})", x, y);
        }
    }
}
