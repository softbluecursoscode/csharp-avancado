using System;

class Program
{
    static void Main()
    {
        Fracao f1 = new Fracao(3, 2);
        Fracao f2 = new Fracao(6, 4);

        Console.WriteLine(f1 == f2);
    }
}

class Fracao : IEquatable<Fracao>
{
    public int Numerador { get; set; }
    public int Denominador { get; set; }

    public double Resultado
    {
        get
        {
            return (double)Numerador / Denominador;
        }
    }

    public Fracao(int numerador, int denominador)
    {
        this.Numerador = numerador;
        this.Denominador = denominador;
    }

    public override string ToString()
    {
        return string.Format("{0:d}/{1:d}", Numerador, Denominador);
    }

    public static Fracao operator *(Fracao f1, Fracao f2)
    {
        return new Fracao(f1.Numerador * f2.Numerador, f1.Denominador * f2.Denominador);
    }

    public static Fracao operator *(Fracao f, int n)
    {
        return new Fracao(f.Numerador * n, f.Denominador);
    }

    public static bool operator <(Fracao f1, Fracao f2)
    {
        return f1.Resultado < f2.Resultado;
    }

    public static bool operator >(Fracao f1, Fracao f2)
    {
        return f1.Resultado > f2.Resultado;
    }

    public static bool operator <=(Fracao f1, Fracao f2)
    {
        return f1.Resultado <= f2.Resultado;
    }

    public static bool operator >=(Fracao f1, Fracao f2)
    {
        return f1.Resultado >= f2.Resultado;
    }

    public bool Equals(Fracao other)
    {
        return this.Resultado == other.Resultado;
    }

    public override bool Equals(object obj)
    {
        Fracao f = obj as Fracao;
        if (f != null)
        {
            return Equals(f);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Resultado.GetHashCode();
    }

    public static bool operator ==(Fracao f1, Fracao f2)
    {
        return f1.Equals(f2);
    }

    public static bool operator !=(Fracao f1, Fracao f2)
    {
        return !f1.Equals(f2);
    }
}
