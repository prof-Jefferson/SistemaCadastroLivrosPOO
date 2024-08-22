using SistemaCadastroLivrosPOO.Entities;

namespace SistemaCadastroLivrosPOO
{
    class Program
    {
        static void Main(string[] args)
        {
            Livro livro = new Livro("O Senhor dos Anéis", "J.R.R. Tolkien", "978-8578270332");
            Cliente cliente = new Cliente("Maria Oliveira", "987654321", "Rua das Flores, 123");

            Emprestimo emprestimo = new Emprestimo(livro, cliente);
            emprestimo.ConcluirEmprestimo();

            emprestimo.DevolverLivro();
        }
    }
}
