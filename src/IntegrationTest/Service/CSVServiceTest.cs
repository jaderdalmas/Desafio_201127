using API.Model;
using API.Service;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.Service
{
  public class CSVServiceTest
  {
    [Fact]
    public async Task GetCotacao()
    {
      // Arrange
      var log = new Mock<ILogger<CSVService>>();
      var service = new CSVService(log.Object);

      // Act
      var cotacao = await service.GetCotacao().ConfigureAwait(false);

      // Assert
      Assert.NotEmpty(cotacao);
    }

    [Fact]
    public async Task GetMoeda()
    {
      // Arrange
      var log = new Mock<ILogger<CSVService>>();
      var service = new CSVService(log.Object);

      // Act
      var moeda = await service.GetMoeda().ConfigureAwait(false);

      // Assert
      Assert.NotEmpty(moeda);
    }

    [Fact]
    public async Task PostMoedaCotacao()
    {
      // Arrange
      var log = new Mock<ILogger<CSVService>>();
      var service = new CSVService(log.Object);

      IList<MoedaCotacao> list = new List<MoedaCotacao>()
      {
        new MoedaCotacao()
        {
          Id = "USD",
          Data = DateTime.Now.Date,
          Codigo = 1,
          Valor = (decimal)1.69
        }
      };

      // Act
      await service.PostMoedaCotacao(list).ConfigureAwait(false);

      // Assert
      Assert.NotEmpty(list);
    }
  }
}
