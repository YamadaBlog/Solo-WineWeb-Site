using Microsoft.AspNetCore.Mvc;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;

namespace Sukuna.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("client")]
    public IActionResult CreateClientOrder(int clientId, List<OrderLine> orderLines)
    {
        try
        {
            _orderService.CreateClientOrder(clientId, orderLines);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("supplier")]
    public IActionResult CreateSupplierOrder(int userId, int supplierId, List<OrderLine> orderLines)
    {
        try
        {
            _orderService.CreateSupplierOrder(userId, supplierId, orderLines);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("client")]
    public IActionResult UpdateClientOrder(ClientOrder clientOrder)
    {
        try
        {
            _orderService.UpdateClientOrder(clientOrder);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("supplier")]
    public IActionResult UpdateSupplierOrder(SupplierOrder supplierOrder)
    {
        try
        {
            _orderService.UpdateSupplierOrder(supplierOrder);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("client/{clientId}")]
    public IActionResult CancelClientOrder(int clientId)
    {
        try
        {
            _orderService.CancelClientOrder(clientId);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("supplier/{supplierId}")]
    public IActionResult CancelSupplierOrder(int supplierId)
    {
        try
        {
            _orderService.CancelSupplierOrder(supplierId);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("client/{orderId}")]
    public IActionResult GetClientOrderById(int orderId)
    {
        try
        {
            var clientOrder = _orderService.GetClientOrderById(orderId);
            if (clientOrder == null)
                return NotFound();

            return Ok(clientOrder);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("supplier/{orderId}")]
    public IActionResult GetSupplierOrderById(int orderId)
    {
        try
        {
            var supplierOrder = _orderService.GetSupplierOrderById(orderId);
            if (supplierOrder == null)
                return NotFound();

            return Ok(supplierOrder);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // Ajoutez les autres méthodes d'API nécessaires pour gérer les commandes, les lignes de commande, etc.
}
