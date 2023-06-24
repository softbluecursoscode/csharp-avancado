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
            // Quantidade de números que será solicitada ao usuário.
            int qtdeNum = 5;

            // Lista onde os números serão armazenados.
            List<int> numeros = new List<int>();

            Console.WriteLine("Digite {0} números:\n", qtdeNum);

            // Solicita os números e guarda em uma lista.
            for (int i = 1; i <= 5; i++)
            {
                int num = LerNumero("Número " + i + " => ");
                numeros.Add(num);
            }

            // Solicita o nome do arquivo.
            Console.Write("\nNome do arquivo para gravar os dados => ");
            string nomeArquivo = Console.ReadLine();

            // Solicita o formato (binário ou XML)
            List<int> opcoes = new List<int>() { 1, 2 };
            int opcao = LerOpcao("Qual o formato do arquivo? (1-Binário, 2-XML) => ", opcoes);

            // Serializa os dados de acordo com a opção escolhida.
            if (opcao == 1)
            {
                SerializarBinario(nomeArquivo, numeros);
            }
            else if (opcao == 2)
            {
                SerializarXml(nomeArquivo, numeros);
            }

            Console.WriteLine("Serialização concluída!");
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

        // Serializa os elementos da lista para o formato binário.
        static void SerializarBinario(string nomeArquivo, List<int> numeros)
        {
            BinaryFormatter bf = new BinaryFormatter();

            using (FileStream fs = File.OpenWrite(nomeArquivo))
            {
                bf.Serialize(fs, numeros);
            }
        }

        // Serializa os elementos da lista para o formato XML.
        static void SerializarXml(string nomeArquivo, List<int> numeros)
        {
            XmlSerializer xmlSer = new XmlSerializer(typeof(List<int>));

            using (FileStream fs = File.OpenWrite(nomeArquivo))
            {
                xmlSer.Serialize(fs, numeros);
            }
        }
    }
}
