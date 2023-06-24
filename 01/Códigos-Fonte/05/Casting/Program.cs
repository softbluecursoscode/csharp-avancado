using System;

class Program
{
    static void Main()
    {
        LetraAlfabeto la = new LetraAlfabeto('B');

        char c = (char)la;
        Console.WriteLine(c);

        LetraAlfabeto la2 = 'X';
    }
}

class LetraAlfabeto
{
    char caractere;

    public LetraAlfabeto(char caractere)
    {
        this.caractere = char.ToUpper(caractere);
    }

    public static explicit operator char(LetraAlfabeto la)
    {
        return la.caractere;
    }

    public static implicit operator LetraAlfabeto(char c)
    {
        return new LetraAlfabeto(c);
    }
}
