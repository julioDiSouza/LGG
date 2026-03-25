using Npgsql;
using Dapper;
using System.Data;
using LGG.Dominio.Library.Entidades;
using LGG.Dominio.Library.Interfaces.IRepositorios.ICanal;
using LGG.Global.Library.Enumeradores;

namespace LGG.Infraestrutura.Library.Repositorios.Canal_
{
    public class CanalRepositorio : ICanalRepositorio
    {
        private readonly string _stringConexao;

        public CanalRepositorio(string stringConexao)
        {
            _stringConexao = stringConexao;
        }
        
        public async Task<Canal> AdicionarAsync(Canal canal)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"INSERT INTO canais 
                                (
                                    id,
                                    usuario_id, 
                                    canal_id,
                                    codigo,
                                    nome,
                                    url_personalizada,
                                    url_imagem,
                                    status,
                                    data_cadastro 
                                )
                                VALUES 
                                (
                                    @Id,    
                                    @UsuarioId, 
                                    @CanalId,
                                    @Codigo,
                                    @Nome,
                                    @UrlPersonalizada,
                                    @UrlImagem,
                                    @Status,
                                    NOW() 
                                )";

                await conexao.ExecuteAsync(
                    sql,
                    param: canal,
                    commandType: CommandType.Text
                    );

                return canal;
            }
        }

        public async Task<bool> CodigoLiberadoAsync(string codigo)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"SELECT codigo 
                                    FROM    canais 
                                    WHERE   codigo = @codigo";

                var canal = await conexao.QueryFirstOrDefaultAsync<Usuario>(
                    sql,
                    param: new { Codigo = codigo },
                    commandType: CommandType.Text
                );

                return canal is null;
            }
        }

        public async Task AtualizarStatusAsync(Guid usuarioId, CanalStatusEnum status)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"UPDATE canais 
                                     SET    status     = @status 
                                     WHERE  usuario_id = @usuarioId";

                await conexao.ExecuteAsync(
                    sql,
                    new
                    {
                        status,
                        usuarioId
                    },
                    commandType: CommandType.Text
                );
            }
        }

        public async Task RemoverDefinitivoPorIdAsync(Guid id)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"DELETE 
                                     FROM   canais 
                                     WHERE  id = @Id ";

                await conexao.ExecuteAsync(
                    sql,
                    new
                    {
                        Id = id
                    },
                    commandType: CommandType.Text
                );
            }
        }
    }
}
