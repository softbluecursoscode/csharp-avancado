using Softblue.Mvvm;


namespace CadCliente.Model
{
    // Representa um endereço
    class Endereco : Bindable
    {
        private Cliente cliente;
        public Cliente Cliente
        {
            get { return cliente; }
            set { SetValue(ref cliente, value); }
        }

        private string logradouro;
        public string Logradouro
        {
            get { return logradouro; }
            set
            {
                SetValue(ref logradouro, value);
                if (string.IsNullOrEmpty(logradouro))
                {
                    AddError("O logradouro não pode ser vazio");
                }
                else
                {
                    RemoveErrors();
                }
            }
        }

        private int? numero;
        public int? Numero
        {
            get { return numero; }
            set
            {
                SetValue(ref numero, value);
                if (numero == null)
                {
                    AddError("O número não pode ser vazio");
                }
                else
                {
                    RemoveErrors();
                }
            }
        }

        private string complemento;
        public string Complemento
        {
            get { return complemento; }
            set { SetValue(ref complemento, value); }
        }

        private string bairro;
        public string Bairro
        {
            get { return bairro; }
            set { SetValue(ref bairro, value); }
        }

        private string cep;
        public string Cep
        {
            get { return cep; }
            set
            {
                SetValue(ref cep, value);

                if (string.IsNullOrEmpty(cep))
                {
                    AddError("O CEP não pode ser vazio");
                }
                else
                {
                    RemoveErrors();
                }
            }
        }
    }
}
