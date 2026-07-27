using Npgsql;
using Dapper;
using System.Data;
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

        public async Task<IEnumerable<EmailBalde>> ObterEmailsBaldeAsync(int qtd)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_connectionString))
            {
                const string sql = @"
                            WITH registros AS (
                                SELECT id
                                FROM email_balde
                                WHERE data_inicio_processamento IS NULL
                                ORDER BY data_cadastro DESC
                                LIMIT @Quantidade
                            )
                            UPDATE email_balde eb
                            SET data_inicio_processamento = NOW()
                            FROM registros r
                            WHERE eb.id = r.id
                            RETURNING
                                eb.id,
                                eb.usuario_id AS UsuarioId,
                                eb.email,
                                eb.tipo,
                                eb.data_cadastro AS DataCadastro,
                                eb.data_inicio_processamento AS DataInicioProcessamento;
                            ";

                var emailsBalde = await conexao.QueryAsync<EmailBalde>(
                    sql,
                    new
                    {
                        Quantidade = qtd
                    },
                    commandType: CommandType.Text
                );

                return emailsBalde;
            }
        }
    }
}
