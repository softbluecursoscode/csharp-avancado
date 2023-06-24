using Db;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System;

namespace CadCliente.Model.Db
{
    // Métodos de acesso a daodos para cliente/endereço.
    class ClienteDao : Dao
    {
        // Obtém a lista de clientes/endereços com base em um padrão de nome.
        public List<Cliente> ListarClientes(string padrao)
        {
            using (DbConnection conn = CreateConnection())
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT Id, Nome, Email, Telefone, Logradouro, Numero, Complemento, Bairro, Cep FROM Cliente AS c INNER JOIN Endereco AS e ON Id = Cliente_Id ");

                if (!string.IsNullOrWhiteSpace(padrao))
                {
                    sql.Append("WHERE Nome LIKE @Padrao ");
                }

                sql.Append("ORDER BY Nome");

                using (DbCommand cmd = CreateCommand(conn, sql.ToString()))
                {
                    if (!string.IsNullOrWhiteSpace(padrao))
                    {
                        AddParameter(cmd, "@Padrao", "%" + padrao + "%");
                    }

                    List<Cliente> clientes = new List<Cliente>();

                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Cliente c = new Cliente();
                            ExtrairDados(dr, c);
                            clientes.Add(c);
                        }

                        return clientes;
                    }
                }
            }
        }

        // Obtém um cliente/endereço com base no seu ID. Os dados do cliente são adicionados no objeto passado como parâmetro.
        public void LerCliente(int id, Cliente cliente)
        {
            using (DbConnection conn = CreateConnection())
            {
                string sql = "SELECT Id, Nome, Email, Telefone, Logradouro, Numero, Complemento, Bairro, Cep FROM Cliente AS c INNER JOIN Endereco AS e ON Id = Cliente_Id WHERE Id = @Id";
                using (DbCommand cmd = CreateCommand(conn, sql))
                {
                    AddParameter(cmd, "@Id", id);

                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        if (!dr.Read())
                        {
                            return;
                        }

                        ExtrairDados(dr, cliente);
                    }
                }
            }
        }

        // Insere um novo cliente/endereço no banco de dados.
        public void Inserir(Cliente cliente)
        {
            using (DbConnection conn = CreateConnection())
            {
                // Usa uma transação para fazer as duas inserções de forma atômica.
                DbTransaction transaction = conn.BeginTransaction();

                string sql = "INSERT INTO Cliente (Id, Nome, Email, Telefone) VALUES (@Id, @Nome, @Email, @Telefone)";
                using (DbCommand cmd = CreateCommand(conn, sql))
                {
                    cmd.Transaction = transaction;
                    
                    cliente.Id = LerMaiorId() + 1;

                    AddParameter(cmd, "@Id", cliente.Id);
                    AddParameter(cmd, "@Nome", cliente.Nome);
                    AddParameter(cmd, "@Email", cliente.Email);
                    AddParameter(cmd, "@Telefone", cliente.Telefone);

                    cmd.ExecuteNonQuery();
                }

                sql = "INSERT INTO Endereco (Cliente_Id, Logradouro, Numero, Complemento, Bairro, Cep) VALUES (@Id, @Logradouro, @Numero, @Complemento, @Bairro, @Cep)";
                using (DbCommand cmd = CreateCommand(conn, sql))
                {
                    cmd.Transaction = transaction;

                    cliente.Endereco.Cliente = cliente;

                    AddParameter(cmd, "@Id", cliente.Id);
                    AddParameter(cmd, "@Logradouro", cliente.Endereco.Logradouro);
                    AddParameter(cmd, "@Numero", cliente.Endereco.Numero);
                    AddParameter(cmd, "@Complemento", cliente.Endereco.Complemento);
                    AddParameter(cmd, "@Bairro", cliente.Endereco.Bairro);
                    AddParameter(cmd, "@Cep", cliente.Endereco.Cep);

                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
            }
        }

        // Altera os dados de um cliente/endereço.
        public void Alterar(Cliente cliente)
        {
            using (DbConnection conn = CreateConnection())
            {
                // Usa uma transação para fazer as duas alterações de forma atômica.
                DbTransaction transaction = conn.BeginTransaction();

                string sql = "UPDATE Cliente SET Nome = @Nome, Email = @Email, Telefone = @Telefone WHERE Id = @Id";
                using (DbCommand cmd = CreateCommand(conn, sql))
                {
                    cmd.Transaction = transaction;

                    AddParameter(cmd, "@Nome", cliente.Nome);
                    AddParameter(cmd, "@Email", cliente.Email);
                    AddParameter(cmd, "@Telefone", cliente.Telefone);
                    AddParameter(cmd, "@Id", cliente.Id);

                    cmd.ExecuteNonQuery();
                }

                sql = "UPDATE Endereco SET Logradouro = @Logradouro, Numero = @Numero, Complemento = @Complemento, Bairro = @Bairro, Cep = @Cep WHERE Cliente_Id = @Id";
                using (DbCommand cmd = CreateCommand(conn, sql))
                {
                    cmd.Transaction = transaction;

                    AddParameter(cmd, "@Logradouro", cliente.Endereco.Logradouro);
                    AddParameter(cmd, "@Numero", cliente.Endereco.Numero);
                    AddParameter(cmd, "@Complemento", cliente.Endereco.Complemento);
                    AddParameter(cmd, "@Bairro", cliente.Endereco.Bairro);
                    AddParameter(cmd, "@Cep", cliente.Endereco.Cep);
                    AddParameter(cmd, "@Id", cliente.Id);

                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
            }
        }

        // Adiciona um parâmetro À query.
        void AddParameter(DbCommand cmd, string name, object value)
        {
            if (value == null)
            {
                // Se o valor for nulo, atribui DbNull.
                value = DBNull.Value;
            }

            CreateParameter(cmd, name, value, null);
        }

        // Exclui um cliente/endereço com base no ID.
        public void Excluir(int id)
        {
            using (DbConnection conn = CreateConnection())
            {
                // Usa uma transação para fazer as duas exclusões de forma atômica.
                DbTransaction transaction = conn.BeginTransaction();

                // Primeiro exclui o endereço devido a restrições de chave estrangeira na tabela.
                string sql = "DELETE FROM Endereco WHERE Cliente_Id = @Id";
                using (DbCommand cmd = CreateCommand(conn, sql))
                {
                    cmd.Transaction = transaction;
                    AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }

                sql = "DELETE FROM Cliente WHERE Id = @Id";
                using (DbCommand cmd = CreateCommand(conn, sql))
                {
                    cmd.Transaction = transaction;
                    AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
            }
        }

        // Obtém o maior ID cadastrado para um cliente.
        public int LerMaiorId()
        {
            using (DbConnection conn = CreateConnection())
            {
                string sql = "SELECT MAX(Id) FROM Cliente";
                using (DbCommand cmd = CreateCommand(conn, sql))
                {
                    object obj = cmd.ExecuteScalar();

                    if (obj == System.DBNull.Value)
                    {
                        return 0;
                    }

                    return (int)obj;
                }
            }
        }

        // Extrai dados de um DbDataReader e os coloca no objeto Cliente/Endereco fornecido.
        private void ExtrairDados(DbDataReader dr, Cliente cliente)
        {
            cliente.Id = (int)dr["Id"];
            cliente.Nome = (string)dr["Nome"];
            cliente.Email = dr["Email"] == DBNull.Value ? null : (string)dr["Email"];
            cliente.Telefone = dr["Telefone"] == DBNull.Value ? null : (string)dr["Telefone"];
            cliente.Endereco = new Endereco()
            {
                Logradouro = (string)dr["Logradouro"],
                Numero = (int)dr["Numero"],
                Complemento = dr["Complemento"] == DBNull.Value ? null : (string)dr["Complemento"],
                Bairro = dr["Bairro"] == DBNull.Value ? null : (string)dr["Bairro"],
                Cep = (string)dr["Cep"]
            };
        }
    }
}
