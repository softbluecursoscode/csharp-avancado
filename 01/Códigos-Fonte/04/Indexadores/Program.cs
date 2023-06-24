using System;

class Program
{
    static void Main()
    {
        Temperaturas t = new Temperaturas();
        Console.WriteLine(t[12]);
        t[12] = 30;
        Console.WriteLine(t[12]);

        Console.WriteLine(t["Dez"]);
    }
}

class Temperaturas
{
    int[] temperaturas = new int[] { 30, 31, 29, 27, 22, 15, 16, 19, 23, 26, 27, 28 };

    public int this[int mes]
    {
        get
        {
            return temperaturas[mes - 1];
        }
        set
        {
            temperaturas[mes - 1] = value;
        }
    }

    public int this[string mes]
    {
        get
        {
            switch (mes)
            {
                case "Jan": return temperaturas[0];
                case "Fev": return temperaturas[1];
                case "Mar": return temperaturas[2];
                case "Abr": return temperaturas[3];
                case "Mai": return temperaturas[4];
                case "Jun": return temperaturas[5];
                case "Jul": return temperaturas[6];
                case "Ago": return temperaturas[7];
                case "Set": return temperaturas[8];
                case "Out": return temperaturas[9];
                case "Nov": return temperaturas[10];
                case "Dez": return temperaturas[11];
                default: return -1;
            }
        }
    }
}