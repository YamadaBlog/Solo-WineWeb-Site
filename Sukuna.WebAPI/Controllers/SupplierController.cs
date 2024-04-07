using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.Service.Services;

namespace Sukuna.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _supplierService;
    private readonly IMapper _mapper;

    public SuppliersController(ISupplierService supplierService, IMapper mapper)
    {
        _supplierService = supplierService;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]

    public IActionResult CreateSupplier([FromBody] SupplierResource supplierCreate)
    {
        if (supplierCreate == null)
            return BadRequest(ModelState);

        // Vérifie si supplier existe à partir du nom
        var suppliers = _supplierService.SupplierExists(supplierCreate);

        if (suppliers != null)
        {
            ModelState.AddModelError("", "Client doesn't exists or Supplier already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var supplierMap = _mapper.Map<Supplier>(supplierCreate);

        if (!_supplierService.CreateSupplier(supplierMap))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }

    [HttpGet("{supplierId}/articles")]
    public IActionResult GetArticlesBySupplier(int supplierId)
    {
        if (!_supplierService.SupplierExistsById(supplierId))
            return NotFound();

        var articles = _mapper.Map<List<ArticleResource>>(
            _supplierService.GetArticlesBySupplier(supplierId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(articles);
    }

    [HttpGet("{supplierId}")]
    [ProducesResponseType(200, Type = typeof(Supplier))]
    [ProducesResponseType(400)]
    public IActionResult GetSupplierById(int supplierId)
    {
        if (!_supplierService.SupplierExistsById(supplierId))
            return NotFound();

        var supplier = _mapper.Map<SupplierResource>(_supplierService.GetSupplierById(supplierId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(supplier);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Supplier>))]
    public IActionResult GetSuppliers()
    {
        var suppliers = _mapper.Map<List<SupplierResource>>(_supplierService.GetSuppliers());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(suppliers);
    }

    [HttpPut("{supplierId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdateSupplier(int supplierId, [FromBody] SupplierResource updatedSupplier)
    {
        if (updatedSupplier == null)
            return BadRequest(ModelState);

        if (supplierId != updatedSupplier.ID)
            return BadRequest(ModelState);

        if (!_supplierService.SupplierExistsById(supplierId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        var supplierMap = _mapper.Map<Supplier>(updatedSupplier);

        if (!_supplierService.UpdateSupplier(supplierMap))
        {
            ModelState.AddModelError("", "Something went wrong updating owner");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully Updated");
    }


    [HttpDelete("{supplierId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteSupplier(int supplierId)
    {
        if (!_supplierService.SupplierExistsById(supplierId))
        {
            return NotFound();
        }

        var supplierToDelete = _supplierService.GetSupplierById(supplierId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_supplierService.DeleteSupplier(supplierToDelete))
        {
            ModelState.AddModelError("", "Something went wrong deleting supplier");
        }

        return Ok("Successfully deleted");
    }
}