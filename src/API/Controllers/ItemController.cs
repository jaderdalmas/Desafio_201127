using API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ItemController : ControllerBase
  {
    private readonly ILogger<ItemController> _logger;

    public ItemController(ILogger<ItemController> logger)
    {
      _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
      Item item = null;

      if (item is null)
        return NotFound();

      return Ok(item);
    }

    [HttpPost]
    public IActionResult Post(IEnumerable<Item> items)
    {
      if (items?.Any() != true)
        return NoContent();

      return Created(string.Empty, null);
    }
  }
}
