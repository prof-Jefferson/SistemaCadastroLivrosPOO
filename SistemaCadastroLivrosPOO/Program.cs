using SistemaCadastroLivrosPOO.Entities;

namespace SistemaCadastroLivrosPOO
{
    class Program
    {
        static void Main(string[] args)
        {
            Biblioteca biblioteca = new Biblioteca();

            Funcionario funcionario = new Funcionario("João Silva", "123456789", "Bibliotecário");
            Cliente cliente = new Cliente("Maria Oliveira", "987654321", "Rua das Flores, 123");
            Livro livro = new Livro("O Senhor dos Anéis", "J.R.R. Tolkien", "978-8578270332");

            biblioteca.RegistrarFuncionario(funcionario);
            biblioteca.RegistrarCliente(cliente);
            biblioteca.AdicionarLivro(livro);

            biblioteca.ListarFuncionarios();
            biblioteca.ListarClientes();
            biblioteca.ListarLivros();

            biblioteca.RealizarEmprestimo(livro, cliente);
            biblioteca.ListarLivros();

            biblioteca.ReceberDevolucao(livro, cliente);
            biblioteca.ListarLivros();
        }
    }
}
