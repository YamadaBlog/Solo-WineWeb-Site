using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;

namespace Sukuna.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]/sukuna")]
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

        var tvaTypes = _tvaTypeService.GetTvaTypeTrimToUpper(tvaTypeCreate);

        if (tvaTypes != null)
        {
            ModelState.AddModelError("", "Owner already exists");
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
}