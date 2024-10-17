using Microsoft.Data.Sqlite;
using SistemaCadastroLivrosPOO.Entities;
using System;
using System.Collections.Generic;

namespace SistemaCadastroLivrosPOO.Services
{
    public class GerenciadorDeBanco<T>
    {
        private readonly string _connectionString;

        public GerenciadorDeBanco(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Método para adicionar um item ao banco de dados
        public void Adicionar(T item)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var query = GerarQueryDeInsercao(item);
                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para obter todos os itens de uma tabela
        public List<T> ObterTodos()
        {
            var itens = new List<T>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var query = GerarQueryDeSelecao();
                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            itens.Add(MapearReaderParaObjeto(reader));
                        }
                    }
                }
            }

            return itens;
        }

        // Método para atualizar um item no banco de dados
        public void Atualizar(T item)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var query = GerarQueryDeAtualizacao(item);
                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para remover um item do banco de dados
        public void Remover(T item)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var query = GerarQueryDeRemocao(item);
                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // Gerar query de inserção dependendo do tipo de T
        private string GerarQueryDeInsercao(T item)
        {
            if (item is Livro livro)
            {
                return $"INSERT INTO Livros (Titulo, Autor, ISBN, Disponivel) VALUES ('{livro.Titulo}', '{livro.Autor}', '{livro.ISBN}', {(livro.Disponivel ? 1 : 0)});";
            }
            else if (item is Cliente cliente)
            {
                return $"INSERT INTO Clientes (Nome, CPF, Endereco) VALUES ('{cliente.Nome}', '{cliente.CPF}', '{cliente.Endereco}');";
            }
            else if (item is Funcionario funcionario)
            {
                return $"INSERT INTO Funcionarios (Nome, CPF, Cargo) VALUES ('{funcionario.Nome}', '{funcionario.CPF}', '{funcionario.Cargo}');";
            }
            else if (item is Emprestimo emprestimo)
            {
                return $"INSERT INTO Emprestimos (LivroId, ClienteId, DataEmprestimo, DataDevolucao) VALUES ({emprestimo.Livro.Id}, {emprestimo.Cliente.Id}, '{emprestimo.DataEmprestimo:yyyy-MM-dd HH:mm:ss}', '{emprestimo.DataDevolucao:yyyy-MM-dd HH:mm:ss}');";
            }

            throw new NotImplementedException("Tipo não suportado para inserção.");
        }

        // Gerar query de seleção dependendo do tipo de T
        private string GerarQueryDeSelecao()
        {
            if (typeof(T) == typeof(Livro))
            {
                return "SELECT * FROM Livros;";
            }
            else if (typeof(T) == typeof(Cliente))
            {
                return "SELECT * FROM Clientes;";
            }
            else if (typeof(T) == typeof(Funcionario))
            {
                return "SELECT * FROM Funcionarios;";
            }
            else if (typeof(T) == typeof(Emprestimo))
            {
                return "SELECT * FROM Emprestimos;";
            }

            throw new NotImplementedException("Tipo não suportado para seleção.");
        }

        // Gerar query de atualização dependendo do tipo de T
        private string GerarQueryDeAtualizacao(T item)
        {
            if (item is Livro livro)
            {
                return $"UPDATE Livros SET Titulo = '{livro.Titulo}', Autor = '{livro.Autor}', ISBN = '{livro.ISBN}', Disponivel = {(livro.Disponivel ? 1 : 0)} WHERE ISBN = '{livro.ISBN}';";
            }
            else if (item is Cliente cliente)
            {
                return $"UPDATE Clientes SET Nome = '{cliente.Nome}', CPF = '{cliente.CPF}', Endereco = '{cliente.Endereco}' WHERE CPF = '{cliente.CPF}';";
            }
            else if (item is Funcionario funcionario)
            {
                return $"UPDATE Funcionarios SET Nome = '{funcionario.Nome}', CPF = '{funcionario.CPF}', Cargo = '{funcionario.Cargo}' WHERE CPF = '{funcionario.CPF}';";
            }
            else if (item is Emprestimo emprestimo)
            {
                return $"UPDATE Emprestimos SET DataDevolucao = '{emprestimo.DataDevolucao:yyyy-MM-dd HH:mm:ss}' WHERE Id = {emprestimo.Id};";
            }

            throw new NotImplementedException("Tipo não suportado para atualização.");
        }

        // Gerar query de remoção dependendo do tipo de T
        private string GerarQueryDeRemocao(T item)
        {
            if (item is Livro livro)
            {
                return $"DELETE FROM Livros WHERE ISBN = '{livro.ISBN}';";
            }
            else if (item is Cliente cliente)
            {
                return $"DELETE FROM Clientes WHERE CPF = '{cliente.CPF}';";
            }
            else if (item is Funcionario funcionario)
            {
                return $"DELETE FROM Funcionarios WHERE CPF = '{funcionario.CPF}';";
            }
            else if (item is Emprestimo emprestimo)
            {
                return $"DELETE FROM Emprestimos WHERE Id = {emprestimo.Id};";
            }

            throw new NotImplementedException("Tipo não suportado para remoção.");
        }

        // Mapear os dados do reader para o objeto correspondente
        private T MapearReaderParaObjeto(SqliteDataReader reader)
        {
            if (typeof(T) == typeof(Livro))
            {
                return (T)(object)new Livro(
                    reader["Titulo"].ToString(),
                    reader["Autor"].ToString(),
                    reader["ISBN"].ToString())
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Disponivel = Convert.ToBoolean(reader["Disponivel"])
                };
            }
            else if (typeof(T) == typeof(Cliente))
            {
                return (T)(object)new Cliente(
                    reader["Nome"].ToString(),
                    reader["CPF"].ToString(),
                    reader["Endereco"].ToString())
                {
                    Id = Convert.ToInt32(reader["Id"])
                };
            }
            else if (typeof(T) == typeof(Funcionario))
            {
                return (T)(object)new Funcionario(
                    reader["Nome"].ToString(),
                    reader["CPF"].ToString(),
                    reader["Cargo"].ToString())
                {
                    Id = Convert.ToInt32(reader["Id"])
                };
            }
            else if (typeof(T) == typeof(Emprestimo))
            {
                var livro = new Livro("", "", "")
                {
                    Id = Convert.ToInt32(reader["LivroId"])
                };

                var cliente = new Cliente("", "", "")
                {
                    Id = Convert.ToInt32(reader["ClienteId"])
                };

                return (T)(object)new Emprestimo(livro, cliente)
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    DataEmprestimo = Convert.ToDateTime(reader["DataEmprestimo"]),
                    DataDevolucao = Convert.ToDateTime(reader["DataDevolucao"])
                };
            }

            throw new NotImplementedException("Tipo não suportado para mapeamento.");
        }
    }
}
