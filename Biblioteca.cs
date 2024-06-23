using System;
using System.IO;
using System.Text;
using System.Collections.Generic;



public class Biblioteca
{


    public List<Livro> livros;

    //QUANTIDADE DE LIVROS
    int quantidade;

    public void ListaLivros()
    {

        for (int i = 0; i < livros.Count; i++)
        {
            Livro l = livros[i];
            Console.WriteLine($"Livro {i + 1}: {l.getNomeLivro()} ");
        }

    }

    //PROCURAR LIVRO POR NOME
    public Livro BuscaLivro(string n)
    {

        for (int i = 0; i < livros.Count; i++)
        {
            Livro l = livros[i];

            if (l.getNomeLivro() == n)
            {
                return l;
            }
        }

        return null;
    }

    //ADICIONAR LIVRO
    public bool AdicionarLivro(Livro l)
    {
        if (livros.Count < quantidade)
        {
            livros.Add(l);
            return true;
        }

        return false;
    }


    //METODO PARA RELATORIO HTML DE LIVROS EMPRESTADOS
    public void ImprimeRelatorioHTML(string nomeArquivo, List<Livro> livros)
    {
        StringBuilder htmlBuilder = new StringBuilder();
        htmlBuilder.AppendLine("<html>");
        htmlBuilder.AppendLine("<head><title>Relatório de Livros Emprestados</title></head>");
        htmlBuilder.AppendLine("<body>");
        htmlBuilder.AppendLine("<h1>Relatório de Livros Emprestados</h1>");

        foreach (Livro livro in livros)
        {
            if (livro.Quantidadeleitores() > 0)
            {
                htmlBuilder.AppendLine($"<h2>Livro: {livro.getNomeLivro()}</h2>");
                htmlBuilder.AppendLine("<ul>");
                List<Pessoa> leitores = livro.getListaLeitores();
                foreach (Pessoa leitor in leitores)
                {
                    htmlBuilder.AppendLine($"<li>{leitor.getNome()}</li>");
                }
                htmlBuilder.AppendLine("</ul>");
            }
        }

        htmlBuilder.AppendLine("</body>");
        htmlBuilder.AppendLine("</html>");

        File.WriteAllText(nomeArquivo, htmlBuilder.ToString());
    }






    //LIVRO 1
    public bool CarregarDadosLivros(string arquivo)
    {
        // Leitura de dados do arquivo, linha por linha.  
        string[] linhas = File.ReadAllLines(arquivo);

        foreach (string linha in linhas)
        {
            string[] dados = linha.Split(";");
            string nome = dados[0];
            int classificacao = int.Parse(dados[1]);
            int quantidade = int.Parse(dados[2]);
            AdicionarLivro(new Livro(nome, classificacao, quantidade));

        }
        return true;
    }


    //MENU DE ACESSO DO USUARIO 
    public void MenuInicial()
    {

        Pessoa leitor = new Pessoa();

        Emprestimo emprestimo = new Emprestimo();

        string opcao = "";


        while (opcao != "0")
        {
            Console.Clear();

            Console.WriteLine("Opcões: ");
            Console.WriteLine("0 - Sair.");
            Console.WriteLine("1 - Cadastrar novo leitor");
            Console.WriteLine("2 - Adicionar novo livro.");
            Console.WriteLine("3 - Emprestar livro a leitor");
            Console.WriteLine("4 - Relátorio de livros emprestados.");
            Console.WriteLine("5 - Carregar dados.");
            Console.WriteLine();
            Console.WriteLine("Informe a opção desejada: ");
            opcao = Console.ReadLine();


            switch (opcao)
            {
                case "0":
                    return;

                //CADASTRO DE NOVO LEITOR
                case "1":
                    Console.WriteLine("Informe o nome do leitor:");
                    string nome = Console.ReadLine();
                    Console.WriteLine("Informe a idade do leitor:");
                    int idade = int.Parse(Console.ReadLine());
                    Console.WriteLine("Informe o CPF do leitor:");
                    string cpf = Console.ReadLine();
                    leitor = new Pessoa(nome, idade, cpf);

                    Console.WriteLine("Leitor cadastrado!");
                    break;

                //ADICIONAR NOVO LIVRO
                case "2":
                    Console.WriteLine("Informe o nome do livro:");
                    string nomeL = Console.ReadLine();
                    Console.WriteLine("Informe a classificação indicativa do livro:");
                    int clas = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Informe o nome do livro");
                    int quant = Convert.ToInt32(Console.ReadLine());


                    //CADASTRANDO NOVO LIVRO
                    Livro novoLivro = new Livro(nomeL, clas, quant);
                    AdicionarLivro(novoLivro);
                    Console.WriteLine("Novo livro cadastrado");


                    break;


                //EMPRESTAR NOVO LIVRO
                case "3":


                    Console.WriteLine("Lista de livros disponíveis para empréstimo:");
                    //IMPRIME A LISTA DE LIVROS
                    ListaLivros();

                    Console.WriteLine("Informe o nome do livro que deseja emprestar:");
                    string nomeLivroEmprestimo = Console.ReadLine();

                    //BUSCA O NOME DO LIVRO PARA EMRESTIMO
                    Livro livroEmprestimo = BuscaLivro(nomeLivroEmprestimo);

                    //SE O LIVRO EMPRESTADO FOI ENCONTRADO CONTINUA
                    if (livroEmprestimo != null)
                    {
                        //CRIA UM BOOLEANO PARA CHECAR USANDO A VARIAVEL "emprestimo" COM A FUNÇAO "EmprestarLivro" DA CLASSE EMPRESTIMO
                        bool emprestadoComSucesso = emprestimo.EmprestarLivro(livroEmprestimo, leitor);
                        if (emprestadoComSucesso)
                        {
                            //RETORNA UMA MENSAGEM NO CONSOLE USNDO O NOME DO LIVRO E O NOME DO LEITOR
                            Console.WriteLine($"Livro '{livroEmprestimo.getNomeLivro()}' emprestado para '{leitor.getNome()}' com sucesso.");
                        }
                    }

                    //RETORNO CASO NÃO ENCONTRE O NOME DO LIVRO
                    else
                    {
                        Console.WriteLine("Livro não encontrado.");
                    }
                    break;



                //RELATORIO DE LIVROS EMPRESTADOS
                case "4":
                    Console.WriteLine("Relatório de Livros Emprestados");
                    Console.WriteLine("================================");

                    //CRIA UM BOOLEANO PARA VERIFICAR SE HÁ LIVROS EMPRESTADOS
                    bool temLivrosEmprestados = false;

                    //FUNÇÃO FOREACH PARA EXECUTAR UMA AÇÃO EM CADA ELEMENTO

                    foreach (Livro livro in livros)
                    {
                        if (livro.Quantidadeleitores() > 0)
                        {
                            //GET DO NOME DO LIVRO
                            Console.WriteLine($"Livro: {livro.getNomeLivro()}");
                            Console.WriteLine("Leitores:");
                            //IMPRIME LISTA DE LEITORES
                            List<Pessoa> leitores = livro.getListaLeitores();

                            // "Pessoa leitorl" É UMA VARIAVEL DIFERENTE PARA O LIVRO
                            foreach (Pessoa leitorl in leitores)
                            {
                                Console.WriteLine($" - {leitor.getNome()}");
                            }
                            Console.WriteLine();

                            //RETORNA PARA TRUE
                            temLivrosEmprestados = true;
                        }
                    }

                    // CASO NÃO TENHA LIVROS EMPRESTADOS
                    if (!temLivrosEmprestados)
                    {
                        Console.WriteLine("Não há livros emprestados no momento.");
                    }

                    Console.WriteLine("================================");
                    Console.WriteLine();

                    //RETORNO PARA SALVAR O RELATORIO EM HTML
                    Console.WriteLine("Deseja salvar o relatório em HTML? (S/N)");

                    // VARIAVEL PARA GUARDAR A REPSOTA DO USUARIO
                    string resposta = Console.ReadLine();

                    // "ToUpper PARA GUARDAR OS CARACTRES EM MAISUCULO"
                    if (resposta.ToUpper() == "S")
                    {
                        try
                        {
                            ImprimeRelatorioHTML("relatorios/Relatorio-Livros-Emprestados.html", livros);
                            Console.WriteLine("Relatório salvo em HTML com sucesso.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Erro ao salvar o relatório em HTML: {ex.Message}");
                        }
                    }
                    break;

                case "5":
                    Console.WriteLine("Carregando dados...");
                    Console.WriteLine("Informe o caminho completo do arquivo (.txt) de dados:");

                    //VARIAVEL PARA GUARDAR O CAMINHO DO ARQUIVO .TXT
                    string caminhoArquivo = Console.ReadLine();

                    try
                    {
                        bool carregadoComSucesso = CarregarDadosLivros(caminhoArquivo);

                        if (carregadoComSucesso)
                        {
                            Console.WriteLine("Dados carregados com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Não foi possível carregar os dados.");
                        }
                    }
                    catch (Exception ex)
                    {
                        //MENSAGEM DE ERRO CASO O CAMINHO NÃO SEJA ENCONTRADO
                        Console.WriteLine($"Erro ao carregar os dados: {ex.Message}");
                    }
                    Console.WriteLine("Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                    break;



            }
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {

            Biblioteca biblioteca = new Biblioteca();

            // Exemplo de carregamento inicial de dados
            biblioteca.livros = new List<Livro>(); // Inicializa a lista de livros
            biblioteca.quantidade = 10; // Define a quantidade máxima de livros

            // Aqui você pode adicionar alguns livros de exemplo para testar
            biblioteca.AdicionarLivro(new Livro("Livro A", 12, 3));
            biblioteca.AdicionarLivro(new Livro("Livro B", 14, 2));

            // Menu inicial
            biblioteca.MenuInicial();
        }
    }




}