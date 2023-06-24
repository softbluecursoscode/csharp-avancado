using Mvvm.Model;
using Softblue.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mvvm.ApplicationServices;

namespace Mvvm.ViewModel
{
    class ItensViewModel : Bindable
    {
        Itens itens;

        private string nomeItem;
        public string NomeItem
        {
            get { return nomeItem; }
            set
            {
                SetValue(ref nomeItem, value);

                if (string.IsNullOrWhiteSpace(nomeItem))
                {
                    AddError("Item em branco");
                }
                else
                {
                    RemoveErrors();
                }
            }
        }

        private ObservableCollection<string> lista;
        public ObservableCollection<string> Lista
        {
            get { return lista; }
            set { SetValue(ref lista, value); }
        }

        private int indice;
        public int Indice
        {
            get { return indice; ; }
            set
            {
                SetValue(ref indice, value);
                OnPropertyChanged("TextoIndice");
                ExcluirCommand.CanExecute = indice >= 0;
            }
        }

        public string TextoIndice
        {
            get
            {
                if (Indice < 0)
                {
                    return "Nenhum item selecionado";
                }
                else
                {
                    return "Índice selecionado: " + Indice;
                }
            }
        }

        public Command AdicionarCommand { get; set; }
        public Command ExcluirCommand { get; set; }

        public IWindowServices WindowServices { get; set; }

        public ItensViewModel()
        {
            AdicionarCommand = new Command(Adicionar, false);
            ExcluirCommand = new Command(Excluir);

            itens = new Itens();
            Lista = new ObservableCollection<string>();
            Indice = -1; 

            ErrorsChanged += (s, a) => AdicionarCommand.CanExecute = !HasErrors;
        }

        void Adicionar()
        {
            itens.Adicionar(NomeItem);
            Lista.Add(NomeItem);
            NomeItem = "";
            Indice = -1;
            WindowServices.SetFocus();
        }

        void Excluir()
        {
            itens.Remover(Indice);
            Lista.RemoveAt(Indice);
            Indice = -1;
        }
    }
}
