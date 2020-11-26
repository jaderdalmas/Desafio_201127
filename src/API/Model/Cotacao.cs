using System;

namespace API.Model
{
  public class Cotacao
  {
    public Cotacao() { }

    public Cotacao(decimal valor, int codigo, DateTime data)
    {
      Valor = valor;
      Codigo = codigo;
      Data = data;
    }

    public decimal Valor { get; set; }

    public int Codigo { get; set; }

    public DateTime Data { get; set; }
  }
}
