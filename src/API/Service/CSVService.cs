using API.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Service
{
  /// <inheritdoc/>
  public class CSVService : ICSVService
  {
    private readonly ILogger _logger;
    private string DateFormat => "dd/MM/yyyy";

    /// <inheritdoc/>
    public CSVService(ILogger<CSVService> logger)
    {
      _logger = logger;
    }

    /// <inheritdoc/>
    public Task<IList<Cotacao>> GetCotacao()
    {
      var lines = File.ReadAllLines("Data/DadosCotacao.csv").Select(a => a.Split(';')).Skip(1);

      IList<Cotacao> result = new List<Cotacao>();
      foreach (var line in lines.AsParallel())
      {
        if (line.Length == 3)
        {
          if (!decimal.TryParse(line[0], out decimal valor))
            _logger.LogInformation("Cannot Parse (decimal)", line[0]);
          if (!int.TryParse(line[1], out int codigo))
            _logger.LogInformation("Cannot Parse (int)", line[1]);
          if (!DateTime.TryParseExact(line[2], DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime data))
            _logger.LogInformation("Cannot Parse (date)", line[2]);

          result.Add(new Cotacao(valor, codigo, data));
        }
        else
        {
          _logger.LogInformation("Wrong Line (GetCotacao)", line);
        }
      }

      return Task.FromResult(result);
    }

    /// <inheritdoc/>
    public Task<IList<Moeda>> GetMoeda()
    {
      var lines = File.ReadAllLines("Data/DadosMoeda.csv").Select(a => a.Split(';'));

      IList<Moeda> result = new List<Moeda>();
      foreach (var line in lines.AsParallel())
      {
        if (line.Length == 2)
        {
          if (!DateTime.TryParse(line[1], out DateTime data))
            _logger.LogInformation("Cannot Parse (date)", line[1]);

          result.Add(new Moeda(line[0], data));
        }
        else
        {
          _logger.LogInformation("Wrong Line (GetMoeda)", line);
        }
      }

      return Task.FromResult(result);
    }

    /// <inheritdoc/>
    public Task PostMoedaCotacao(IEnumerable<MoedaCotacao> moedas)
    {
      var result = moedas.Select(x => x.ToString()).ToList();
      result.Insert(0, "ID_MOEDA;DATA_REF;VL_COTACAO");

      File.WriteAllLines($"Data/Resultado_{DateTime.Now:yyyyMMdd_HHmmss}.csv", result);

      return Task.CompletedTask;
    }
  }
}
