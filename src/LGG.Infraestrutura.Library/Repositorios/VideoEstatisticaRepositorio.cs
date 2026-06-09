using Dapper;
using Npgsql;
using LGG.Dominio.Library.Entidades;
using LGG.Dominio.Library.Interfaces.IRepositorios;

namespace LGG.Infraestrutura.Library.Repositorios
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
                INSERT INTO video_estatistica (
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

        /// <summary>
        /// Obtém o top 10 de vídeos ordenados por QtdLike e QtdView em ordem decrescente
        /// </summary>
        /// <returns>Lista com até 10 vídeos mais bem avaliados</returns>
        public async Task<List<VideoEstatistica>> ObterTopDezAsync(long videoCicloId)
        {
            const string sql = @"
                SELECT id, 
                       video_ciclo_id as VideoCicloId, 
                       video_id as VideoId, 
                       qtd_view as QtdView, 
                       qtd_like as QtdLike, 
                       data_cadastro as DataCadastro
                FROM video_estatistica
                WHERE video_ciclo_id = @VideoCicloId
                ORDER BY qtd_like DESC, qtd_view DESC
                LIMIT 10;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var videos = await connection.QueryAsync<VideoEstatistica>(
                    sql, 
                    new 
                    { 
                        VideoCicloId = videoCicloId 
                    });

                return videos.ToList();
            }
        }
    }
}
