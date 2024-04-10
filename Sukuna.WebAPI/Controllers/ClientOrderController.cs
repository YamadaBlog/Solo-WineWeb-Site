using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;

namespace Sukuna.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientOrdersController : ControllerBase
{
    private readonly IClientOrderService _clientOrderService;
    private readonly IMapper _mapper;

    public ClientOrdersController(IClientOrderService clientOrderService, IMapper mapper)
    {
        _clientOrderService = clientOrderService;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]

    public IActionResult CreateClientOrder([FromBody] ClientOrderResource clientOrderCreate)
    {
        if (clientOrderCreate == null)
            return BadRequest(ModelState);

        // Vérifie si clientOrder existe à partir du nom
        var clientOrders = _clientOrderService.ClientOrderExists(clientOrderCreate);

        if (clientOrders)
        {
            ModelState.AddModelError("", "Client doesn't exists or ClientOrder already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var clientOrderMap = _mapper.Map<ClientOrder>(clientOrderCreate);

        if (!_clientOrderService.CreateClientOrder(clientOrderMap))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }

        return Ok(clientOrderMap);
    }

    [HttpGet("{clientOrderId}/orderLines")]
    public IActionResult GetOrderLinesByClientOrder(int clientOrderId)
    {
        if (!_clientOrderService.ClientOrderExistsById(clientOrderId))
            return NotFound();

        var orderLines = _mapper.Map<List<OrderLineResource>>(
            _clientOrderService.GetOrderLinesByClientOrder(clientOrderId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(orderLines);
    }

    [HttpGet("{clientOrderId}")]
    [ProducesResponseType(200, Type = typeof(ClientOrder))]
    [ProducesResponseType(400)]
    public IActionResult GetClientOrderById(int clientOrderId)
    {
        if (!_clientOrderService.ClientOrderExistsById(clientOrderId))
            return NotFound();

        var clientOrder = _mapper.Map<ClientOrderResource>(_clientOrderService.GetClientOrderById(clientOrderId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(clientOrder);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ClientOrder>))]
    public IActionResult GetClientOrders()
    {
        var clientOrders = _mapper.Map<List<ClientOrderResource>>(_clientOrderService.GetClientOrders());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(clientOrders);
    }

    [HttpPut("{clientOrderId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdateClientOrder(int clientOrderId, [FromBody] ClientOrderResource updatedClientOrder)
    {
        if (updatedClientOrder == null)
            return BadRequest(ModelState);

        if (clientOrderId != updatedClientOrder.ID)
            return BadRequest(ModelState);

        if (!_clientOrderService.ClientOrderExistsById(clientOrderId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        var clientOrderMap = _mapper.Map<ClientOrder>(updatedClientOrder);

        if (!_clientOrderService.UpdateClientOrder(clientOrderMap))
        {
            ModelState.AddModelError("", "Something went wrong updating owner");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully Updated");
    }


    [HttpDelete("{clientOrderId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteClientOrder(int clientOrderId)
    {
        if (!_clientOrderService.ClientOrderExistsById(clientOrderId))
        {
            return NotFound();
        }

        var clientOrderToDelete = _clientOrderService.GetClientOrderById(clientOrderId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_clientOrderService.DeleteClientOrder(clientOrderToDelete))
        {
            ModelState.AddModelError("", "Something went wrong deleting clientOrder");
        }

        return Ok("Successfully deleted");
    }
}