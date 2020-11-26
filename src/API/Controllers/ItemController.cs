using API.Model;
using API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ItemController : ControllerBase
  {
    private readonly ILogger<ItemController> _logger;

    private readonly IItemRepository _itemRepository;

    public ItemController(ILogger<ItemController> logger, IItemRepository itemRepository)
    {
      _logger = logger;

      _itemRepository = itemRepository;
    }

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Item))]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Get()
    {
      var item = await _itemRepository.LIFO().ConfigureAwait(false);

      if (item is null)
        return NotFound();

      return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(string))]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> Post(IEnumerable<Item> items)
    {
      if (items?.Any() != true)
        return NoContent();

      _ = await _itemRepository.Add(items).ConfigureAwait(false);

      return Created(@"\[controller]", null);
    }
  }
}
