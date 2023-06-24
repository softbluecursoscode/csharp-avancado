using System;

namespace Softblue
{
    // Gêneros musicais.
    enum Genero
    {
        Rock, Pop, Jazz, Reggae, Blues
    }

    // Classe que representa uma música.
    class Musica
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Cantor { get; set; }
        public string Album { get; set; }
        public int? Ano { get; set; }
        public Genero Genero { get; set; }
    }
}
