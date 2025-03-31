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
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return Ok(await _pedidoService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _pedidoService.GetByIdAsync(id);
            if (pedido == null)
                return NotFound(new { message = "Pedido no encontrado." });

            return Ok(pedido);
        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> CrearPedido([FromBody] Pedido pedido)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevoPedido = await _pedidoService.CreateAsync(pedido);
            return CreatedAtAction(nameof(GetPedido), new { id = nuevoPedido.Id }, nuevoPedido);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> ActualizarPedido(int id, [FromBody] Pedido pedido)
        {
            if (id != pedido.Id)
                return BadRequest(new { message = "El ID en la URL no coincide con el del objeto." });

            var actualizado = await _pedidoService.UpdateAsync(id, pedido);
            if (!actualizado)
                return NotFound(new { message = "Pedido no encontrado." });

            return Ok(new { message = "Pedido actualizado con éxito." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> EliminarPedido(int id)
        {
            var eliminado = await _pedidoService.DeleteAsync(id);
            if (!eliminado)
                return NotFound(new { message = "Pedido no encontrado." });

            return Ok(new { message = "Pedido eliminado con éxito." });
        }
    }
}
