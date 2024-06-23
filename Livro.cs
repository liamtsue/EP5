using System;
using System.IO;
using System.Collections.Generic;


public class Livro {

public string nome; // NOME DO LIVRO
public int classificacao; //CONTROLADOR DO GENERO DO LIVRO
public int quantidade; //QUANTIDADE DESSE MESMO LIVRO PARA EMPRESTIMO

public List<Pessoa> leitores; 

 // METODO PARA CASO N TENHA RETORNO
  public Livro()
  {
      nome = "Livro Não Informado";
      quantidade = 0;
      leitores = new List<Pessoa>();
  }
//METODO COM RETORNO COMPLETO
  public Livro(string n, int e, int c)
  {
      this.nome = n;
      this.quantidade = e;
      this.classificacao = c;

//RELAÇÃO ENTRE PESSOA -> LIVRO
      leitores = new List<Pessoa>();
  }


  //METODO PARA CONTAR QUANTAS PESSOAS E QUE PESSOAS ESTÃO COM O LIVRO
  public bool EmprestarLivro(Pessoa p)
  {
      if (leitores.Count < quantidade)
      {
          leitores.Add(p);
          return true;
      }

      return false;
  }


//METODO PARA BUSCAR QUE LEITORES ESTÃO COM UM LIVRO ESPECIFICO
  public Pessoa BuscaLeitor(string nome)
  {

      for (int i = 0; i < leitores.Count; i++)
      {
          Pessoa p = leitores[i];

          if (p.getNome() == nome)
          {
              return p;
          }
      }

      return null;
  }


    public List<Pessoa> getListaLeitores()
    {
        return leitores;
    }

    //CONTAR QUANTIDADE DE LEITORES
    public int Quantidadeleitores()
    {
        return leitores.Count;
    }

    //GET DO LIVRO
    public string getNomeLivro(){
        return nome;
      }

      // METODO PARA SET DO ATRIBUTO NOME DO LIVRO
      public void setNomeLivro(string n){
        nome = n.ToUpper();
      }

    //LIMPAR LISTA DE LEITORES COM O LIVRO
    public void LimparEmprestimo()
    {
        leitores.Clear();
    }

    //FUNCAO PARA ADD LIVRO
    public void AddLivro(Livro l,string n, int i, int c){

    l = new Livro (n, i, c);

    }

    //METODO PARA CHECAR SE HÁ LIVROS DISPONIVEIS
    public bool AdicionarLeitor(Pessoa leitor)
    {
        if (leitores.Count < quantidade)
        {
            leitores.Add(leitor);
            return true;
        }
        else
        {
            Console.WriteLine("Limite de leitores atingido para este livro.");
            return false;
        }
    }




    //FUNÇÃO PARA IMPRIMIR RELATORIO DO LIVRO
    // 1 - IMPRIME A INFORMAÇÃO DE CADA LEITOR

    public void ImprimeRelatorioLeitores()
    {
        Console.WriteLine("=====================================================");
        Console.WriteLine("============ Relatório de Leitores    ===============");
        Console.WriteLine("=====================================================");

        //percorre toda a lista de passageiros para imprimir a informação de cada pessoa:
        for (int i = 0; i < leitores.Count; i++)
        {
            Pessoa p = leitores[i];
            Console.WriteLine($"Pessoa {i + 1}: {p.getNome()} ");
        }

        Console.WriteLine("=====================================================");



    }



    public void ImprimeRelatorioHTML(string arquivo_template)
        {

            string texto_arquivo = File.ReadAllText(arquivo_template);


            string linha_tabela = "<tr><td>{{nome_leitor}}</td></tr>";
            string linhas_preenchidas = "";

            for (int i = 0; i <leitores.Count; i++)
            {
                Pessoa p = leitores[i];
                string linha_aux = linha_tabela.Replace("{{nome_leitor}}", p.getNome());


                linhas_preenchidas += linha_aux + Environment.NewLine;
            }



            string nome_relatorio = "relatorios/Relatorio-Leitores.html";
            File.WriteAllText(nome_relatorio, texto_arquivo);
        }

    }



