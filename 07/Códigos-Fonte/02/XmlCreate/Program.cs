using System;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        Funcionario f = CriarFuncionario();

        XElement xml = new XElement("funcionario",
            new XElement("id", f.Id),
            new XElement("nome", f.Nome),
            new XElement("telefone", new XAttribute("tipo", "residencial"), f.TelefoneResidencial),
            new XElement("telefone", new XAttribute("tipo", "celular"), f.TelefoneCelular),
            new XElement("endereco",
                new XElement("rua", f.Endereco.Rua),
                new XElement("numero", f.Endereco.Numero),
                new XElement("cidade", f.Endereco.Cidade),
                new XElement("estado", f.Endereco.Estado)));

        Console.WriteLine(xml);
    }

    private static Funcionario CriarFuncionario()
    {
        Funcionario f = new Funcionario();
        f.Id = 1;
        f.Nome = "José da Silva";
        f.TelefoneCelular = "3333-5555";
        f.TelefoneResidencial = "9999-5555";

        Endereco e = new Endereco();
        e.Rua = "R. dos Limões";
        e.Numero = 100;
        e.Cidade = "São Paulo";
        e.Estado = "SP";
        f.Endereco = e;

        return f;
    }
}

class Funcionario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string TelefoneResidencial { get; set; }
    public string TelefoneCelular { get; set; }
    public Endereco Endereco { get; set; }
}

class Endereco
{
    public string Rua { get; set; }
    public int Numero { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
}