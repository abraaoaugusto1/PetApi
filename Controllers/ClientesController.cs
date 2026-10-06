using Microsoft.AspNetCore.Mvc;
using LHPetsApi.Models;

namespace LHPetsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        // Lista em memória (simulando o banco)
        private static List<Cliente> _clientes = new List<Cliente>
        {
            new Cliente { Id = 1, Nome = "João Silva", Cpf = "123.456.789-00", Email = "joao@email.com" },
            new Cliente { Id = 2, Nome = "Maria Souza", Cpf = "987.654.321-00", Email = "maria@email.com" }
        };

        private static int _proximoId = 3;

        // GET /api/clientes
        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            return Ok(_clientes);
        }

        // POST /api/clientes
        [HttpPost]
        public ActionResult<Cliente> Post([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest("Cliente inválido.");

            cliente.Id = _proximoId++;
            _clientes.Add(cliente);

            return CreatedAtAction(nameof(Get), new { id = cliente.Id }, cliente);
        }
    }
}