using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;

namespace Sukuna.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly IMapper _mapper;

    public ClientsController(IClientService clientService, IMapper mapper)
    {
        _clientService = clientService;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]

    public IActionResult CreateClient([FromBody] ClientResource clientCreate)
    {
        if (clientCreate == null)
            return BadRequest(ModelState);

        // Vérifie si client existe à partir du nom
        var clients = _clientService.ClientExists(clientCreate);

        if (clients)
        {
            ModelState.AddModelError("", "Client doesn't exists or Client already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var clientMap = _mapper.Map<Client>(clientCreate);

        if (!_clientService.CreateClient(clientMap))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }

    [HttpGet("{clientId}/clientOrders")]
    public IActionResult GetClientOrdersByClient(int clientId)
    {
        if (!_clientService.ClientExistsById(clientId))
            return NotFound();

        var clientOrders = _mapper.Map<List<ClientOrderResource>>(
            _clientService.GetClientOrdersByClient(clientId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(clientOrders);
    }

    [HttpGet("{clientId}")]
    [ProducesResponseType(200, Type = typeof(Client))]
    [ProducesResponseType(400)]
    public IActionResult GetClientById(int clientId)
    {
        if (!_clientService.ClientExistsById(clientId))
            return NotFound();

        var client = _mapper.Map<ClientResource>(_clientService.GetClientById(clientId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(client);
    }

    [HttpPost("{clientEmail},{clientMdp}")]
    [ProducesResponseType(200, Type = typeof(ClientResource))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public IActionResult GetAuthauthClient(string clientEmail, string clientMdp)
    {
        var client = _clientService.GetAuthauthClient(clientEmail, clientMdp);

        if (client == null)
            return Unauthorized(); // L'authentification a échoué

        var clientResource = _mapper.Map<ClientResource>(client);

        return Ok(clientResource); // Authentification réussie, retourne les données du client
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Client>))]
    public IActionResult GetClients()
    {
        var clients = _mapper.Map<List<ClientResource>>(_clientService.GetClients());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(clients);
    }

    [HttpPut("{clientId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdateClient(int clientId, [FromBody] ClientResource updatedClient)
    {
        if (updatedClient == null)
            return BadRequest(ModelState);

        if (clientId != updatedClient.ID)
            return BadRequest(ModelState);

        if (!_clientService.ClientExistsById(clientId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        var clientMap = _mapper.Map<Client>(updatedClient);

        if (!_clientService.UpdateClient(clientMap))
        {
            ModelState.AddModelError("", "Something went wrong updating owner");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully Updated");
    }


    [HttpDelete("{clientId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteClient(int clientId)
    {
        if (!_clientService.ClientExistsById(clientId))
        {
            return NotFound();
        }

        var clientToDelete = _clientService.GetClientById(clientId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_clientService.DeleteClient(clientToDelete))
        {
            ModelState.AddModelError("", "Something went wrong deleting client");
        }

        return Ok("Successfully deleted");
    }
}