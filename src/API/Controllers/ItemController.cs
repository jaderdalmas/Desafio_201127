using API.Model;
using API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
  /// <inheritdoc/>
  [ApiController]
  [Route("[controller]")]
  public class ItemController : ControllerBase
  {
    [SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Keep file format as a sample")]
    private readonly ILogger<ItemController> _logger;

    private readonly IItemRepository _itemRepository;

    /// <inheritdoc/>
    public ItemController(ILogger<ItemController> logger, IItemRepository itemRepository)
    {
      _logger = logger;

      _itemRepository = itemRepository;
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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
