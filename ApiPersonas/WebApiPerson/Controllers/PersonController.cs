using Microsoft.AspNetCore.Mvc;
using WebApiPerson.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApiPerson.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            return Ok(await _personService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _personService.GetByIdAsync(id);
            if (person == null)
                return NotFound(new { message = "Persona no encontrada." });

            return Ok(person);
        }

        [HttpPost]
        public async Task<ActionResult<Person>> CreatePerson([FromBody] Person person)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevaPersona = await _personService.CreateAsync(person);
            return CreatedAtAction(nameof(GetPerson), new { id = nuevaPersona.Id }, nuevaPersona);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] Person person)
        {
            if (id != person.Id)
                return BadRequest(new { message = "El ID en la URL no coincide con el del objeto." });

            var actualizado = await _personService.UpdateAsync(id, person);
            if (!actualizado)
                return NotFound(new { message = "Persona no encontrada." });

            return Ok(new { message = "Persona actualizada con éxito." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var eliminado = await _personService.DeleteAsync(id);
            if (!eliminado)
                return NotFound(new { message = "Persona no encontrada." });

            return Ok(new { message = "Persona eliminada con éxito." });
        }
    }
}
