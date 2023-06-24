using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;

class Program
{
    static string connString;
    static DbProviderFactory factory;

    static void Main()
    {
        factory = DbProviderFactories.GetFactory(ConfigurationManager.AppSettings["dbProvider"]);
        connString = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;

        using (DbConnection conn = factory.CreateConnection())
        {
            conn.ConnectionString = connString;
            conn.Open();

            Produto p = new Produto()
            {
                Nome = "Cadeira",
                Descricao = "Cadeira de escritório",
                Valor = 1200
            };

            Insert(conn, p);
            Update(conn, p);
            p = Find(conn, 11);
            Delete(conn, 11);

            List<Produto> produtos = List(conn);
            foreach (Produto prod in produtos)
            {
                Console.WriteLine(prod);
            }

        }
    }

    static List<Produto> List(DbConnection conn)
    {
        List<Produto> produtos = new List<Produto>();

        using (DbCommand cmd = factory.CreateCommand())
        {
            cmd.Connection = conn;
            cmd.CommandText = "SELECT id, nome, valor, descricao FROM produto ORDER BY id";

            using (DbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Produto p = new Produto();
                    p.Id = (int)dr["id"];
                    p.Nome = (string)dr["nome"];
                    p.Valor = Convert.ToDouble((decimal)dr["valor"]);
                    p.Descricao = (string)dr["descricao"];
                    produtos.Add(p);
                }
            }
        }

        return produtos;
    }

    static void Insert(DbConnection conn, Produto p)
    {
        DbCommand cmd = factory.CreateCommand();
        cmd.Connection = conn;
        cmd.CommandText = "INSERT INTO produto (nome, valor, descricao) VALUES (@Nome, @Valor, @Descricao)";

        DbParameter param = factory.CreateParameter();
        param.ParameterName = "@Nome";
        param.Value = p.Nome;
        cmd.Parameters.Add(param);

        param = factory.CreateParameter();
        param.ParameterName = "@Valor";
        param.Value = p.Valor;
        cmd.Parameters.Add(param);

        param = factory.CreateParameter();
        param.ParameterName = "@Descricao";
        param.Value = p.Descricao;
        cmd.Parameters.Add(param);

        cmd.ExecuteNonQuery();
    }

    static void Update(DbConnection conn, Produto p)
    {
        DbCommand cmd = factory.CreateCommand();
        cmd.Connection = conn;
        cmd.CommandText = "UPDATE produto SET nome = @Nome, valor = @Valor, descricao = @Descricao WHERE id = @Id";

        DbParameter param = factory.CreateParameter();
        param.ParameterName = "@Nome";
        param.Value = p.Nome;
        cmd.Parameters.Add(param);

        param = factory.CreateParameter();
        param.ParameterName = "@Valor";
        param.Value = p.Valor;
        cmd.Parameters.Add(param);

        param = factory.CreateParameter();
        param.ParameterName = "@Descricao";
        param.Value = p.Descricao;
        cmd.Parameters.Add(param);

        param = factory.CreateParameter();
        param.ParameterName = "@Id";
        param.Value = p.Id;
        cmd.Parameters.Add(param);

        cmd.ExecuteNonQuery();
    }

    static void Delete(DbConnection conn, int id)
    {
        DbCommand cmd = factory.CreateCommand();
        cmd.Connection = conn;
        cmd.CommandText = "DELETE FROM produto WHERE id = @Id";

        DbParameter param = factory.CreateParameter();
        param.ParameterName = "@Id";
        param.Value = id;
        cmd.Parameters.Add(param);

        cmd.ExecuteNonQuery();
    }

    static Produto Find(DbConnection conn, int id)
    {
        using (DbCommand cmd = factory.CreateCommand())
        {
            cmd.Connection = conn;
            cmd.CommandText = "SELECT id, nome, valor, descricao FROM produto WHERE id = @Id";

            DbParameter param = factory.CreateParameter();
            param.ParameterName = "@Id";
            param.Value = id;
            cmd.Parameters.Add(param);

            using (DbDataReader dr = cmd.ExecuteReader())
            {
                if (!dr.Read())
                {
                    return null;
                }

                Produto p = new Produto();
                p.Id = (int)dr["id"];
                p.Nome = (string)dr["nome"];
                p.Valor = Convert.ToDouble((decimal)dr["valor"]);
                p.Descricao = (string)dr["descricao"];

                return p;
            }
        }
    }

    static int Count(DbConnection conn)
    {
        using (DbCommand cmd = factory.CreateCommand())
        {
            cmd.Connection = conn;
            cmd.CommandText = "SELECT COUNT(*) FROM produto";

            int count = (int)cmd.ExecuteScalar();
            return count;
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
        return string.Format("{0,-3}{1,-15}{2,-15:C}{3}", Id, Nome, Valor, Descricao);
    }
}
