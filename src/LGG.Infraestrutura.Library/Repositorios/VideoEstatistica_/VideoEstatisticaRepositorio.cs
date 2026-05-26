using Dapper;
using Npgsql;
using LGG.Dominio.Library.Entidades;
using LGG.Dominio.Library.Interfaces.IRepositorios.IVideoEstatistica;

namespace LGG.Infraestrutura.Library.Repositorios.VideoEstatistica_
{
    public class VideoEstatisticaRepositorio : IVideoEstatisticaRepositorio
    {
        private readonly string _connectionString;

        public VideoEstatisticaRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<VideoEstatistica> AdicionarAsync(VideoEstatistica video)
        {
            const string sql = @"
                INSERT INTO video_estatisticas (
                    video_ciclo_id, 
                    video_id, 
                    qtd_view, 
                    qtd_like, 
                    data_cadastro
                )
                VALUES (
                    @VideoCicloId, 
                    @VideoId, 
                    @QtdView, 
                    @QtdLike, 
                    @DataCadastro
                )
                RETURNING id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var id = await connection.QuerySingleAsync<long>(sql, video);
                video.Id = id;
                return video;
            }
        }
    }
}
