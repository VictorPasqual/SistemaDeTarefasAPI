using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Controllers
{
    [Route("api/[controller]")] // api/Usuario
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioModel>>> BuscarTodosUsuarios()
        {
            List<UsuarioModel> usuarios = await _usuarioRepositorio.BuscarTodosUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<UsuarioModel>>> BuscarPorId(int id)
        {
            UsuarioModel usuario = await _usuarioRepositorio.BuscarPorId(id);
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> Cadastrar([FromBody] UsuarioModel usuario)
        {
            UsuarioModel novousuario = await _usuarioRepositorio.Adicionar(usuario);
            return Ok(novousuario);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioModel>> Atualizar([FromBody] UsuarioModel usuario, int id)
        {
            usuario.Id = id;
            UsuarioModel novousuario = await _usuarioRepositorio.Atualizar(usuario, id);
            return Ok(novousuario);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioModel>> Apagar(int id)
        {
            bool apagado = await _usuarioRepositorio.Apagar(id);
            return Ok(apagado);
        }

    }
}
