using Dapper;
using LGG.Dominio.Library.Entidades;
using LGG.Dominio.Library.Interfaces.IRepositorios.IVideo;
using Npgsql;
using Microsoft.Extensions.Configuration;

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
                INSERT INTO videos (titulo, descricao, url, video_id, qtd_view, qtd_like, url_imagem, canal_id, data_publicacao, data_criacao, ativo)
                VALUES (@Titulo, @Descricao, @Url, @VideoId, @QtdView, @QtdLike, @UrlImagem, @CanalId, @DataPublicacao, @DataCriacao, @Ativo)
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
                SELECT id, titulo, descricao, url, video_id as VideoId, qtd_view as QtdView, 
                       qtd_like as QtdLike, url_imagem as UrlImagem, canal_id as CanalId, 
                       data_publicacao as DataPublicacao, data_criacao as DataCriacao, ativo
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
                SELECT id, titulo, descricao, url, video_id as VideoId, qtd_view as QtdView, 
                       qtd_like as QtdLike, url_imagem as UrlImagem, canal_id as CanalId, 
                       data_publicacao as DataPublicacao, data_criacao as DataCriacao, ativo
                FROM videos
                WHERE canal_id = @CanalId AND ativo = true
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
                SELECT id, titulo, descricao, url, video_id as VideoId, qtd_view as QtdView, 
                       qtd_like as QtdLike, url_imagem as UrlImagem, canal_id as CanalId, 
                       data_publicacao as DataPublicacao, data_criacao as DataCriacao, ativo
                FROM videos
                WHERE ativo = true
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
                RETURNING id, titulo, descricao, url, video_id as VideoId, qtd_view as QtdView, 
                          qtd_like as QtdLike, url_imagem as UrlImagem, canal_id as CanalId, 
                          data_publicacao as DataPublicacao, data_criacao as DataCriacao, ativo;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<Video>(sql, new { Id = id, Titulo = titulo, Descricao = descricao, Url = url });
            }
        }

        public async Task<Video> ApagarPorIdAsync(long id)
        {
            const string sql = @"
                UPDATE videos
                SET ativo = false
                WHERE id = @Id
                RETURNING id, titulo, descricao, url, video_id as VideoId, qtd_view as QtdView, 
                          qtd_like as QtdLike, url_imagem as UrlImagem, canal_id as CanalId, 
                          data_publicacao as DataPublicacao, data_criacao as DataCriacao, ativo;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<Video>(sql, new { Id = id });
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
