using Npgsql;
using Dapper;
using System.Data;
using LGG.Dominio.Library.Entidades;
using LGG.Global.Library.Enumeradores;
using LGG.Dominio.Library.Interfaces.IRepositorios.ICanal;

namespace LGG.Infraestrutura.Library.Repositorios.Canal_
{
    public class CanalRepositorio : ICanalRepositorio
    {
        private readonly string _stringConexao;

        public CanalRepositorio(string stringConexao)
        {
            _stringConexao = stringConexao;
        }

        public async Task<Canal> ObterPorIdAsync(Guid canalId)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"SELECT id, 
                                            usuario_id AS UsuarioId, 
                                            canal_id AS CanalId, 
                                            codigo, 
                                            nome, 
                                            url_personalizada AS UrlPersonalizada, 
                                            url_imagem AS UrlImagem, 
                                            status, 
                                            data_cadastro AS DataCadastro, 
                                            titulo 
                                    FROM    canais 
                                    WHERE   id      =  @Id ";

                var canal = await conexao.QueryFirstOrDefaultAsync<Canal>(
                    sql,
                    param: new
                    {
                        Id = canalId,
                        Status = CanalStatusEnum.Excluido
                    },
                    commandType: CommandType.Text
                );

                return canal;
            }
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

        public async Task AtualizarInformacoesAsync(Guid usuarioId, string canalId, string nome, string titulo,
            string urlPersonalizada, string urlImagem)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"UPDATE canais 
                                     SET    nome                = @nome, 
                                            canal_id            = @canalId, 
                                            titulo              = @titulo, 
                                            url_personalizada   = @urlPersonalizada, 
                                            url_imagem          = @urlImagem 
                                     WHERE  usuario_id  = @usuarioId 
                                     AND    status      = @status ";

                await conexao.ExecuteAsync(
                    sql,
                    new
                    {
                        canalId,
                        nome,
                        titulo,
                        urlPersonalizada,
                        urlImagem,
                        usuarioId,
                        status = CanalStatusEnum.Ativo
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
