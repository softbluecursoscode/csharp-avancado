using System;
using System.Configuration;
using System.Data.Common;
using System.Collections.Generic;
using Db;

class Program
{
    static void Main()
    {
        ProdutoDAO dao = DaoFactory.CreateDao<ProdutoDAO>();

        Produto p = new Produto();
        p.Nome = "Televisão";
        p.Valor = 1600;
        p.Descricao = "Televisão de LED";

        dao.Inserir(p);

        p = dao.Carregar(12);
        Console.WriteLine(p);
    }
}

class ProdutoDAO : Dao
{
    public void Inserir(Produto p)
    {
        using (DbConnection conn = CreateConnection())
        {
            using (DbCommand cmd = CreateCommand(conn, "INSERT INTO produto (nome, descricao, valor) VALUES (@Nome, @Descricao, @Valor)"))
            {
                CreateParameter(cmd, "@Nome", p.Nome, null);
                CreateParameter(cmd, "@Descricao", p.Descricao, null);
                CreateParameter(cmd, "@Valor", p.Valor, null);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public Produto Carregar(int id)
    {
        using (DbConnection conn = CreateConnection())
        {
            using (DbCommand cmd = CreateCommand(conn, "SELECT id, nome, descricao, valor FROM produto WHERE id = @Id"))
            {
                CreateParameter(cmd, "@Id", id, null);
                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    if (!dr.Read())
                    {
                        return null;
                    }

                    return new Produto()
                    {
                        Id = (int)dr["id"],
                        Nome = (string)dr["nome"],
                        Descricao = (string)dr["descricao"],
                        Valor = Convert.ToDouble((decimal)dr["valor"])
                    };
                }
            }
        }
    }

}

class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public double Valor { get; set; }
    public string Descricao { get; set; }

    public override string ToString()
    {
        return string.Format("{0}: {1}, {2:C}, {3}", Id, Nome, Valor, Descricao);
    }
}