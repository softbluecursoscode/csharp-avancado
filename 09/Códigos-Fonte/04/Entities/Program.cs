using System;
using App;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        using (AppEntities context = new AppEntities())
        {
            CadastrarAluno(context);
            CarregarAluno(context);
            AlterarAluno(context);
            ExcluirAluno(context);
            CadastrarAlunoComNotas(context);
            FiltrarInformacoes(context);
            CarregarAlunoLazyEager(context);
        }
    }

    static void CadastrarAluno(AppEntities context)
    {
        Aluno aluno = context.Aluno.Create();
        aluno.Nome = "José";
        context.Aluno.Add(aluno);

        context.SaveChanges();
    }

    static void CarregarAluno(AppEntities context)
    {
        Aluno aluno = context.Aluno.Find(1);
        Console.WriteLine(aluno.Nome);
    }

    static void AlterarAluno(AppEntities context)
    {
        Aluno aluno = context.Aluno.Find(1);
        aluno.Nome = "Pedro";
        context.SaveChanges();
    }

    static void ExcluirAluno(AppEntities context)
    {
        Aluno aluno = context.Aluno.Find(1);
        context.Aluno.Remove(aluno);
        context.SaveChanges();
    }

    static void CadastrarAlunoComNotas(AppEntities context)
    {
        Aluno aluno = context.Aluno.Create();
        aluno.Nome = "Paulo";
        context.Aluno.Add(aluno);

        Nota n1 = context.Nota.Create();
        n1.Materia = "Matemática";
        n1.Valor = 8.5;
        n1.Aluno = aluno;
        context.Nota.Add(n1);

        Nota n2 = context.Nota.Create();
        n2.Materia = "Português";
        n2.Valor = 7;
        n2.Aluno = aluno;
        context.Nota.Add(n2);

        context.SaveChanges();
    }

    static void FiltrarInformacoes(AppEntities context)
    {
        var q1 = from a in context.Aluno
                 select a.Nome;

        var q2 = from n in context.Nota
                 where n.Valor >= 7
                 select n.Aluno.Nome;

        var q3 = from n in context.Nota
                 where n.Aluno.Nome == "Otávio"
                 select n;

        var q4 = from a in context.Aluno
                 from n in a.Notas
                 where a.Nome == "Otávio"
                 select n;

        foreach (var i in q4)
        {
            Console.WriteLine(i);   
        }
    }

    static void CarregarAlunoLazyEager(AppEntities context)
    {
        //Aluno aluno = context.Aluno.Find(3);

        Aluno aluno = (from a in context.Aluno.Include("Notas")
                   where a.Id == 3
                   select a).ToList()[0];
        
        Console.WriteLine(aluno.Nome);

        ICollection<Nota> notas = aluno.Notas;

        foreach (Nota n in notas)
        {
            Console.WriteLine("\t" + n.Materia + " - " + n.Valor);
        }
    }
}
