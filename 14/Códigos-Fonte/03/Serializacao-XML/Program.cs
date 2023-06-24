using System;
using System.IO;
using System.Xml.Serialization;

class Program
{
    static void Main()
    {
        Serialize();
        Deserialize();
    }

    static void Serialize()
    {
        Esporte e = new Esporte();
        e.Nome = "Judô";
        e.Individual = true;

        Atleta a = new Atleta();
        a.Nome = "José Silva";
        a.Pais = "Brasil";
        a.Esporte = e;

        XmlSerializer ser = new XmlSerializer(typeof(Atleta));
        using (FileStream fs = File.OpenWrite("atleta.xml"))
        {
            ser.Serialize(fs, a);
        }

    }

    static void Deserialize()
    {
        XmlSerializer ser = new XmlSerializer(typeof(Atleta));
        using (FileStream fs = File.OpenRead("atleta.xml"))
        {
            Atleta a = (Atleta)ser.Deserialize(fs);
            Console.WriteLine(a);
        }
    }
}

[XmlRoot(ElementName = "AtletaEsporte")]
public class Atleta
{
    public string Nome { get; set; }

    [XmlAttribute]
    public string Pais { get; set; }
    
    public Esporte Esporte { get; set; }

    public override string ToString()
    {
        return string.Format("{0}, {1}, {2}", Nome, Pais, Esporte);
    }
}

public class Esporte
{
    public string Nome { get; set; }

    [XmlElement(ElementName = "Ind")]
    public bool Individual { get; set; }

    public override string ToString()
    {
        return string.Format("{0}, {1}", Nome, Individual);
    }
}