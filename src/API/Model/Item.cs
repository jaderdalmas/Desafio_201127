using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Model
{
  public class Item
  {
    [StringLength(3, MinimumLength = 3)]
    [JsonPropertyName("moeda")]
    public string Moeda { get; set; }

    [Required]
    [JsonPropertyName("data_inicio")]
    public DateTime DataInicio { get; set; }

    [Required]
    [JsonPropertyName("data_fim")]
    public DateTime DataFim { get; set; }
  }
}
