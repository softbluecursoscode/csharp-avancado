using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Softblue
{
    class MusicaDao
    {
        DbProviderFactory factory;

        // O construtor recebe o DbProviderFacory como parâmetro.
        public MusicaDao(DbProviderFactory factory)
        {
            this.factory = factory;
        }

        // Lista as músicas cadastradas.
        public List<Musica> Listar(DbConnection conn)
        {
            List<Musica> musicas = new List<Musica>();
            
            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT Id, Titulo, Cantor, Album, Ano, Genero FROM Musica ORDER BY Titulo";

                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Musica m = new Musica();
                        m.Id = (int)dr["Id"];
                        m.Titulo = (string)dr["Titulo"];
                        m.Cantor = (string)dr["Cantor"];
                        m.Album = (string)dr["Album"];
                        m.Genero = (Genero)Enum.Parse(typeof(Genero), (string)dr["Genero"]);
                        musicas.Add(m);
                    }
                }
            }

            return musicas;
        }

        // Insere uma nova música.
        public void Inserir(DbConnection conn, Musica m)
        {
            DbCommand cmd = factory.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Musica (Titulo, Cantor, Album, Ano, Genero) VALUES (@Titulo, @Cantor, @Album, @Ano, @Genero)";

            DbParameter param = factory.CreateParameter();
            param.ParameterName = "@Titulo";
            param.Value = m.Titulo;
            cmd.Parameters.Add(param);

            param = factory.CreateParameter();
            param.ParameterName = "@Cantor";
            param.Value = m.Cantor;
            cmd.Parameters.Add(param);

            param = factory.CreateParameter();
            param.ParameterName = "@Album";
            param.Value = m.Album;
            cmd.Parameters.Add(param);

            param = factory.CreateParameter();
            param.ParameterName = "@Ano";
            param.Value = m.Ano;
            cmd.Parameters.Add(param);

            param = factory.CreateParameter();
            param.ParameterName = "@Genero";
            param.Value = m.Genero.ToString();
            cmd.Parameters.Add(param);

            cmd.ExecuteNonQuery();
        }

        // Atualiza uma música existente com base no Id.
        public void Atualizar(DbConnection conn, Musica m)
        {
            DbCommand cmd = factory.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE Musica SET Titulo = @Titulo, Cantor = @Cantor, Album = @Album, Ano = @Ano, Genero = @Genero WHERE Id = @Id";

            DbParameter param = factory.CreateParameter();
            param.ParameterName = "@Titulo";
            param.Value = m.Titulo;
            cmd.Parameters.Add(param);

            param = factory.CreateParameter();
            param.ParameterName = "@Cantor";
            param.Value = m.Cantor;
            cmd.Parameters.Add(param);

            param = factory.CreateParameter();
            param.ParameterName = "@Album";
            param.Value = m.Album;
            cmd.Parameters.Add(param);

            param = factory.CreateParameter();
            param.ParameterName = "@Ano";
            param.Value = m.Ano;
            cmd.Parameters.Add(param);

            param = factory.CreateParameter();
            param.ParameterName = "@Genero";
            param.Value = m.Genero;
            cmd.Parameters.Add(param);

            param = factory.CreateParameter();
            param.ParameterName = "@Id";
            param.Value = m.Id;
            cmd.Parameters.Add(param);

            cmd.ExecuteNonQuery();
        }

        // Exclui uma música por Id.
        public void Excluir(DbConnection conn, int id)
        {
            DbCommand cmd = factory.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM Musica WHERE Id = @Id";

            DbParameter param = factory.CreateParameter();
            param.ParameterName = "@Id";
            param.Value = id;
            cmd.Parameters.Add(param);

            cmd.ExecuteNonQuery();
        }

        // Carrega uma música pelo Id.
        public Musica Carregar(DbConnection conn, int id)
        {
            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT Id, Titulo, Cantor, Album, Ano, Genero FROM Musica WHERE Id = @Id";

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

                    Musica m = new Musica();
                    m.Id = (int)dr["Id"];
                    m.Titulo = (string)dr["Titulo"];
                    m.Cantor = (string)dr["Cantor"];
                    m.Album = (string)dr["Album"];
                    m.Ano = (int)dr["Ano"];
                    m.Genero = (Genero)Enum.Parse(typeof(Genero), (string)dr["Genero"]);

                    return m;
                }
            }
        }

        // Retorna a quantidade de músicas cadastradas.
        public int Contar(DbConnection conn)
        {
            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT COUNT(*) FROM Musica";

                int count = (int)cmd.ExecuteScalar();
                return count;
            }
        }
    }
}
