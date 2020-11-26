namespace API.Model
{
  public class MoedaCotacao : Moeda
  {
    public MoedaCotacao() : base() { }

    public MoedaCotacao(Moeda moeda, int codigo, decimal valor) : base(moeda.Id, moeda.Data)
    {
      Codigo = codigo;
      Valor = valor;
    }

    public int Codigo { get; set; }

    public decimal Valor { get; set; }

    public override string ToString()
    {
      return $"{base.ToString()};{Valor}";
    }
  }
}
