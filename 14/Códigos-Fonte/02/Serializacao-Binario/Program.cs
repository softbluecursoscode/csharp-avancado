using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

        BinaryFormatter bf = new BinaryFormatter();

        using (FileStream fs = File.OpenWrite("atleta.bin"))
        {
            bf.Serialize(fs, a);
        }
    }

    static void Deserialize()
    {
        BinaryFormatter bf = new BinaryFormatter();

        using (FileStream fs = File.OpenRead("atleta.bin"))
        {
            Atleta a = (Atleta)bf.Deserialize(fs);
            Console.WriteLine(a);
        }
    }
}

[Serializable]
class Atleta
{
    public string Nome { get; set; }
    public string Pais { get; set; }
    public Esporte Esporte { get; set; }

    public override string ToString()
    {
        return string.Format("{0}, {1}, {2}", Nome, Pais, Esporte);
    }
}

[Serializable]
class Esporte
{
    public string Nome { get; set; }
    public bool Individual { get; set; }

    public override string ToString()
    {
        return string.Format("{0}, {1}", Nome, Individual);
    }
}
