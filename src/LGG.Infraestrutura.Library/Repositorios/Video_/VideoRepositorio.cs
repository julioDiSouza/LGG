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
                INSERT INTO video (usuario_id, campeonato_mensal_id, video_ciclo_id, titulo, descricao, url, video_id, qtd_view, qtd_like, url_imagem, data_publicacao, data_cadastro, status)
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
                FROM video
                WHERE id = @Id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<Video>(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<Video>> ObterFaltantesPorIdsAsync(IEnumerable<long> ids)
        {
            const string sql = @"
                SELECT id, usuario_id as UsuarioId, campeonato_mensal_id as CampeonatoMensalId, video_ciclo_id as VideoCicloId,
                       titulo, descricao, url, video_id as VideoId, qtd_view as QtdView, 
                       qtd_like as QtdLike, url_imagem as UrlImagem,
                       data_publicacao as DataPublicacao, data_cadastro as DataCadastro, status
                FROM video
                WHERE id <> ALL(@Ids);";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Video>(sql, new { Ids = ids });
            }
        }

        /// <summary>
        /// Obtém um vídeo pelo Id do usuário e Id do campeonato mensal
        /// </summary>
        /// <param name="usuarioId">Id do usuário</param>
        /// <param name="id">Id do campeonato mensal</param>
        /// <returns>Vídeo encontrado ou null</returns>
        public async Task<Video?> ObterPorUsuarioIdCampIdAsync(string usuarioId, int id)
        {
            const string sql = @"
                SELECT id, usuario_id as UsuarioId, campeonato_mensal_id as CampeonatoMensalId, video_ciclo_id as VideoCicloId,
                       titulo, descricao, url, video_id as VideoId, qtd_view as QtdView, 
                       qtd_like as QtdLike, url_imagem as UrlImagem,
                       data_publicacao as DataPublicacao, data_cadastro as DataCadastro, status
                FROM video
                WHERE usuario_id = @UsuarioId 
                  AND campeonato_mensal_id = @CampeonatoMensalId
                LIMIT 1;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<Video>(
                    sql, 
                    new 
                    { 
                        UsuarioId = Guid.Parse(usuarioId), 
                        CampeonatoMensalId = id 
                    });
            }
        }

        public async Task<List<Video>> ObterTodosAsync()
        {
            const string sql = @"
                SELECT id, usuario_id as UsuarioId, campeonato_mensal_id as CampeonatoMensalId, video_ciclo_id as VideoCicloId,
                       titulo, descricao, url, video_id as VideoId, qtd_view as QtdView, 
                       qtd_like as QtdLike, url_imagem as UrlImagem,
                       data_publicacao as DataPublicacao, data_cadastro as DataCadastro, status
                FROM video
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
                UPDATE video
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
                UPDATE video
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
            const string sql = @"DELETE FROM video WHERE id = @Id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}
