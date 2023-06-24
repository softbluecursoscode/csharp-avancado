using System;
using System.IO;
using System.Net.Sockets;

class Program
{
    static void Main()
    {
        TcpClient client = new TcpClient("localhost", 4000);

        NetworkStream ns = client.GetStream();
        StreamReader reader = new StreamReader(ns);
        StreamWriter writer = new StreamWriter(ns);
        writer.AutoFlush = true;

        while (true)
        {
            Console.Write("Texto para enviar: ");
            string texto = Console.ReadLine();

            writer.WriteLine(texto);

            string textoConvertido = reader.ReadLine();
            Console.WriteLine("Texto convertido: " + textoConvertido);
        }
    }
}
