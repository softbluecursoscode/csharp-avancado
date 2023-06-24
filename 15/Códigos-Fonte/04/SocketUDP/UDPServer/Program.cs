using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        UdpClient server = new UdpClient(4000);

        while (true)
        {
            IPEndPoint clientIP = new IPEndPoint(0, 0);
            byte[] bytes = server.Receive(ref clientIP);

            string text = Encoding.ASCII.GetString(bytes);
            string textUpper = text.ToUpper();

            bytes = Encoding.ASCII.GetBytes(textUpper);

            server.Send(bytes, bytes.Length, clientIP);
        }
    }
}
