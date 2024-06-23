using System;

public class Pessoa{
string nome;
  public int idade;

  string cpf ;


// PARAMETRO SIMPLES, CASO NÃO HAJA ENTRADA 
  public Pessoa(){
    nome = "<Nome não Informado>";
    cpf = "";
    idade = 0;
  }

  //PARAMETRO PARA CASO SO HAJA O NOME DE ENTRADA
  public Pessoa (string n){
this.nome = n;
cpf = "<CPF não Informado>";
    idade = 0;

  }

  public Pessoa (string n, int i, string c){
//GUARDA O NOME NO PARAMETRO  EM LETRAS MAISCULAS
    this.nome = n.ToUpper();

    // CHECA SE A IDADE É VALIDA
    if (i > 0)
    {
        idade = i;  
    }
    else
    {
        Console.WriteLine("Idade inválida."); 
        idade = 0;                            
    }





    // CHECA SE O CPF TEM O TAMANHO CERTO
    if (c.Length == 11){
this.cpf = c; 

    }else {
 Console.WriteLine("CPF invalido");

    }

    }

// METODOS //////////////////

  //METODO PARA GET DO ATRIBUTO NOME
  public string getNome(){
    return nome;
  }
  // METODO PARA SET DO ATRIBUTO NOME
  public void setNome(string n){
    nome = n.ToUpper();
  }

  public int getIdade(){
    return idade;
  }
  public void setIdade(int i){

    if (idade >0){
this.idade = i;

    }
    else {
Console.WriteLine("Idade invalida. ");
    }
  }

  public string getCpf(){
return cpf;

  }
  public void setCpf(string c){

    if (c.Length == 11){
    this.cpf = c; 

        }else {
      Console.WriteLine("CPF invalido");
      Console.WriteLine("Salvando como '0'... ");

         }
    }







  public override string ToString(){
    return $"[Nome: {nome}] => Idade: {idade}  - cpf: {cpf}.";
  } 


  }

