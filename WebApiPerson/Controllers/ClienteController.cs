using Microsoft.AspNetCore.Mvc;
using WebApiPerson.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApiPerson.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return Ok(await _clienteService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cliente>> ObtenerCliente(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null)
                return NotFound(new { message = "Cliente no encontrado." });

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> CrearCliente([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevoCliente = await _clienteService.CreateAsync(cliente);
            return CreatedAtAction(nameof(ObtenerCliente), new { id = nuevoCliente.Id }, nuevoCliente);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> ActualizarCliente(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.Id)
                return BadRequest(new { message = "El ID en la URL no coincide con el del objeto." });

            var actualizado = await _clienteService.UpdateAsync(id, cliente);
            if (!actualizado)
                return NotFound(new { message = "Cliente no encontrado." });

            return Ok(new { message = "Cliente actualizado con éxito." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            var eliminado = await _clienteService.DeleteAsync(id);
            if (!eliminado)
                return NotFound(new { message = "Cliente no encontrado." });

            return Ok(new { message = "Cliente eliminado con éxito." });
        }
    }
}
