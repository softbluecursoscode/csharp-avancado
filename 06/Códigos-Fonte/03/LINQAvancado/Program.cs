using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<Aluno> alunos = CriarAlunos();
        List<Familia> familias = CriarFamilias();

        //var q = from a in alunos
        //        where a.Turma.Serie == 3 && a.Turma.Letra == 'A'
        //        orderby a.Nota descending
        //        select a;

        //var q = from a in alunos
        //        from e in a.Atividades
        //        where e.Nome == "Xadrez"
        //        select a;

        //var q = from a in alunos
        //        from e in a.Atividades
        //        group e by e.Nome into g
        //        select new { Atividade = g.Key, Qtde = g.Count() };

        //var q = from a in alunos
        //        join f in familias on a.Nome equals f.Filho
        //        select new { Pai = f.Pai, Nota = a.Nota };

        //var q = from a in alunos
        //        join f in familias on a.Nome equals f.Filho into af
        //        from f in af.DefaultIfEmpty()
        //        select new { Pai = f == null ? "-" : f.Pai, Nota = a.Nota };

        var q = from a in alunos
                where a.Turma.Serie == 3 && a.Turma.Letra == 'A'
                select a.Nota;

        //Console.WriteLine("Média: " + q.Average());
        //Console.WriteLine("Mínima: " + q.Min());
        //Console.WriteLine("Máxima: " + q.Max());

        foreach (var i in q)
        {
            Console.WriteLine(i);
        }
    }

    static List<Aluno> CriarAlunos()
    {
        Turma t1 = new Turma() { Serie = 2, Letra = 'B' };
        Turma t2 = new Turma() { Serie = 3, Letra = 'A' };

        AtividadeExtra a1 = new AtividadeExtra() { Nome = "Judô" };
        AtividadeExtra a2 = new AtividadeExtra() { Nome = "Balé" };
        AtividadeExtra a3 = new AtividadeExtra() { Nome = "Xadrez" };

        List<Aluno> alunos = new List<Aluno>();
        alunos.Add(new Aluno(a1) { Nome = "João", Nota = 9.5, Turma = t1 });
        alunos.Add(new Aluno(a1, a3) { Nome = "Pedro", Nota = 4, Turma = t1 });
        alunos.Add(new Aluno(a2, a3) { Nome = "Maria", Nota = 5, Turma = t2 });
        alunos.Add(new Aluno(a2) { Nome = "Joana", Nota = 6.5, Turma = t2 });
        alunos.Add(new Aluno(a2) { Nome = "Julia", Nota = 7, Turma = t2 });

        return alunos;
    }

    static List<Familia> CriarFamilias()
    {
        List<Familia> familias = new List<Familia>();
        familias.Add(new Familia() { Filho = "João", Pai = "Augusto", Mae = "Mariana" });
        familias.Add(new Familia() { Filho = "Pedro", Pai = "José", Mae = "Bianca" });
        familias.Add(new Familia() { Filho = "Maria", Pai = "Sérgio", Mae = "Rita" });
        familias.Add(new Familia() { Filho = "Joana", Pai = "Joaquim", Mae = "Rose" });
        return familias;
    }
}

class Aluno
{
    public string Nome { get; set; }
    public double Nota { get; set; }
    public Turma Turma { get; set; }
    public List<AtividadeExtra> Atividades { get; set; }

    public Aluno(params AtividadeExtra[] atividades)
    {
        Atividades = atividades.ToList();
    }

    public override string ToString()
    {
        string a = ""; 
        Atividades.ForEach(atividade => a += atividade.Nome + " ");
        return string.Format("{0} -> {1} ({2}) -> {3}", Nome, Nota, Turma, a);
    }
}

class Turma
{
    public int Serie { get; set; }
    public char Letra { get; set; }

    public override string ToString()
    {
        return "" + Serie + Letra;
    }
}

class AtividadeExtra
{
    public String Nome { get; set; }

    public override string ToString()
    {
        return Nome;
    }
}

class Familia
{
    public string Pai { get; set; }
    public string Mae { get; set; }
    public string Filho { get; set; }

    public override string ToString()
    {
        return string.Format("{0} / {1}: {2}");
    }
}