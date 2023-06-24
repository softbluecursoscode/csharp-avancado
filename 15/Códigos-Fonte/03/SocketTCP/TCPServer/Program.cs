using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

class Program
{
    static void Main()
    {
        TcpListener server = new TcpListener(IPAddress.Loopback, 4000);
        server.Start();

        Console.WriteLine("Servidor iniciado");

        TcpClient client = server.AcceptTcpClient();
        NetworkStream ns = client.GetStream();

        StreamReader reader = new StreamReader(ns);
        StreamWriter writer = new StreamWriter(ns);
        writer.AutoFlush = true;

        while (true)
        {
            string line = reader.ReadLine();
            string lineUpper = line.ToUpper();

            writer.WriteLine(lineUpper);
        }
    }
}
