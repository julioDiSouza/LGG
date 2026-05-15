using Dapper;
using Npgsql;
using System.Data;
using LGG.Dominio.Library.Entidades;
using LGG.Dominio.Library.Interfaces.IRepositorios.IVideoCiclo;

namespace LGG.Infraestrutura.Library.Repositorios.VideoCiclo_
{
    public class VideoCicloRepositorio : IVideoCicloRepositorio
    {
        private readonly string _connectionString;

        public VideoCicloRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<VideoCiclo?> ObterUltimoVideoCicloAsync()
        {
            using (IDbConnection conexao = new NpgsqlConnection(_connectionString))
            {
                const string sql = @"select id, 
                                            campeonato_mensal_id AS CampeonatoMensalId, 
                                            data_cadastro AS DataCadastro, 
                                            status, 
                                            data_status AS DataStatus 
                                     from   video_ciclos 
                                     order  by id desc 
                                     limit  1; ";

                var videoCiclo = await conexao.QueryFirstOrDefaultAsync<VideoCiclo>(
                    sql,
                    commandType: CommandType.Text
                );

                return videoCiclo;
            }
        }

        public async Task AdicionarAsync(VideoCiclo videoCiclo)
        {
            const string sql = @"INSERT INTO video_ciclos 
                                (
                                    campeonato_mensal_id, 
                                    data_cadastro, 
                                    status, 
                                    data_status 
                                )
                                VALUES 
                                (
                                    @CampeonatoMensalId, 
                                    @DataCadastro, 
                                    @Status, 
                                    @DataStatus 
                                )";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sql, videoCiclo);
            }
        }
    }
}