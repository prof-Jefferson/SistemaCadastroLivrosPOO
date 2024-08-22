using SistemaCadastroLivrosPOO.Entities;

namespace SistemaCadastroLivrosPOO
{
    class Program
    {
        static void Main(string[] args)
        {
            Funcionario funcionario = new Funcionario("João Silva", "123456789", "Bibliotecário");
            Cliente cliente = new Cliente("Maria Oliveira", "987654321", "Rua das Flores, 123");

            funcionario.ObterIdentificacao();
            cliente.ObterIdentificacao();
        }
    }
}
