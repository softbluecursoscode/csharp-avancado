using System;
using System.Linq;
using System.Xml.Linq;

namespace Softblue
{
    class Program
    {
        static void Main()
        {
            // Lê os dados do arquivo XML.
            XElement xml = XElement.Load("dados.xml");

            // Expressão LINQ que retorna o nome dos cantores em ordem alfabética.
            // O uso do 'let' define uma variável, que pode ser referenciada em outros pontos da expressão.
            var q1 = from e in xml.Elements("Cantor")
                     let nome = e.Element("Nome").Value
                     orderby nome
                     select nome;

            // Expressão LINQ que retorna os cantores nascidos em maio. Um objeto DateTime é criado para
            // facilitar a identificação do mês 5.
            var q2 = from e in xml.Elements("Cantor")
                    let nome = e.Element("Nome").Value
                    let data = DateTime.Parse(e.Element("DataNascimento").Value)
                    where data.Month == 5
                    orderby nome
                    select new
                    {
                        Nome = nome,
                        Data = data
                    };

            // Exibe a coleção resultante da expressão 1
            Console.WriteLine("1)");
            foreach (var c in q1)
            {
                Console.WriteLine(c);
            }

            // Exibe a coleção resultante da expressão 2
            Console.WriteLine("\n2)");
            foreach (var c in q2)
            {
                Console.WriteLine(c);
            }
        }
    }
}
