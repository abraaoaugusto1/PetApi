using Microsoft.AspNetCore.Mvc;
using LHPetsApi.Models;

namespace LHPetsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        // Lista em memória
        private static List<Pet> _pets = new List<Pet>
        {
            new Pet { Id = 1, Nome = "Rex", Especie = "Cachorro", Raca = "Labrador", ClienteId = 1 },
            new Pet { Id = 2, Nome = "Mimi", Especie = "Gato", Raca = "Siamês", ClienteId = 2 }
        };

        private static int _proximoId = 3;

        // GET /api/pets
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> Get()
        {
            return Ok(_pets);
        }

        // POST /api/pets
        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet pet)
        {
            if (pet == null)
                return BadRequest("Pet inválido.");

            // Verifica se o ClienteId existe (opcional, mas recomendado)
            // (aqui estamos só validando se veio preenchido)
            if (pet.ClienteId <= 0)
                return BadRequest("ClienteId é obrigatório.");

            pet.Id = _proximoId++;
            _pets.Add(pet);

            return CreatedAtAction(nameof(Get), new { id = pet.Id }, pet);
        }

        // GET /api/pets/cliente/{clienteId}
        [HttpGet("cliente/{clienteId}")]
        public ActionResult<IEnumerable<Pet>> GetByCliente(int clienteId)
        {
            var petsDoCliente = _pets.Where(p => p.ClienteId == clienteId).ToList();

            if (!petsDoCliente.Any())
                return NotFound($"Nenhum pet encontrado para o cliente {clienteId}.");

            return Ok(petsDoCliente);
        }
    }
}