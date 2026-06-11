using Dapper;
using Npgsql;
using LGG.Dominio.Library.Entidades;
using LGG.Dominio.Library.Interfaces.IRepositorios;

namespace LGG.Infraestrutura.Library.Repositorios
{
    public class EmailBaldeRepositorio : IEmailBaldeRepositorio
    {
        private readonly string _connectionString;

        public EmailBaldeRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AdicionarAsync(EmailBalde emailBalde)
        {
            const string sql = @"
                INSERT INTO email_balde 
                (
                    usuario_id, 
                    email, 
                    tipo, 
                    data_cadastro 
                )
                VALUES 
                (
                    @UsuarioId, 
                    @Email, 
                    @Tipo, 
                    NOW() 
                )";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sql, emailBalde);
            }
        }
    }
}
