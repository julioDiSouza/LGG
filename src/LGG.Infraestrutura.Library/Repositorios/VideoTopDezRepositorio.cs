using Npgsql;
using Dapper;
using LGG.Dominio.Library.Entidades;
using LGG.Dominio.Library.Interfaces.IRepositorios;

namespace LGG.Infraestrutura.Library.Repositorios
{
    public class VideoTopDezRepositorio : IVideoTopDezRepositorio
    {
        private readonly string _connectionString;

        public VideoTopDezRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Obtém um vídeo top 10 pelo Id
        /// </summary>
        /// <param name="id">Id do vídeo top 10</param>
        /// <returns>Vídeo top 10 encontrado ou null</returns>
        public async Task<VideoTopDez?> ObterPorIdAsync(long id)
        {
            const string sql = @"
                SELECT id, video_id as VideoId, video_ciclo_id as VideoCicloId, usuario_id as UsuarioId,
                       campeonato_mensal_id as CampeonatoMensalId, titulo, url,
                       qtd_view as QtdView, qtd_like as QtdLike, url_imagem as UrlImagem,
                       data_cadastro as DataCadastro
                FROM videos_top_dez
                WHERE id = @Id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<VideoTopDez>(sql, new { Id = id });
            }
        }

        /// <summary>
        /// Obtém todos os vídeos top 10 ordenados pela data de cadastro
        /// </summary>
        /// <returns>Lista de vídeos top 10</returns>
        public async Task<List<VideoTopDez>> ObterTodosAsync()
        {
            const string sql = @"
                SELECT id, video_id as VideoId, video_ciclo_id as VideoCicloId, usuario_id as UsuarioId,
                       campeonato_mensal_id as CampeonatoMensalId, titulo, url,
                       qtd_view as QtdView, qtd_like as QtdLike, url_imagem as UrlImagem,
                       data_cadastro as DataCadastro
                FROM videos_top_dez
                ORDER BY data_cadastro DESC;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var videos = await connection.QueryAsync<VideoTopDez>(sql);
                return videos.ToList();
            }
        }

        /// <summary>
        /// Obtém os vídeos top 10 de um campeonato mensal específico
        /// </summary>
        /// <param name="campeonatoMensalId">Id do campeonato mensal</param>
        /// <returns>Lista de vídeos top 10 do campeonato</returns>
        public async Task<List<VideoTopDez>> ObterPorCampeonatoMensalIdAsync(int campeonatoMensalId)
        {
            const string sql = @"
                SELECT id, video_id as VideoId, video_ciclo_id as VideoCicloId, usuario_id as UsuarioId,
                       campeonato_mensal_id as CampeonatoMensalId, titulo, url,
                       qtd_view as QtdView, qtd_like as QtdLike, url_imagem as UrlImagem,
                       data_cadastro as DataCadastro
                FROM videos_top_dez
                WHERE campeonato_mensal_id = @CampeonatoMensalId
                ORDER BY qtd_like DESC, qtd_view DESC;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var videos = await connection.QueryAsync<VideoTopDez>(sql, new { CampeonatoMensalId = campeonatoMensalId });
                return videos.ToList();
            }
        }

        /// <summary>
        /// Obtém os vídeos top 10 de um usuário específico
        /// </summary>
        /// <param name="usuarioId">Id do usuário</param>
        /// <returns>Lista de vídeos top 10 do usuário</returns>
        public async Task<List<VideoTopDez>> ObterPorUsuarioIdAsync(Guid usuarioId)
        {
            const string sql = @"
                SELECT id, video_id as VideoId, video_ciclo_id as VideoCicloId, usuario_id as UsuarioId,
                       campeonato_mensal_id as CampeonatoMensalId, titulo, url,
                       qtd_view as QtdView, qtd_like as QtdLike, url_imagem as UrlImagem,
                       data_cadastro as DataCadastro
                FROM videos_top_dez
                WHERE usuario_id = @UsuarioId
                ORDER BY qtd_like DESC, qtd_view DESC;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var videos = await connection.QueryAsync<VideoTopDez>(sql, new { UsuarioId = usuarioId });
                return videos.ToList();
            }
        }

        /// <summary>
        /// Adiciona um novo vídeo aos top 10
        /// </summary>
        /// <param name="videoTopDez">Vídeo top 10 a adicionar</param>
        /// <returns>Vídeo top 10 adicionado com Id preenchido</returns>
        public async Task<VideoTopDez> AdicionarAsync(VideoTopDez videoTopDez)
        {
            const string sql = @"
                INSERT INTO videos_top_dez 
                (
                    video_id, 
                    video_ciclo_id, 
                    usuario_id, 
                    campeonato_mensal_id, 
                    titulo, url, 
                    qtd_view, 
                    qtd_like, 
                    url_imagem, 
                    data_cadastro
                )
                VALUES 
                (
                    @VideoId, 
                    @VideoCicloId, 
                    @UsuarioId, 
                    @CampeonatoMensalId, 
                    @Titulo, 
                    @Url, 
                    @QtdView, 
                    @QtdLike, 
                    @UrlImagem, 
                    @DataCadastro
                )
                RETURNING id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var id = await connection.QuerySingleAsync<long>(sql, videoTopDez);
                videoTopDez.Id = id;
                return videoTopDez;
            }
        }

        /// <summary>
        /// Remove um vídeo dos top 10 pelo Id
        /// </summary>
        /// <param name="id">Id do vídeo top 10 a remover</param>
        public async Task RemoverPorIdAsync(long id)
        {
            const string sql = @"DELETE FROM videos_top_dez WHERE id = @Id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

        /// <summary>
        /// Remove todos os vídeos top 10 de um campeonato mensal
        /// </summary>
        /// <param name="campeonatoMensalId">Id do campeonato mensal</param>
        public async Task RemoverPorCampeonatoMensalIdAsync(int campeonatoMensalId)
        {
            const string sql = @"DELETE FROM videos_top_dez WHERE campeonato_mensal_id = @CampeonatoMensalId;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sql, new { CampeonatoMensalId = campeonatoMensalId });
            }
        }

        /// <summary>
        /// Limpa a tabela de vídeos top 10 e insere novos registros
        /// </summary>
        /// <param name="videosTopDez">Lista de vídeos top 10 a inserir</param>
        public async Task ReconstruirTopDezAsync(List<VideoTopDez> videosTopDez)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await using var transaction = await connection.BeginTransactionAsync();

                try
                {
                    // Limpar tabela
                    const string deleteSql = @"TRUNCATE TABLE videos_top_dez;";
                    await connection.ExecuteAsync(deleteSql, transaction: transaction);

                    // Inserir novos registros
                    if (videosTopDez != null && videosTopDez.Count > 0)
                    {
                        const string insertSql = @"
                            INSERT INTO videos_top_dez (video_id, video_ciclo_id, usuario_id, campeonato_mensal_id, titulo, url, qtd_view, qtd_like, url_imagem, data_cadastro)
                            VALUES (@VideoId, @VideoCicloId, @UsuarioId, @CampeonatoMensalId, @Titulo, @Url, @QtdView, @QtdLike, @UrlImagem, @DataCadastro);";

                        await connection.ExecuteAsync(insertSql, videosTopDez, transaction: transaction);
                    }

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
