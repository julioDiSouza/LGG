using Npgsql;
using Dapper;
using System.Data;
using LGG.Dominio.Library.Entidades;
using LGG.Dominio.Library.Interfaces.IRepositorios.IVideoBalde;

namespace LGG.Infraestrutura.Library.Repositorios.VideoBalde_
{
    public class VideoBaldeRepositorio : IVideoBaldeRepositorio
    {
        private readonly string _connectionString;

        public VideoBaldeRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<long>> ObterVideosIdNoBaldeAsync(long cicloId)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_connectionString))
            {
                const string sql = @"select video_id 
                                     from   videos_balde 
                                     where  video_ciclo_id = @CicloId ";

                var ids = await conexao.QueryAsync<long>( 
                    sql, 
                    new 
                    { 
                        CicloId = cicloId 
                    }, 
                    commandType: CommandType.Text 
                );

                return ids;
            }
        }

        public async Task AdicionarAsync(List<VideoBalde> videosBalde)
        {
            if (videosBalde == null || videosBalde.Count == 0)
                return;

            var values = new List<string>();
            var parameters = new List<NpgsqlParameter>();

            for (int i = 0; i < videosBalde.Count; i++)
            {
                values.Add($"(@video_ciclo_id{i}, @usuario_id{i}, @video_id{i}, @video_id_yt{i}, @data_cadastro{i})");

                parameters.Add(new NpgsqlParameter($"@video_ciclo_id{i}", videosBalde[i].VideoCicloId));
                parameters.Add(new NpgsqlParameter($"@usuario_id{i}", videosBalde[i].UsuarioId));
                parameters.Add(new NpgsqlParameter($"@video_id{i}", videosBalde[i].VideoId));
                parameters.Add(new NpgsqlParameter($"@video_id_yt{i}", videosBalde[i].VideoIdYt));
                parameters.Add(new NpgsqlParameter($"@data_cadastro{i}", videosBalde[i].DataCadastro));
            }

            var valuesString = string.Join(", ", values);
            var sql = $@"INSERT INTO videos_balde 
                        (
                            video_ciclo_id, 
                            usuario_id, 
                            video_id, 
                            video_id_yt, 
                            data_cadastro
                        )
                        VALUES 
                        {valuesString}";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await using var cmd = new NpgsqlCommand(sql, connection);

                cmd.Parameters.AddRange(parameters.ToArray());

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
