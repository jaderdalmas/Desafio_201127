using API;
using API.Extension;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.Controller
{
  public class ItemControllerTest : IClassFixture<TestApplicationFactory<Startup>>
  {
    private readonly TestApplicationFactory<Startup> _factory;

    public ItemControllerTest(TestApplicationFactory<Startup> factory)
    {
      _factory = factory;
    }

    [Fact]
    public async Task Post_BadRequest()
    {
      // Arrange
      var client = _factory.CreateClient();
      var request = new List<Item>() {
        new Item(){
          Moeda = "USDT",
          DataInicio = DateTime.Parse("2010-01-01"),
          DataFim = DateTime.Parse("2010-12-01")
        },
        new Item()
        {
          Moeda = "EUR",
          DataInicio = DateTime.Parse("2020-01-01"),
          DataFim = DateTime.MinValue
        },
        new Item()
        {
          Moeda = "JPY",
          DataInicio = DateTime.MinValue,
          DataFim = DateTime.Parse("2000-03-30")
        }
      };

      // Act
      var response = await client.PostAsync("/Item", request.AsContent());

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var item = JsonSerializer.Deserialize<ProblemDetails>(result);
      Assert.NotEmpty(item.Title);
      Assert.NotEmpty(item.Type);
      Assert.NotEmpty(item.Extensions);
    }

    [Fact]
    public async Task Post()
    {
      // Arrange
      var client = _factory.CreateClient();
      var request = new List<Item>() {
        new Item(){
          Moeda = "USD",
          DataInicio = DateTime.Parse("2010-01-01"),
          DataFim = DateTime.Parse("2010-12-01")
        },
        new Item()
        {
          Moeda = "EUR",
          DataInicio = DateTime.Parse("2020-01-01"),
          DataFim = DateTime.Parse("2010-12-01")
        },
        new Item()
        {
          Moeda = "JPY",
          DataInicio = DateTime.Parse("2000-03-11"),
          DataFim = DateTime.Parse("2000-03-30")
        }
      };

      // Act
      var response = await client.PostAsync("/Item", request.AsContent());

      // Assert
      Assert.Equal(HttpStatusCode.Created, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.Empty(result);
    }
  }
}
