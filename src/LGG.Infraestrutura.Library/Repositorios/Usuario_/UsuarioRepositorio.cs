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
                const string sql = @"INSERT INTO usuarios 
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
                                    senha 
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
                                    @Senha 
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
                c.id,
                c.usuario_id AS UsuarioId,
                c.canal_id AS CanalId,
                c.codigo,
                c.nome,
                c.url_personalizada AS UrlPersonalizada,
                c.url_imagem AS UrlImagem,
                c.status,
                c.data_cadastro AS DataCadastro, 
                c.titulo 
            FROM 
                usuarios u 
            LEFT JOIN 
                canais c ON c.usuario_id = u.id 
            WHERE 
                u.id = @Id ";

                var usuario = await conexao.QueryAsync<Usuario, Canal, Usuario>(
                    sql,
                    (u, c) =>
                    {
                        u.Canal = c;
                        return u;
                    },
                    param: new
                    {
                        Id = id,
                        StatusUsuario = UsuarioStatusEnum.Excluido,
                        StatusCanal = CanalStatusEnum.Excluido
                    },
                    splitOn: "Id"
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
                c.id,
                c.usuario_id AS UsuarioId,
                c.canal_id AS CanalId,
                c.codigo,
                c.nome,
                c.url_personalizada AS UrlPersonalizada,
                c.url_imagem AS UrlImagem,
                c.status,
                c.data_cadastro AS DataCadastro, 
                c.titulo 
            FROM 
                usuarios u 
            LEFT JOIN 
                canais c ON c.usuario_id = u.id 
            WHERE 
                u.email = @Email ";

                var usuario = await conexao.QueryAsync<Usuario, Canal, Usuario>(
                    sql,
                    (u, c) =>
                    {
                        u.Canal = c;
                        return u;
                    },
                    param: new
                    {
                        Email = email
                    },
                    splitOn: "Id"
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
                c.id,
                c.usuario_id AS UsuarioId,
                c.canal_id AS CanalId,
                c.codigo,
                c.nome,
                c.url_personalizada AS UrlPersonalizada,
                c.url_imagem AS UrlImagem,
                c.status,
                c.data_cadastro AS DataCadastro, 
                c.titulo 
            FROM 
                usuarios u 
            LEFT JOIN 
                canais c ON c.usuario_id = u.id 
            WHERE 
                u.email = @Email ";

                var usuario = await conexao.QueryAsync<Usuario, Canal, Usuario>(
                    sql,
                    (u, c) =>
                    {
                        u.Canal = c;
                        return u;
                    },
                    param: new
                    {
                        Email = email
                    },
                    splitOn: "Id"
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
                c.id,
                c.usuario_id AS UsuarioId,
                c.canal_id AS CanalId,
                c.codigo,
                c.nome,
                c.url_personalizada AS UrlPersonalizada,
                c.url_imagem AS UrlImagem,
                c.status,
                c.data_cadastro AS DataCadastro, 
                c.titulo 
            FROM 
                usuarios u 
            LEFT JOIN 
                canais c ON c.usuario_id = u.id 
            WHERE 
                u.status <> @Status ";

                var usuariosDictionary = new Dictionary<Guid, Usuario>();

                var usuarios = await conexao.QueryAsync<Usuario, Canal, Usuario>(
                    sql,
                    (u, c) =>
                    {
                        if (!usuariosDictionary.ContainsKey(u.Id))
                        {
                            usuariosDictionary[u.Id] = u;
                        }
                        else
                        {
                            u = usuariosDictionary[u.Id];
                        }

                        if (c != null)
                        {
                            u.Canal = c;
                        }

                        return u;
                    },
                    param: new
                    {
                        Status = UsuarioStatusEnum.Excluido
                    },
                    splitOn: "Id"
                );

                return usuariosDictionary.Values.ToList();
            }
        }

        public async Task<Usuario> AtualizarAsync(Guid id, string nome, UsuarioSexoEnum sexo, DateTime dataNascimento)
        {
            using (IDbConnection conexao = new NpgsqlConnection(_stringConexao))
            {
                const string sql = @"UPDATE usuarios 
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
                                            status ";

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
                const string sql = @"UPDATE usuarios 
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
                                            status ";

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
                const string sql = @"UPDATE usuarios 
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
                                            status ";

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
                                     FROM   usuarios 
                                     WHERE  id = @Id ";

                await conexao.ExecuteAsync(
                    sql,
                    new
                    {
                        Id = id
                    },
                    commandType: CommandType.Text
                );

                const string sql2 = @"DELETE 
                                     FROM   canais 
                                     WHERE  usuario_id = @Id ";

                await conexao.ExecuteAsync(
                    sql2,
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
