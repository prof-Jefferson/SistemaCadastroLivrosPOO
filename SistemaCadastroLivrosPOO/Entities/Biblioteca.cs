using SistemaCadastroLivrosPOO.Services;
using System.Collections.Generic;

namespace SistemaCadastroLivrosPOO.Entities
{
    public class Biblioteca
    {
        private readonly List<Livro> _livros;
        private readonly List<Cliente> _clientes;
        private readonly List<Funcionario> _funcionarios;
        private readonly List<Emprestimo> _emprestimos;

        private readonly GerenciadorDeArquivo<Livro> _gerenciadorLivros;
        private readonly GerenciadorDeArquivo<Cliente> _gerenciadorClientes;
        private readonly GerenciadorDeArquivo<Funcionario> _gerenciadorFuncionarios;
        private readonly GerenciadorDeArquivo<Emprestimo> _gerenciadorEmprestimos;

        public Biblioteca()
        {
            _gerenciadorLivros = new GerenciadorDeArquivo<Livro>("livros.json");
            _gerenciadorClientes = new GerenciadorDeArquivo<Cliente>("clientes.json");
            _gerenciadorFuncionarios = new GerenciadorDeArquivo<Funcionario>("funcionarios.json");
            _gerenciadorEmprestimos = new GerenciadorDeArquivo<Emprestimo>("emprestimos.json");

            _livros = _gerenciadorLivros.Carregar();
            _clientes = _gerenciadorClientes.Carregar();
            _funcionarios = _gerenciadorFuncionarios.Carregar();
            _emprestimos = _gerenciadorEmprestimos.Carregar();
        }

        public void AdicionarLivro(Livro livro)
        {
            _livros.Add(livro);
            _gerenciadorLivros.Salvar(_livros);
            Console.WriteLine($"Livro '{livro.Titulo}' adicionado à biblioteca.");
        }

        public void RegistrarCliente(Cliente cliente)
        {
            _clientes.Add(cliente);
            _gerenciadorClientes.Salvar(_clientes);
            Console.WriteLine($"Cliente '{cliente.Nome}' registrado.");
        }

        public void RegistrarFuncionario(Funcionario funcionario)
        {
            _funcionarios.Add(funcionario);
            _gerenciadorFuncionarios.Salvar(_funcionarios);
            Console.WriteLine($"Funcionário '{funcionario.Nome}' registrado.");
        }

        public void RealizarEmprestimo(Livro livro, Cliente cliente)
        {
            if (livro.Disponivel)
            {
                var emprestimo = new Emprestimo(livro, cliente);
                _emprestimos.Add(emprestimo);
                _gerenciadorEmprestimos.Salvar(_emprestimos);
                emprestimo.ConcluirEmprestimo();
                _gerenciadorLivros.Salvar(_livros);
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
                _gerenciadorEmprestimos.Salvar(_emprestimos);
                _gerenciadorLivros.Salvar(_livros);
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
