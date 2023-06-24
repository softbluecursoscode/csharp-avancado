using Softblue.Mvvm;

namespace CadCliente.Model
{
    // Representa um cliente
    class Cliente : Bindable
    {
        private int? id;
        public int? Id
        {
            get { return id; }
            set { SetValue(ref id, value); }
        }

        private string nome;
        public string Nome
        {
            get { return nome; }
            set
            {
                SetValue(ref nome, value);

                if (string.IsNullOrEmpty(nome))
                {
                    AddError("O nome não pode ser vazio");
                }
                else
                {
                    RemoveErrors();
                }
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { SetValue(ref email, value); }
        }

        private string telefone;
        public string Telefone
        {
            get { return telefone; }
            set { SetValue(ref telefone, value); }
        }

        private Endereco endereco;
        public Endereco Endereco
        {
            get
            {
                if (endereco == null)
                {
                    endereco = new Endereco();
                }
                return endereco;
            }
            set { SetValue(ref endereco, value); }
        }

    }
}
