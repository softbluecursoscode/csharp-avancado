using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<Aluno> alunos = CriarAlunos();

        var q = from a in alunos
                where a.Nota >= 5 && a.Nome.StartsWith("J")
                orderby a.Nota ascending
                select new { Inicial = a.Nome[0], Nota = a.Nota * 10 };

        foreach (var i in q)
        {
            Console.WriteLine(i);
        }
    }

    static List<Aluno> CriarAlunos()
    {
        List<Aluno> alunos = new List<Aluno>();
        alunos.Add(new Aluno() { Nome = "João", Nota = 9.5 });
        alunos.Add(new Aluno() { Nome = "Pedro", Nota = 4 });
        alunos.Add(new Aluno() { Nome = "Maria", Nota = 5 });
        alunos.Add(new Aluno() { Nome = "Joana", Nota = 6.5 });
        alunos.Add(new Aluno() { Nome = "Julia", Nota = 7 });

        return alunos;
    }
}

class Aluno
{
    public string Nome { get; set; }
    public double Nota { get; set; }

    public override string ToString()
    {
        return Nome + " -> " + Nota;
    }
}