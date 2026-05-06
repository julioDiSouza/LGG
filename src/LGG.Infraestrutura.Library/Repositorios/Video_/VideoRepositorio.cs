using Dapper;
using Npgsql;
using LGG.Dominio.Library.Entidades;
using LGG.Global.Library.Enumeradores;
using LGG.Dominio.Library.Interfaces.IRepositorios.IVideo;

namespace LGG.Infraestrutura.Library.Repositorios.Video_
{
    public class VideoRepositorio : IVideoRepositorio
    {
        private readonly string _connectionString;

        public VideoRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Video> AdicionarAsync(Video video)
        {
            const string sql = @"
                INSERT INTO videos (usuario_id, campeonato_mensal_id, video_ciclo_id, titulo, descricao, url, video_id, qtd_view, qtd_like, url_imagem, data_publicacao, data_cadastro, status)
                VALUES (@UsuarioId, @CampeonatoMensalId, @VideoCicloId, @Titulo, @Descricao, @Url, @VideoId, @QtdView, @QtdLike, @UrlImagem, @DataPublicacao, @DataCadastro, @Status)
                RETURNING id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var id = await connection.QuerySingleAsync<long>(sql, video);
                video.Id = id;
                return video;
            }
        }

        public async Task<Video> ObterPorIdAsync(long id)
        {
            const string sql = @"
                SELECT id, usuario_id as UsuarioId, campeonato_mensal_id as CampeonatoMensalId, video_ciclo_id as VideoCicloId,
                       titulo, descricao, url, video_id as VideoId, qtd_view as QtdView, 
                       qtd_like as QtdLike, url_imagem as UrlImagem,
                       data_publicacao as DataPublicacao, data_cadastro as DataCadastro, status
                FROM videos
                WHERE id = @Id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<Video>(sql, new { Id = id });
            }
        }

        public async Task<List<Video>> ObterPorCanalIdAsync(Guid canalId)
        {
            const string sql = @"
                SELECT id, usuario_id as UsuarioId, campeonato_mensal_id as CampeonatoMensalId, video_ciclo_id as VideoCicloId,
                       titulo, descricao, url, video_id as VideoId, qtd_view as QtdView, 
                       qtd_like as QtdLike, url_imagem as UrlImagem,
                       data_publicacao as DataPublicacao, data_cadastro as DataCadastro, status
                FROM videos
                WHERE usuario_id = @CanalId
                ORDER BY data_publicacao DESC;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var videos = await connection.QueryAsync<Video>(sql, new { CanalId = canalId });
                return videos.ToList();
            }
        }

        public async Task<List<Video>> ObterTodosAsync()
        {
            const string sql = @"
                SELECT id, usuario_id as UsuarioId, campeonato_mensal_id as CampeonatoMensalId, video_ciclo_id as VideoCicloId,
                       titulo, descricao, url, video_id as VideoId, qtd_view as QtdView, 
                       qtd_like as QtdLike, url_imagem as UrlImagem,
                       data_publicacao as DataPublicacao, data_cadastro as DataCadastro, status
                FROM videos
                ORDER BY data_publicacao DESC;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var videos = await connection.QueryAsync<Video>(sql);
                return videos.ToList();
            }
        }

        public async Task<Video> AtualizarAsync(long id, string titulo, string descricao, string url)
        {
            const string sql = @"
                UPDATE videos
                SET titulo = @Titulo, descricao = @Descricao, url = @Url
                WHERE id = @Id
                RETURNING id, usuario_id as UsuarioId, campeonato_mensal_id as CampeonatoMensalId, video_ciclo_id as VideoCicloId,
                          titulo, descricao, url, video_id as VideoId, qtd_view as QtdView, 
                          qtd_like as QtdLike, url_imagem as UrlImagem,
                          data_publicacao as DataPublicacao, data_cadastro as DataCadastro, status;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<Video>(sql, new { Id = id, Titulo = titulo, Descricao = descricao, Url = url });
            }
        }

        public async Task<Video> ApagarPorIdAsync(long id)
        {
            const string sql = @"
                UPDATE videos
                SET status = @Status
                WHERE id = @Id
                RETURNING id, usuario_id as UsuarioId, campeonato_mensal_id as CampeonatoMensalId, video_ciclo_id as VideoCicloId,
                          titulo, descricao, url, video_id as VideoId, qtd_view as QtdView, 
                          qtd_like as QtdLike, url_imagem as UrlImagem,
                          data_publicacao as DataPublicacao, data_cadastro as DataCadastro, status;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<Video>(sql, new { Id = id, Status = VideoStatusEnum.Excluido });
            }
        }

        public async Task RemoverDefinitivoPorIdAsync(long id)
        {
            const string sql = @"DELETE FROM videos WHERE id = @Id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}
