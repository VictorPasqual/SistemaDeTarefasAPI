using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly SistemaDeTarefasDBContex _dbContext;

        public TarefaRepositorio(SistemaDeTarefasDBContex sistemaDeTarefasDBContex)
        {
            _dbContext = sistemaDeTarefasDBContex;
        }
        public async Task<TarefaModel> BuscarPorId(int id)
        {
            return await _dbContext.Tarefas
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TarefaModel>> BuscarTodasTarefas()
        {
            return await _dbContext.Tarefas
                .Include(x => x.Usuario)
                .ToListAsync();
        }

        public async Task<TarefaModel> Adicionar(TarefaModel tarefa)
        {
            await _dbContext.Tarefas.AddAsync(tarefa);
            await _dbContext.SaveChangesAsync();

            return tarefa;
        }

        public async Task<TarefaModel> Atualizar(TarefaModel tarefa, int id)
        {
            TarefaModel tarefaPorId = await BuscarPorId(id);

            if(tarefaPorId == null)
            {
                throw new Exception($"Tarefa para o ID: {id} não foi encontrado no banco de dados.");
            }
            try
            {
                tarefaPorId.Nome = tarefa.Nome;
                tarefaPorId.Descricao = tarefa.Descricao;
                tarefaPorId.Status = tarefa.Status;
                tarefaPorId.UsuarioId = tarefa.UsuarioId;

                _dbContext.Tarefas.Update(tarefaPorId);
                await _dbContext.SaveChangesAsync();

                return tarefaPorId;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is MySqlException mysqlException && mysqlException.Number == 1452)
                {
                    throw new Exception("Não foi possível associar a tarefa a um usuário válido. Verifique o ID do usuário.");
                }
                else
                {
                    throw; // Ou retorne uma mensagem de erro mais genérica
                }
            }
        }

        public async Task<bool> Apagar(int id)
        {
            TarefaModel tarefaPorId = await BuscarPorId(id);

            if(tarefaPorId == null)
            {
                throw new Exception($"Tarefa para o ID: {id} não foi encontrado no banco de dados.");
            }

            _dbContext.Tarefas.Remove(tarefaPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

    }
}
