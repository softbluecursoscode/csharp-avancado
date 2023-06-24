using System;
using System.IO;
using System.Net;

class Program
{
    static void Main()
    {
        //string url = "http://www.softblue.com.br/public/images/sbv2_logotipo.png";
        //WebClient wc = new WebClient();
        //wc.DownloadFile(url, "imagem.png");

        UriBuilder builder = new UriBuilder();
        builder.Scheme = "http";
        builder.Port = 80;
        builder.Host = "www.google.com";

        WebRequest req = WebRequest.Create(builder.Uri);

        // Chamada síncrona.
        //HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

        // Chamada assíncrona.
        req.BeginGetResponse(OnResponseCompleted, req);

        Console.ReadLine();
    }

    static void OnResponseCompleted(IAsyncResult ar)
    {
        WebRequest req = (WebRequest)ar.AsyncState;
        HttpWebResponse resp = (HttpWebResponse)req.EndGetResponse(ar);

        Console.WriteLine("Status: " + resp.StatusDescription);
        Console.WriteLine("Server: " + resp.Server);
        Console.WriteLine("Headers:\n" + resp.Headers);

        Stream s = resp.GetResponseStream();
        StreamReader sr = new StreamReader(s);
        string content = sr.ReadToEnd();

        Console.WriteLine(content);
    }
}
