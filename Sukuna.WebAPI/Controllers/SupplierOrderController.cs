using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;

namespace Sukuna.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]/sukuna")]
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
            ModelState.AddModelError("", "User doesn't exists or SupplierOrder already exists");
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
}