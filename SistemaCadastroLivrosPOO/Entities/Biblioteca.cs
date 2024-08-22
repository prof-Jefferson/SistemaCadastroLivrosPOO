using System;
using System.Collections.Generic;

namespace SistemaCadastroLivrosPOO.Entities
{
    public class Biblioteca
    {
        private readonly List<Livro> _livros;
        private readonly List<Cliente> _clientes;
        private readonly List<Funcionario> _funcionarios;
        private readonly List<Emprestimo> _emprestimos;

        public Biblioteca()
        {
            _livros = new List<Livro>();
            _clientes = new List<Cliente>();
            _funcionarios = new List<Funcionario>();
            _emprestimos = new List<Emprestimo>();
        }

        public void AdicionarLivro(Livro livro)
        {
            _livros.Add(livro);
            Console.WriteLine($"Livro '{livro.Titulo}' adicionado à biblioteca.");
        }

        public void RegistrarCliente(Cliente cliente)
        {
            _clientes.Add(cliente);
            Console.WriteLine($"Cliente '{cliente.Nome}' registrado.");
        }

        public void RegistrarFuncionario(Funcionario funcionario)
        {
            _funcionarios.Add(funcionario);
            Console.WriteLine($"Funcionário '{funcionario.Nome}' registrado.");
        }

        public void RealizarEmprestimo(Livro livro, Cliente cliente)
        {
            if (livro.Disponivel)
            {
                var emprestimo = new Emprestimo(livro, cliente);
                _emprestimos.Add(emprestimo);
                emprestimo.ConcluirEmprestimo();
            }
            else
            {
                Console.WriteLine($"O livro '{livro.Titulo}' não está disponível para empréstimo.");
            }
        }

        public void ReceberDevolucao(Livro livro, Cliente cliente)
        {
            var emprestimo = _emprestimos.Find(e => e.Livro == livro && e.Cliente == cliente);
            if (emprestimo != null)
            {
                emprestimo.DevolverLivro();
                _emprestimos.Remove(emprestimo);
            }
            else
            {
                Console.WriteLine($"Nenhum empréstimo encontrado para o livro '{livro.Titulo}' pelo cliente '{cliente.Nome}'.");
            }
        }

        public void ListarLivros()
        {
            Console.WriteLine("Lista de Livros na Biblioteca:");
            foreach (var livro in _livros)
            {
                Console.WriteLine($"- {livro.Titulo} (Disponível: {livro.Disponivel})");
            }
        }

        public void ListarClientes()
        {
            Console.WriteLine("Lista de Clientes Registrados:");
            foreach (var cliente in _clientes)
            {
                Console.WriteLine($"- {cliente.Nome} (CPF: {cliente.CPF})");
            }
        }

        public void ListarFuncionarios()
        {
            Console.WriteLine("Lista de Funcionários Registrados:");
            foreach (var funcionario in _funcionarios)
            {
                Console.WriteLine($"- {funcionario.Nome} (Cargo: {funcionario.Cargo})");
            }
        }
    }
}
