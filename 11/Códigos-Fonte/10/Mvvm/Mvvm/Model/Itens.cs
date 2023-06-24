using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvvm.Model
{
    class Itens
    {
        List<string> nomes = new List<string>();

        public void Adicionar(string nome)
        {
            nomes.Add(nome);
            ShowDebug();
        }

        public void Remover(int pos)
        {
            nomes.RemoveAt(pos);
            ShowDebug();
        }

        void ShowDebug()
        {
            StringBuilder s = new StringBuilder("Itens: ");
            nomes.ForEach(n => s.Append(n).Append(" "));
            Debug.WriteLine(s.ToString());
        }
    }
}
