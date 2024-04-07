using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;

namespace Sukuna.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupplierOrdersController : ControllerBase
{
    private readonly ISupplierOrderService _supplierOrderService;
    private readonly IMapper _mapper;

    public SupplierOrdersController(ISupplierOrderService supplierOrderService, IMapper mapper)
    {
        _supplierOrderService = supplierOrderService;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]

    public IActionResult CreateSupplierOrder([FromBody] SupplierOrderResource supplierOrderCreate)
    {
        if (supplierOrderCreate == null)
            return BadRequest(ModelState);

        // Vérifie si supplierOrder existe à partir du nom
        var supplierOrders = _supplierOrderService.SupplierOrderExists(supplierOrderCreate);

        if (supplierOrders)
        {
            ModelState.AddModelError("", "Supplier doesn't exists or SupplierOrder already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var supplierOrderMap = _mapper.Map<SupplierOrder>(supplierOrderCreate);

        if (!_supplierOrderService.CreateSupplierOrder(supplierOrderMap))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }

    [HttpGet("{supplierOrderId}/orderLines")]
    public IActionResult GetOrderLinesBySupplierOrder(int supplierOrderId)
    {
        if (!_supplierOrderService.SupplierOrderExistsById(supplierOrderId))
            return NotFound();

        var orderLines = _mapper.Map<List<OrderLineResource>>(
            _supplierOrderService.GetOrderLinesBySupplierOrder(supplierOrderId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(orderLines);
    }

    [HttpGet("{supplierOrderId}")]
    [ProducesResponseType(200, Type = typeof(SupplierOrder))]
    [ProducesResponseType(400)]
    public IActionResult GetSupplierOrderById(int supplierOrderId)
    {
        if (!_supplierOrderService.SupplierOrderExistsById(supplierOrderId))
            return NotFound();

        var supplierOrder = _mapper.Map<SupplierOrderResource>(_supplierOrderService.GetSupplierOrderById(supplierOrderId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(supplierOrder);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SupplierOrder>))]
    public IActionResult GetSupplierOrders()
    {
        var supplierOrders = _mapper.Map<List<SupplierOrderResource>>(_supplierOrderService.GetSupplierOrders());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(supplierOrders);
    }

    [HttpPut("{supplierOrderId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdateSupplierOrder(int supplierOrderId, [FromBody] SupplierOrderResource updatedSupplierOrder)
    {
        if (updatedSupplierOrder == null)
            return BadRequest(ModelState);

        if (supplierOrderId != updatedSupplierOrder.ID)
            return BadRequest(ModelState);

        if (!_supplierOrderService.SupplierOrderExistsById(supplierOrderId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        var supplierOrderMap = _mapper.Map<SupplierOrder>(updatedSupplierOrder);

        if (!_supplierOrderService.UpdateSupplierOrder(supplierOrderMap))
        {
            ModelState.AddModelError("", "Something went wrong updating owner");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully Updated");
    }


    [HttpDelete("{supplierOrderId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteSupplierOrder(int supplierOrderId)
    {
        if (!_supplierOrderService.SupplierOrderExistsById(supplierOrderId))
        {
            return NotFound();
        }

        var supplierOrderToDelete = _supplierOrderService.GetSupplierOrderById(supplierOrderId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_supplierOrderService.DeleteSupplierOrder(supplierOrderToDelete))
        {
            ModelState.AddModelError("", "Something went wrong deleting supplierOrder");
        }

        return Ok("Successfully deleted");
    }
}