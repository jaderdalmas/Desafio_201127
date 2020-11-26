using API;
using API.Extension;
using API.Model;
using API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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

    private readonly IServiceScope _serviceScope;
    private IServiceProvider ServiceProvider => _serviceScope.ServiceProvider;

    private IItemService Service => ServiceProvider.GetService<IItemService>();

    public ItemControllerTest(TestApplicationFactory<Startup> factory)
    {
      _factory = factory;
      _serviceScope = factory.Services.CreateScope();
    }

    [Fact]
    public async Task Post_Null()
    {
      // Arrange
      var client = _factory.CreateClient();
      List<Item> request = null;

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
    public async Task Post_NoContent()
    {
      // Arrange
      var client = _factory.CreateClient();
      var request = new List<Item>();

      // Act
      var response = await client.PostAsync("/Item", request.AsContent());

      // Assert
      Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.Empty(result);
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

    [Fact]
    public async Task Get_NotFound()
    {
      // Arrange
      var client = _factory.CreateClient();

      // Act
      var response = await client.GetAsync("/Item");

      // Assert
      Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var item = JsonSerializer.Deserialize<ProblemDetails>(result);
      Assert.NotEmpty(item.Title);
      Assert.NotEmpty(item.Type);
      Assert.NotEmpty(item.Extensions);
    }

    [Fact]
    public async Task Get()
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
      _ = await client.PostAsync("/Item", request.AsContent());

      // Act
      var response = await client.GetAsync("/Item");

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var item = JsonSerializer.Deserialize<Item>(result);
      Assert.Equal(request[2].Moeda, item.Moeda);
      Assert.Equal(request[2].DataInicio, item.DataInicio);
      Assert.Equal(request[2].DataFim, item.DataFim);
    }

    [Fact]
    public async Task GetItem()
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
          DataFim = DateTime.Parse("2020-12-01")
        },
        new Item()
        {
          Moeda = "JPY",
          DataInicio = DateTime.Parse("2000-03-11"),
          DataFim = DateTime.Parse("2000-03-30")
        }
      };
      _ = await client.PostAsync("/Item", request.AsContent());

      // Act
      var response = await client.GetAsync("/Item");

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      Assert.NotEmpty(result);

      var item = JsonSerializer.Deserialize<Item>(result);
      Service.PostCSV(item).Wait();
    }
  }
}
