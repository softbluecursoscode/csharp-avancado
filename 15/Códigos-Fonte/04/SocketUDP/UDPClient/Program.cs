using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        UdpClient client = new UdpClient();

        while (true)
        {
            Console.Write("Texto para enviar: ");
            string texto = Console.ReadLine();

            byte[] bytes = Encoding.ASCII.GetBytes(texto);

            client.Send(bytes, bytes.Length, "localhost", 4000);

            IPEndPoint serverIp = new IPEndPoint(0, 0);
            bytes = client.Receive(ref serverIp);

            string textoConvertido = Encoding.ASCII.GetString(bytes);
            Console.WriteLine(textoConvertido);
        }
    }
}
