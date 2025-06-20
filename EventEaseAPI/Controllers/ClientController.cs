using Microsoft.AspNetCore.Mvc;
using EventEaseAPI.Interfaces;
using EventEaseAPI.DTOs.Client;

namespace EventEaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        [Route("GetAllClients")]
        public async Task<ActionResult<IEnumerable<ClientReadDto>>> GetAllClients()
        {
            var clients = await _clientService.GetAllAsync();
            return Ok(clients);
        }

        [HttpGet]
        [Route("GetClientById")]
        public async Task<ActionResult<ClientReadDto>> GetClientById(int id)
        {
            var client = await _clientService.GetByIdAsync(id);
            if (client == null) return NotFound(new { message = $"Client with ID {id} not found." });
            return Ok(client);
        }

        [HttpPost]
        [Route("CreateClient")]
        public async Task<ActionResult<ClientReadDto>> CreateClient([FromBody] ClientCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdClient = await _clientService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetClientById), new { id = createdClient.ClientId }, createdClient);
        }

        [HttpPut]
        [Route("UpdateClient")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] ClientUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _clientService.UpdateAsync(id, dto);
            if (!success) return NotFound(new { message = $"Client with ID {id} not found." });
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteClient")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var success = await _clientService.DeleteAsync(id);
            if (!success) return NotFound(new { message = $"Client with ID {id} not found." });
            return NoContent();
        }
    }
}
