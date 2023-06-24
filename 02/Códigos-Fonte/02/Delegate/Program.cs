using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Semaforo s = new Semaforo();

        for (int i = 1; i <= 3; i++)
        {
            Carro c = new Carro(i);
            s.AdicionarCallback(c.SemaforoAlterado);
        }

        s.Iniciar();
    }
}

enum Cor
{
    VERDE,
    VERMELHO,
    AMARELO
}

delegate void SemaforoHandler(Cor cor);

class Semaforo
{
    Cor cor = Cor.VERMELHO;
    SemaforoHandler callbacks;

    public void Iniciar()
    {
        while (true)
        {
            if (cor == Cor.VERMELHO)
            {
                cor = Cor.VERDE;
            }
            else if (cor == Cor.VERDE)
            {
                cor = Cor.AMARELO;
            }
            else if (cor == Cor.AMARELO)
            {
                cor = Cor.VERMELHO;
            }

            Console.WriteLine("Semáforo mudou para " + cor);
            callbacks(cor);
            Thread.Sleep(2000);
        }
    }

    public void AdicionarCallback(SemaforoHandler handler)
    {
        callbacks += handler;
    }
}

class Carro
{
    int id;

    public Carro(int id)
    {
        this.id = id;
    }

    public void SemaforoAlterado(Cor cor)
    {
        Console.WriteLine("Carro {0:d} notificado: cor {1}", id, cor);
    }
}