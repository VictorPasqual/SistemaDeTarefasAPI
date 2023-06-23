using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Repositorios.Interfaces;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SistemaDeTarefas.Repositorios;

namespace SistemaDeTarefas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddEntityFrameworkMySql()
                .AddDbContext<SistemaDeTarefasDBContex>(
                    options => options.UseMySql(builder.Configuration.GetConnectionString("Database"),
                    new MySqlServerVersion(ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Database")))));


            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<ITarefaRepositorio, TarefaRepositorio>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Add CORS configuration here
            app.UseCors(options =>
            {
                options.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
