using System;
using System.Collections.Generic;

public class Emprestimo
{
    //CRIA UMA LISTA DE LIVROS EMPRESTADOS
    private List<Livro> livrosEmprestados;

    public Emprestimo()
    {
        livrosEmprestados = new List<Livro>();
    }

    //BOOLEANO PARA EMPRESTAR OU NÃO
    public bool EmprestarLivro(Livro livro, Pessoa leitor)
    {
        if (livro.quantidade > 0)  // Verifica se há exemplares disponíveis
        {
            livro.AdicionarLeitor(leitor);
            livrosEmprestados.Add(livro);
            livro.quantidade--;   // Decrementa a quantidade disponível
            return true;
        }
        else
        {
            Console.WriteLine("Não há exemplares disponíveis para empréstimo deste livro.");
            return false;
        }
    }

    public List<Livro> ListarLivrosEmprestados()
    {
        return livrosEmprestados;
    }
}
