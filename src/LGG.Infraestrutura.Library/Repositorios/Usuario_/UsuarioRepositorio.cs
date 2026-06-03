using Npgsql;
using Dapper;
using System.Data;
using LGG.Dominio.Library.Entidades;
using LGG.Global.Library.Enumeradores;
using LGG.Dominio.Library.Interfaces.IRepositorios.IUsuario;

namespace LGG.Infraestrutura.Library.Repositorios.Usuario_
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly string _stringConexao;

        public UsuarioRepositorio(string stringConexao)
        {
            _stringConexao = stringConexao;
        }

        public async Task<Usuario> AdicionarAsync(Usuario usuario)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"INSERT INTO usuario 
                                (
                                    id,
                                    nome, 
                                    email,
                                    sexo,
                                    data_nascimento,
                                    tipo,
                                    status,
                                    data_status, 
                                    data_cadastro,
                                    senha, 
                                    codigo_video 
                                )
                                VALUES 
                                (
                                    @Id,    
                                    @Nome, 
                                    @Email,
                                    @Sexo,
                                    @DataNascimento,
                                    @Tipo,
                                    @Status,
                                    NOW(),
                                    NOW(), 
                                    @Senha, 
                                    @CodigoVideo 
                                )";

                await conexao.ExecuteAsync(
                    sql,
                    param: usuario,
                    commandType: CommandType.Text
                    );

                return usuario;
            }
        }

        public async Task<Usuario> ObterPorIdAsync(Guid id)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"
            SELECT  
                u.id,
                u.nome,
                u.email,
                u.data_nascimento AS DataNascimento,
                u.sexo,
                u.tipo,
                u.data_cadastro AS DataCadastro,
                u.status,
                u.data_status AS DataStatus,
                u.refresh_token AS RefreshToken,
                u.refresh_token_data_expiracao AS RefreshTokenDataExpiracao,
                u.reset_password_token AS ResetPasswordToken,
                u.reset_password_data_expiracao AS ResetPasswordDataExpiracao,
                u.codigo_video AS CodigoVideo
            FROM 
                usuario u 
            WHERE 
                u.id = @Id ";

                var usuario = await conexao.QueryAsync<Usuario>(
                    sql,
                    param: new
                    {
                        Id = id
                    },
                    commandType: CommandType.Text
                );

                return usuario.FirstOrDefault()!;
            }
        }

        public async Task<Usuario?> ObterPorEmailAsync(string email)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"
            SELECT  
                u.id,
                u.nome,
                u.email,
                u.data_nascimento AS DataNascimento,
                u.sexo,
                u.tipo,
                u.data_cadastro AS DataCadastro,
                u.status,
                u.data_status AS DataStatus,
                u.refresh_token AS RefreshToken,
                u.refresh_token_data_expiracao AS RefreshTokenDataExpiracao,
                u.reset_password_token AS ResetPasswordToken,
                u.reset_password_data_expiracao AS ResetPasswordDataExpiracao,
                u.codigo_video AS CodigoVideo
            FROM 
                usuario u 
            WHERE 
                u.email = @Email ";

                var usuario = await conexao.QueryAsync<Usuario>(
                    sql,
                    param: new
                    {
                        Email = email
                    },
                    commandType: CommandType.Text
                );

                return usuario.FirstOrDefault();
            }
        }

        public async Task<Usuario?> ObterPorEmailSenhaAsync(string email, string senha)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"
            SELECT  
                u.id,
                u.nome,
                u.email,
                u.data_nascimento AS DataNascimento,
                u.sexo,
                u.tipo,
                u.data_cadastro AS DataCadastro,
                u.status,
                u.data_status AS DataStatus,
                u.refresh_token AS RefreshToken,
                u.refresh_token_data_expiracao AS RefreshTokenDataExpiracao,
                u.reset_password_token AS ResetPasswordToken,
                u.reset_password_data_expiracao AS ResetPasswordDataExpiracao,
                u.senha,
                u.codigo_video AS CodigoVideo
            FROM 
                usuario u 
            WHERE 
                u.email = @Email ";

                var usuario = await conexao.QueryAsync<Usuario>(
                    sql,
                    param: new
                    {
                        Email = email
                    },
                    commandType: CommandType.Text
                );

                return usuario.FirstOrDefault();
            }
        }

        public async Task<List<Usuario>> ObterTodosAsync()
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"
            SELECT  
                u.id,
                u.nome,
                u.email,
                u.data_nascimento AS DataNascimento,
                u.sexo,
                u.tipo,
                u.data_cadastro AS DataCadastro,
                u.status,
                u.data_status AS DataStatus,
                u.refresh_token AS RefreshToken,
                u.refresh_token_data_expiracao AS RefreshTokenDataExpiracao,
                u.reset_password_token AS ResetPasswordToken,
                u.reset_password_data_expiracao AS ResetPasswordDataExpiracao,
                u.codigo_video AS CodigoVideo
            FROM 
                usuario u 
            WHERE 
                u.status <> @Status ";

                var usuarios = await conexao.QueryAsync<Usuario>(
                    sql,
                    param: new
                    {
                        Status = UsuarioStatusEnum.Excluido
                    },
                    commandType: CommandType.Text
                );

                return usuarios.ToList();
            }
        }

        public async Task<bool> CodigoLiberadoAsync(string codigo)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"SELECT codigo_video 
                                    FROM    usuario 
                                    WHERE   codigo_video = @codigo";

                var usuario = await conexao.QueryFirstOrDefaultAsync<Usuario>(
                    sql,
                    param: new { Codigo = codigo },
                    commandType: CommandType.Text
                );

                return usuario is null;
            }
        }

        public async Task<Usuario> AtualizarAsync(Guid id, string nome, UsuarioSexoEnum sexo, DateTime dataNascimento)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"UPDATE usuario 
                                     SET    nome  = @Nome, 
                                            sexo  = @Sexo, 
                                            data_nascimento = @DataNascimento 
                                     WHERE  id    = @Id 
                                     AND    status <> ALL(@Status) 
                                     RETURNING 
                                            id,
                                            nome, 
                                            email,
                                            sexo,
                                            data_nascimento as DataNascimento,
                                            tipo,
                                            status,
                                            codigo_video as CodigoVideo ";

                var usuario = await conexao.QueryFirstOrDefaultAsync<Usuario>(
                    sql,
                    new
                    {
                        Id = id,
                        Nome = nome,
                        Sexo = sexo,
                        DataNascimento = dataNascimento,
                        Status = new int[] { (int)UsuarioStatusEnum.Excluido, (int)UsuarioStatusEnum.Bloqueado } 
                    },
                    commandType: CommandType.Text
                );

                return usuario;
            }
        }

        public async Task<Usuario> AtualizarStatusAsync(Guid id, UsuarioStatusEnum status)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"UPDATE usuario 
                                     SET    status      = @status, 
                                            data_status = NOW() 
                                     WHERE  id    = @Id 
                                     RETURNING 
                                            id,
                                            nome, 
                                            email,
                                            sexo,
                                            data_nascimento as DataNascimento,
                                            tipo,
                                            status,
                                            codigo_video as CodigoVideo ";

                var usuario = await conexao.QueryFirstOrDefaultAsync<Usuario>(
                    sql,
                    new
                    {
                        Id = id, 
                        Status = status 
                    },
                    commandType: CommandType.Text
                );

                return usuario;
            }
        }

        public async Task<Usuario> ApagarPorIdAsync(Guid id)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const bool ativo = false;
                const string sql = @"UPDATE usuario 
                                     SET    status   = @Status, 
                                            data_status = NOW() 
                                     WHERE  id      = @Id 
                                     RETURNING 
                                            id,
                                            nome,
                                            email,
                                            sexo,
                                            data_nascimento as DataNascimento,
                                            tipo,
                                            status,
                                            codigo_video as CodigoVideo ";

                var usuario = await conexao.QueryFirstOrDefaultAsync<Usuario>(
                    sql,
                    new
                    {
                        Id = id,
                        Status = UsuarioStatusEnum.Excluido
                    },
                    commandType: CommandType.Text
                );

                return usuario;
            }
        }

        public async Task RemoverDefinitivoPorIdAsync(Guid id)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"DELETE 
                                     FROM   usuario 
                                     WHERE  id = @Id ";

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
