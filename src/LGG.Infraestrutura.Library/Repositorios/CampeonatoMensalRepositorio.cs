using Npgsql;
using Dapper;
using System.Data;
using LGG.Dominio.Library.Entidades;
using LGG.Global.Library.Enumeradores;
using LGG.Dominio.Library.Interfaces.IRepositorios;

namespace LGG.Infraestrutura.Library.Repositorios
{
    public class CampeonatoMensalRepositorio : ICampeonatoMensalRepositorio
    {
        private readonly string _stringConexao;

        public CampeonatoMensalRepositorio(string stringConexao)
        {
            _stringConexao = stringConexao;
        }

        public async Task<CampeonatoMensal?> ObterPorIdAsync(int id)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"SELECT id,
                                            mes,
                                            ano,
                                            data_inicio AS DataInicio,
                                            data_fim AS DataFim,
                                            status,
                                            data_cadastro AS DataCadastro
                                    FROM    campeonato_mensal
                                    WHERE   id = @Id";

                var campeonatoMensal = await conexao.QueryFirstOrDefaultAsync<CampeonatoMensal>(
                    sql,
                    param: new { Id = id },
                    commandType: CommandType.Text
                );

                return campeonatoMensal;
            }
        }

        public async Task<CampeonatoMensal?> ObterVigenteAsync()
        {
            var dataAtual = DateTime.Now;
            var ano = dataAtual.Year;
            var mes = (MesEnum)dataAtual.Month;

            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"select id, 
                                            mes, 
                                            ano, 
                                            data_inicio AS DataInicio, 
                                            data_fim AS DataFim, 
                                            status, 
                                            data_cadastro AS DataCadastro 
                                     from campeonato_mensal 
                                     where ano = @Ano 
                                     and   mes = @Mes 
                                     and   status = ANY(@Status) 
                                     limit 1;";

                var campeonatoMensal = await conexao.QueryFirstOrDefaultAsync<CampeonatoMensal>(
                    sql,
                    param: new
                    {
                        Ano = ano,
                        Mes = mes,
                        Status = new[] { (int)CampeonatoMensalStatusEnum.Aberto, (int)CampeonatoMensalStatusEnum.Fechado }
                    },
                    commandType: CommandType.Text
                );

                return campeonatoMensal;
            }
        }

        public async Task<List<CampeonatoMensal>> ObterTodosAsync()
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"SELECT id,
                                            mes,
                                            ano,
                                            data_inicio AS DataInicio,
                                            data_fim AS DataFim,
                                            status,
                                            data_cadastro AS DataCadastro
                                    FROM    campeonato_mensal
                                    ORDER BY ano DESC, mes DESC";

                var campeonatos = await conexao.QueryAsync<CampeonatoMensal>(
                    sql,
                    commandType: CommandType.Text
                );

                return campeonatos.ToList();
            }
        }

        public async Task<List<CampeonatoMensal>> ObterPorAnoAsync(int ano)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"SELECT id,
                                            mes,
                                            ano,
                                            data_inicio AS DataInicio,
                                            data_fim AS DataFim,
                                            status,
                                            data_cadastro AS DataCadastro
                                    FROM    campeonato_mensal
                                    WHERE   ano = @Ano
                                    ORDER BY mes DESC";

                var campeonatos = await conexao.QueryAsync<CampeonatoMensal>(
                    sql,
                    param: new { Ano = ano },
                    commandType: CommandType.Text
                );

                return campeonatos.ToList();
            }
        }

        public async Task<CampeonatoMensal?> ObterPorMesEAnoAsync(MesEnum mes, int ano)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"SELECT id,
                                            mes,
                                            ano,
                                            data_inicio AS DataInicio,
                                            data_fim AS DataFim,
                                            status,
                                            data_cadastro AS DataCadastro
                                    FROM    campeonato_mensal
                                    WHERE   mes = @Mes
                                    AND     ano = @Ano";

                var campeonatoMensal = await conexao.QueryFirstOrDefaultAsync<CampeonatoMensal>(
                    sql,
                    param: new { Mes = mes, Ano = ano },
                    commandType: CommandType.Text
                );

                return campeonatoMensal;
            }
        }

        public async Task<CampeonatoMensal> AdicionarAsync(CampeonatoMensal campeonatoMensal)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"INSERT INTO campeonato_mensal
                                (
                                    id,
                                    mes,
                                    ano,
                                    data_inicio,
                                    data_fim,
                                    status,
                                    data_cadastro
                                )
                                VALUES
                                (
                                    @Id,
                                    @Mes,
                                    @Ano,
                                    @DataInicio,
                                    @DataFim,
                                    @Status,
                                    NOW()
                                )";

                await conexao.ExecuteAsync(
                    sql,
                    param: campeonatoMensal,
                    commandType: CommandType.Text
                );

                return campeonatoMensal;
            }
        }

        public async Task AtualizarAsync(CampeonatoMensal campeonatoMensal)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"UPDATE campeonato_mensal
                                     SET    mes = @Mes,
                                            ano = @Ano,
                                            data_inicio = @DataInicio,
                                            data_fim = @DataFim,
                                            status = @Status
                                     WHERE  id = @Id";

                await conexao.ExecuteAsync(
                    sql,
                    param: campeonatoMensal,
                    commandType: CommandType.Text
                );
            }
        }

        public async Task AtualizarStatusAsync(int id, CampeonatoMensalStatusEnum status)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"UPDATE campeonato_mensal
                                     SET    status = @Status
                                     WHERE  id = @Id";

                await conexao.ExecuteAsync(
                    sql,
                    new
                    {
                        Id = id,
                        Status = status
                    },
                    commandType: CommandType.Text
                );
            }
        }

        public async Task RemoverAsync(int id)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"DELETE
                                     FROM   campeonato_mensal
                                     WHERE  id = @Id";

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
