namespace API.Model
{
  public class DePara
  {
    public DePara() { }

    public DePara(string idMoeda, int codCotacao)
    {
      IdMoeda = idMoeda;
      CodCotacao = codCotacao;
    }

    public string IdMoeda { get; set; }

    public int CodCotacao { get; set; }
  }
}
