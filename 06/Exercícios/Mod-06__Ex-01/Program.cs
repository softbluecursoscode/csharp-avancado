using System;
using System.Linq;
using System.Collections.Generic;

namespace Softblue
{
    class Program
    {
        static void Main()
        {
            List<ItemMercado> itens = LerItens();

            // Itens de higiene em ordem decrescente de preço.
            var q1 = from i in itens
                     where i.Tipo == Tipo.Higiene
                     orderby i.Preco descending
                     select i;

            // Itens com preço maior ou igual a R$ 3,00 ordenados por preço de forma crescente.
            var q2 = from i in itens
                     where i.Preco >= 3
                     orderby i.Preco
                     select i;

            // Itens do tipo Comida ou Bebida em ordem alfabética.
            var q3 = from i in itens
                     where i.Tipo == Tipo.Comida || i.Tipo == Tipo.Bebida
                     orderby i.Nome
                     select i;

            // Tipos e quantidade de itens por tipo.
            var q4 = from i in itens
                     group i by i.Tipo into g
                     select new { Tipo = g.Key, Qtde = g.Count() };

            // Tipos e valor mínimo, valor máximo e valor médio por tipo.
            var q5 = from i in itens
                     group i by i.Tipo into g
                     select new { Tipo = g.Key, Min = g.Min(i => i.Preco), Max = g.Max(i => i.Preco), Media = g.Average(i => i.Preco) };
        }
        
        // Cria a lista com os dados para filtragem.
        static List<ItemMercado> LerItens()
        {
            List<ItemMercado> itens = new List<ItemMercado>();

            itens.Add(new ItemMercado() { Nome = "Arroz", Tipo = Tipo.Comida, Preco = 3.9 });
            itens.Add(new ItemMercado() { Nome = "Azeite", Tipo = Tipo.Comida, Preco = 2.5 });
            itens.Add(new ItemMercado() { Nome = "Macarrão", Tipo = Tipo.Comida, Preco = 3.9 });
            itens.Add(new ItemMercado() { Nome = "Cerveja", Tipo = Tipo.Bebida, Preco = 22.9 });
            itens.Add(new ItemMercado() { Nome = "Refrigerante", Tipo = Tipo.Bebida, Preco = 5.5 });
            itens.Add(new ItemMercado() { Nome = "Shampoo", Tipo = Tipo.Higiene, Preco = 7 });
            itens.Add(new ItemMercado() { Nome = "Sabonete", Tipo = Tipo.Higiene, Preco = 2.4 });
            itens.Add(new ItemMercado() { Nome = "Cotonete", Tipo = Tipo.Higiene, Preco = 5.7 });
            itens.Add(new ItemMercado() { Nome = "Sabão em pó", Tipo = Tipo.Limpeza, Preco = 8.2 });
            itens.Add(new ItemMercado() { Nome = "Detergente", Tipo = Tipo.Limpeza, Preco = 2.6 });
            itens.Add(new ItemMercado() { Nome = "Amaciante", Tipo = Tipo.Limpeza, Preco = 6.4 });

            return itens;
        }
    }

    // Tipos de itens.
    enum Tipo
    {
        Comida,
        Bebida,
        Limpeza,
        Higiene
    }

    // Item para comprar no supermercado.
    class ItemMercado
    {
        public string Nome { get; set; }
        public Tipo Tipo { get; set; }
        public double Preco { get; set; }

        public override string ToString()
        {
            return string.Format("{0,-15}{1,-10}{2:C}", Nome, Tipo, Preco);
        }
    }
}
