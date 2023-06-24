using System;

namespace Softblue
{
    class Program
    {
        static void Main()
        {
            // Cria uma pessoa.
            Pessoa pessoa = new Pessoa();
            pessoa.Peso = 80;
            pessoa.Altura = 1.8;

            // Calcula o IMC usando uma expressão lambda
            double imc = pessoa.CalcularImc((p, a) => p / (a * a));

            Console.WriteLine(imc);
        }
    }

    // Delegate que vai referenciar o método para cálculo do IMC.
    delegate double Calculo(double peso, double altura);

    // Pessoa.
    class Pessoa
    {
        public double Peso { get; set; }
        public double Altura { get; set; }

        // Método para calcular o IMC.
        public double CalcularImc(Calculo calculo)
        {
            return calculo(Peso, Altura);
        }
    }
}
