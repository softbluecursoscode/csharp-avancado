using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;

namespace Softblue
{
    class Program
    {
        static void Main()
        {
            // Lê os dados para conexão de App.config.
            DbProviderFactory factory = DbProviderFactories.GetFactory(ConfigurationManager.AppSettings["dbProvider"]);
            string connString = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;

            // Cria o Dao, que vai interagir com o banco de dados.
            MusicaDao dao = new MusicaDao(factory);

            // Abre a conexão e executa algumas operações.
            using (DbConnection conn = factory.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();

                ExcluirMusicas(conn, dao);
                CadastrarMusicas(conn, dao);
                Atualizar(conn, dao);
                ContarMusicas(conn, dao);
            }
        }

        // Exclui todas as músicas cadastradas.
        static void ExcluirMusicas(DbConnection conn, MusicaDao dao)
        {
            // Lista as músicas.
            List<Musica> musicas = dao.Listar(conn);

            // Exclui cada uma delas.
            foreach(Musica musica in musicas)
            {
                dao.Excluir(conn, musica.Id);
            }
        }

        // Cadastra várias músicas.
        static void CadastrarMusicas(DbConnection conn, MusicaDao dao)
        {
            // Cria a lista de músicas.
            List<Musica> musicas = new List<Musica>()
            {
                new Musica() { Titulo = "Bohemian Rhapsody", Cantor = "Queen", Genero = Genero.Rock, Ano = 1975, Album = "A Night at the Opera" },
                new Musica() { Titulo = "Don't Speak", Cantor = "No Doubt", Genero = Genero.Pop, Ano = 1995, Album = "Tragic Kingdom" },
                new Musica() { Titulo = "Is This Love?", Cantor = "Bob Marley", Genero = Genero.Reggae, Ano = 1978, Album = "Kaya"}
            };

            // Itera sobre a lista e cadastra.
            foreach(Musica musica in musicas)
            {
                dao.Inserir(conn, musica);
            }
        }

        // Atualiza o álbum da música com id 20, se ela existir.
        static void Atualizar(DbConnection conn, MusicaDao dao)
        {
            Musica musica = dao.Carregar(conn, 20);

            if (musica != null)
            {
                musica.Album = "<Desconhecido>";
                dao.Atualizar(conn, musica);
            }
        }

        // Exibe o número de músicas cadastradas.
        static void ContarMusicas(DbConnection conn, MusicaDao dao)
        {
            int c = dao.Contar(conn);
            Console.WriteLine(c);
        }
    }
}
