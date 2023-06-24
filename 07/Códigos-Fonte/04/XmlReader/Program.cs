using System;
using System.Xml;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        XmlReader reader = XmlReader.Create("colaboradores.xml");

        List<Colaborador> colaboradores = new List<Colaborador>();
        Colaborador colaborador = null;

        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.Element && reader.Name == "colaborador")
            {
                colaborador = new Colaborador();
                colaboradores.Add(colaborador);

                colaborador.Codigo = int.Parse(reader.GetAttribute("codigo"));
                colaborador.Tipo = (Tipo)Enum.Parse(typeof(Tipo), reader.GetAttribute("tipo"));

            }
            else if (reader.NodeType == XmlNodeType.Element && reader.Name == "nome")
            {
                colaborador.Nome = reader.ReadElementContentAsString();
            }
            else if (reader.NodeType == XmlNodeType.Element && reader.Name == "idade")
            {
                colaborador.Idade = reader.ReadElementContentAsInt();
            }
        }

        foreach (Colaborador c in colaboradores)
        {
            Console.WriteLine(c);
        }
    }
}

enum Tipo
{
    Funcionario,
    Terceirizado
}

class Colaborador
{
    public int Codigo { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public Tipo Tipo { get; set; }

    public override string ToString()
    {
        return string.Format("{0}: {1}, {2} anos, {3}", Codigo, Nome, Idade, Tipo);
    }
}