using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Controllers
{
    [Route("api/[controller]")] // api/Tarefa
    [ApiController]
    public class TarefaController : ControllerBase
    {

        private readonly ITarefaRepositorio _tarefaRepositorio;

        public TarefaController(ITarefaRepositorio tarefaRepositorio)
        {
            _tarefaRepositorio = tarefaRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<TarefaModel>>> ListarTodas()
        {
            List<TarefaModel> tarefas = await _tarefaRepositorio.BuscarTodasTarefas();
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<TarefaModel>>> BuscarPorId(int id)
        {
            TarefaModel tarefa = await _tarefaRepositorio.BuscarPorId(id);
            return Ok(tarefa);
        }

        [HttpPost]
        public async Task<ActionResult<TarefaModel>> Cadastrar([FromBody] TarefaModel tarefa)
        {
            TarefaModel novatarefa = await _tarefaRepositorio.Adicionar(tarefa);
            return Ok(novatarefa);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TarefaModel>> Atualizar([FromBody] TarefaModel tarefa, int id)
        {
            tarefa.Id = id;
            TarefaModel novatarefa = await _tarefaRepositorio.Atualizar(tarefa, id);
            return Ok(novatarefa);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TarefaModel>> Apagar(int id)
        {
            bool apagado = await _tarefaRepositorio.Apagar(id);
            return Ok(apagado);
        }

    }
}
