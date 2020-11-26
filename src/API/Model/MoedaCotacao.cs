namespace API.Model
{
  public class MoedaCotacao : Moeda
  {
    public MoedaCotacao() : base() { }

    public MoedaCotacao(Moeda moeda) : base(moeda.Id, moeda.Data) { }

    public int Codigo { get; set; }

    public decimal Valor { get; set; }

    public override string ToString()
    {
      return $"{base.ToString()};{Valor}";
    }
  }
}
