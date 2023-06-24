using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Softblue
{
    class Program
    {
        static void Main()
        {
            // Solicita o nome do arquivo.
            Console.Write("Nome do arquivo para ler => ");
            string nomeArquivo = Console.ReadLine();

            // Verifica se o arquivo existe.
            if (!File.Exists(nomeArquivo))
            {
                Console.WriteLine("O arquivo " + nomeArquivo + " não existe");
                return;
            }

            // Solicita o formato para leitura.
            List<int> opcoes = new List<int>() { 1, 2 };
            int opcao = LerOpcao("Qual o formato do arquivo? (1-Binário, 2-XML) => ", opcoes);

            List<int> numeros;

            // Faz o processo de deserialização de acordo com o formato escolhido.
            if (opcao == 1)
            {
                numeros = DeserializarBinario(nomeArquivo);
            }
            else
            {
                numeros = DeserializarXml(nomeArquivo);
            }

            Console.WriteLine("\nDados do arquivo:");

            // Mostra os números na tela.
            foreach(int numero in numeros)
            {
                Console.WriteLine(numero);
            }
        }

        // Lê uma opção do console. Apenas números dentro da lista 'opcoes' são opções válidas. Fica em loop enquanto a opção for inválida.
        static int LerOpcao(string msg, List<int> opcoes)
        {
            int opcao;
            do
            {
                opcao = LerNumero(msg);
            }
            while (!opcoes.Contains(opcao));

            return opcao;
        }

        // Lê um número do console. Enquanto não for um número válido, fica em loop solicitando o número.
        static int LerNumero(string msg)
        {
            while (true)
            {
                Console.Write(msg);
                try
                {
                    return int.Parse(Console.ReadLine());
                }
                catch
                {
                }
            }
        }

        // Deserializa os elementos da lista no formato binário.
        static List<int> DeserializarBinario(string nomeArquivo)
        {
            BinaryFormatter bf = new BinaryFormatter();

            using (FileStream fs = File.OpenRead(nomeArquivo))
            {
                return (List<int>)bf.Deserialize(fs);
            }
        }

        // Deserializa os elementos da lista no formato XML.
        static List<int> DeserializarXml(string nomeArquivo)
        {
            XmlSerializer xmlSer = new XmlSerializer(typeof(List<int>));

            using (FileStream fs = File.OpenRead(nomeArquivo))
            {
                return (List<int>)xmlSer.Deserialize(fs);
            }
        }
    }
}
