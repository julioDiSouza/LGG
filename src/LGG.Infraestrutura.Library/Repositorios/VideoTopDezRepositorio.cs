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
        /// Obtém os vídeos top 10
        /// </summary>
        /// <returns></returns>
        public async Task<List<VideoTopDez>> ObterAsync()
        {
            const string sql = @"
                SELECT  id, 
                        video_id as VideoId, 
                        video_ciclo_id as VideoCicloId, 
                        usuario_id as UsuarioId,
                        usuario_nome as UsuarioNome,
                        campeonato_mensal_id as CampeonatoMensalId, 
                        titulo, 
                        url,
                        qtd_view as QtdView, 
                        qtd_like as QtdLike, 
                        url_imagem as UrlImagem,
                        data_cadastro as DataCadastro 
                FROM    video_top_dez 
                WHERE   (campeonato_mensal_id, video_ciclo_id) = (
                        SELECT  campeonato_mensal_id, video_ciclo_id
                        FROM    dbggclt.video_top_dez
                        ORDER   BY campeonato_mensal_id DESC, video_ciclo_id DESC
                        LIMIT 1
                        );";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var videos = await connection.QueryAsync<VideoTopDez>(sql);

                return videos.ToList();
            }
        }

        /// <summary>
        /// Adiciona um novo vídeo aos top 10
        /// </summary>
        /// <param name="videoTopDez">Vídeo top 10 a adicionar</param>
        /// <returns>Vídeo top 10 adicionado com Id preenchido</returns>
        public async Task AdicionarAsync(VideoTopDez videoTopDez)
        {
            const string sql = @"
                INSERT INTO video_top_dez 
                (
                    video_id, 
                    video_ciclo_id, 
                    usuario_id, 
                    usuario_nome,
                    campeonato_mensal_id, 
                    titulo, 
                    url, 
                    qtd_view, 
                    qtd_like, 
                    url_imagem
                )
                VALUES 
                (
                    @VideoId, 
                    @VideoCicloId, 
                    @UsuarioId, 
                    @UsuarioNome,
                    @CampeonatoMensalId, 
                    @Titulo, 
                    @Url, 
                    @QtdView, 
                    @QtdLike, 
                    @UrlImagem
                )";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sql, videoTopDez);
            }
        }

        /// <summary>
        /// Remove um vídeo dos top 10 pelo Id
        /// </summary>
        /// <param name="id">Id do vídeo top 10 a remover</param>
        public async Task RemoverPorIdAsync(long id)
        {
            const string sql = @"DELETE FROM video_top_dez WHERE id = @Id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
        
    }
}
