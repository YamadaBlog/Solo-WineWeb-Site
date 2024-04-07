using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.Service.Services;

namespace Sukuna.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TvaTypesController : ControllerBase
{
    private readonly ITvaTypeService _tvaTypeService;
    private readonly IMapper _mapper;

    public TvaTypesController(ITvaTypeService tvaTypeService, IMapper mapper)
    {
        _tvaTypeService = tvaTypeService;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]

    public IActionResult CreateTvaType([FromBody] TvaTypeResource tvaTypeCreate)
    {
        if (tvaTypeCreate == null)
            return BadRequest(ModelState);

        // Vérifie si tvaType existe à partir du nom
        var tvaTypes = _tvaTypeService.TvaTypeExists(tvaTypeCreate);

        if (tvaTypes != null)
        {
            ModelState.AddModelError("", "Client doesn't exists or TvaType already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var tvaTypeMap = _mapper.Map<TvaType>(tvaTypeCreate);

        if (!_tvaTypeService.CreateTvaType(tvaTypeMap))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }

    [HttpGet("{tvaTypeId}")]
    [ProducesResponseType(200, Type = typeof(TvaType))]
    [ProducesResponseType(400)]
    public IActionResult GetTvaTypeById(int tvaTypeId)
    {
        if (!_tvaTypeService.TvaTypeExistsById(tvaTypeId))
            return NotFound();

        var tvaType = _mapper.Map<TvaTypeResource>(_tvaTypeService.GetTvaTypeById(tvaTypeId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(tvaType);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<TvaType>))]
    public IActionResult GetTvaTypes()
    {
        var tvaTypes = _mapper.Map<List<TvaTypeResource>>(_tvaTypeService.GetTvaTypes());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(tvaTypes);
    }

    [HttpPut("{tvaTypeId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdateTvaType(int tvaTypeId, [FromBody] TvaTypeResource updatedTvaType)
    {
        if (updatedTvaType == null)
            return BadRequest(ModelState);

        if (tvaTypeId != updatedTvaType.ID)
            return BadRequest(ModelState);

        if (!_tvaTypeService.TvaTypeExistsById(tvaTypeId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        var tvaTypeMap = _mapper.Map<TvaType>(updatedTvaType);

        if (!_tvaTypeService.UpdateTvaType(tvaTypeMap))
        {
            ModelState.AddModelError("", "Something went wrong updating owner");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully Updated");
    }


    [HttpDelete("{tvaTypeId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteTvaType(int tvaTypeId)
    {
        if (!_tvaTypeService.TvaTypeExistsById(tvaTypeId))
        {
            return NotFound();
        }

        var tvaTypeToDelete = _tvaTypeService.GetTvaTypeById(tvaTypeId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_tvaTypeService.DeleteTvaType(tvaTypeToDelete))
        {
            ModelState.AddModelError("", "Something went wrong deleting tvaType");
        }

        return Ok("Successfully deleted");
    }
}