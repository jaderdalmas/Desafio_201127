using System;

namespace API.Model
{
  public class Moeda
  {
    public Moeda() { }

    public Moeda(string id, DateTime data)
    {
      Id = id;
      Data = data;
    }

    public string Id { get; set; }

    public DateTime Data { get; set; }

    public override string ToString()
    {
      return $"{Id};{Data:yyyy-MM-dd}";
    }
  }
}
