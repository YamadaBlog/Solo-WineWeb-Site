using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.Service.Services;

namespace Sukuna.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderLinesController : ControllerBase
{
    private readonly IOrderLineService _orderLineService;
    private readonly IMapper _mapper;
    private readonly IClientOrderService _clientOrderService;

    public OrderLinesController(IOrderLineService orderLineService, IMapper mapper, IClientOrderService clientOrderService)
    {
        _orderLineService = orderLineService;
        _mapper = mapper;
        _clientOrderService = clientOrderService;
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]

    public IActionResult CreateOrderLine([FromBody] OrderLineResource orderLineCreate)
    {
        if (orderLineCreate == null)
            return BadRequest(ModelState);

        // Vérifie si orderLine existe à partir du l'id
        var orderLines = _orderLineService.OrderLineExists(orderLineCreate);

        if (orderLines)
        {
            ModelState.AddModelError("", "Command doesn't exists or OrderLine already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var orderLineMap = _mapper.Map<OrderLine>(orderLineCreate);

        if (!_orderLineService.CreateOrderLine(orderLineMap))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }

    [HttpGet("{orderLineId}")]
    [ProducesResponseType(200, Type = typeof(OrderLine))]
    [ProducesResponseType(400)]
    public IActionResult GetOrderLine(int orderLineId)
    {
        if (!_orderLineService.OrderLineExistsById(orderLineId))
            return NotFound();

        var orderLine = _mapper.Map<OrderLineResource>(_orderLineService.GetOrderLineById(orderLineId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(orderLine);
    }

    [HttpGet("clientOrder/{clientOrderId}")]
    [ProducesResponseType(200, Type = typeof(OrderLine))]
    [ProducesResponseType(400)]

    public IActionResult GetOrderLinesForAClientOrder(int clientOrderId)
    {
        var orderLines = _mapper.Map<List<OrderLineResource>>(_orderLineService.GetOrderLinesOfAClientOrder(clientOrderId));

        if (!ModelState.IsValid)
            return BadRequest();

        return Ok(orderLines);
    }

    [HttpGet("supplierOrder/{supplierOrderId}")]
    [ProducesResponseType(200, Type = typeof(OrderLine))]
    [ProducesResponseType(400)]

    public IActionResult GetOrderLinesForASupplierOrder(int supplierOrderId)
    {
        var orderLines = _mapper.Map<List<OrderLineResource>>(_orderLineService.GetOrderLinesOfASupplierOrder(supplierOrderId));

        if (!ModelState.IsValid)
            return BadRequest();

        return Ok(orderLines);
    }

    [HttpGet("article/{articleId}")]
    [ProducesResponseType(200, Type = typeof(OrderLine))]
    [ProducesResponseType(400)]

    public IActionResult GetOrderLinesForAArticle(int articleId)
    {
        var orderLines = _mapper.Map<List<OrderLineResource>>(_orderLineService.GetOrderLinesOfAArticle(articleId));

        if (!ModelState.IsValid)
            return BadRequest();

        return Ok(orderLines);
    }

    [HttpPut("{orderLineId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdateOrderLine(int orderLineId, [FromBody] OrderLineResource updatedOrderLine)
    {
        if (updatedOrderLine == null)
            return BadRequest(ModelState);

        if (orderLineId != updatedOrderLine.ID)
            return BadRequest(ModelState);

        if (!_orderLineService.OrderLineExistsById(orderLineId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        var orderLineMap = _mapper.Map<OrderLine>(updatedOrderLine);

        if (!_orderLineService.UpdateOrderLine(orderLineMap))
        {
            ModelState.AddModelError("", "Something went wrong updating owner");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully Updated");
    }

    [HttpDelete("{orderLineId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteOrderLine(int orderLineId)
    {
        if (!_orderLineService.OrderLineExistsById(orderLineId))
        {
            return NotFound();
        }

        var orderLineToDelete = _orderLineService.GetOrderLineById(orderLineId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_orderLineService.DeleteOrderLine(orderLineToDelete))
        {
            ModelState.AddModelError("", "Something went wrong deleting owner");
        }

        return NoContent();
    }

    // Added missing delete range of orderLines by a clientOrder **>CK
    [HttpDelete("/DeleteOrderLinesByClientOrder/{clientOrderId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteOrderLinesByClientOrder(int clientOrderId)
    {
        if (!_clientOrderService.ClientOrderExistsById(clientOrderId))
            return NotFound();

        var orderLinesToDelete = _clientOrderService.GetOrderLinesByClientOrder(clientOrderId).ToList();
        if (!ModelState.IsValid)
            return BadRequest();

        if (!_orderLineService.DeleteOrderLines(orderLinesToDelete))
        {
            ModelState.AddModelError("", "error deleting orderLines");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfully Deleted");
    }
}