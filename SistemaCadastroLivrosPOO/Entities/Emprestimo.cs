using System;

namespace SistemaCadastroLivrosPOO.Entities
{
    public class Emprestimo
    {
        public Livro Livro { get; private set; }
        public Cliente Cliente { get; private set; }
        public DateTime DataEmprestimo { get; private set; }
        public DateTime DataDevolucao { get; private set; }

        public Emprestimo(Livro livro, Cliente cliente)
        {
            Livro = livro;
            Cliente = cliente;
            DataEmprestimo = DateTime.Now;
            DataDevolucao = DataEmprestimo.AddDays(14); // Padrão de 14 dias para devolução
        }

        public void ConcluirEmprestimo()
        {
            Livro.Emprestar();
            Console.WriteLine($"Emprestimo concluído para o cliente {Cliente.Nome}. Data de devolução: {DataDevolucao.ToShortDateString()}");
        }

        public void DevolverLivro()
        {
            Livro.Devolver();
            Console.WriteLine($"Livro {Livro.Titulo} devolvido pelo cliente {Cliente.Nome}");
        }
    }
}
