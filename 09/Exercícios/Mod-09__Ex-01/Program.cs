using System;
using System.Collections.Generic;
using System.Linq;

namespace Softblue
{
    class Program
    {
        static void Main()
        {
            // Cria o context do Entity Framework.
            using (Entities context = new Entities())
            {
                ExcluirMusicas(context);
                CadastrarMusicas(context);
                Atualizar(context);
                ContarMusicas(context);
            }
        }

        // Exclui todas as músicas cadastradas.
        static void ExcluirMusicas(Entities context)
        {
            // Lista as músicas.
            var q = from m in context.Musica
                    select m;

            // Remove cada uma delas.
            foreach (var musica in q)
            {
                context.Musica.Remove(musica);
            }

            context.SaveChanges();
        }

        // Cadastra várias músicas.
        static void CadastrarMusicas(Entities context)
        {
            // Cria a lista de músicas.
            List<Musica> musicas = new List<Musica>()
            {
                new Musica() { Titulo = "Bohemian Rhapsody", Cantor = "Queen", Genero = Genero.Rock.ToString(), Ano = 1975, Album = "A Night at the Opera" },
                new Musica() { Titulo = "Don't Speak", Cantor = "No Doubt", Genero = Genero.Pop.ToString(), Ano = 1995, Album = "Tragic Kingdom" },
                new Musica() { Titulo = "Is This Love?", Cantor = "Bob Marley", Genero = Genero.Reggae.ToString(), Ano = 1978, Album = "Kaya"}
            };

            // Itera sobre a lista e cadastra.
            foreach (Musica musica in musicas)
            {
                context.Musica.Add(musica);
            }

            context.SaveChanges();
        }

        // Atualiza o álbum da música com id 20, se ela existir.
        static void Atualizar(Entities context)
        {
            Musica musica = context.Musica.Find(20);

            if (musica != null)
            {
                musica.Album = "<Desconhecido>";
                context.SaveChanges();
            }
        }

        // Exibe o número de músicas cadastradas.
        static void ContarMusicas(Entities context)
        {
            int c = (from m in context.Musica
                     select m).Count();

            Console.WriteLine(c);
        }
    }
}
