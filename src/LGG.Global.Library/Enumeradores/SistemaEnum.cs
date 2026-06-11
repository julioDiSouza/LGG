using System.ComponentModel;

namespace LGG.Global.Library.Enumeradores
{
    public enum SistemaEnum
    {
        #region Sucesso

        [Description("Processado com sucesso.")]
        Sucesso = 200,
        [Description("Api funcionando.")]
        ApiFuncionando = 2000,

        #endregion Fim - Sucesso

        #region Validações

        [Description("Email já cadastrado.")]
        EmailJaCadastrado = 4002,
        [Description("Email indisponível para validação.")]
        EmailIndisponivelValidacao = 4003,
        [Description("Parâmetro id é obrigatório.")]
        ParametroIdObrigatorio = 4004,
        [Description("Campeonato mensal já cadastrado para este mês e ano.")]
        CampeonatoMensalJaCadastrado = 4005,
        [Description("Usuário inválido, por favor regularize seu usuário.")]
        UsuarioInvalido = 4006,
        [Description("Canal inválido, por favor regularize seu canal.")]
        CanalInvalido = 4007,
        [Description("Campeonato indisponível, por favor aguarde o início de um novo campeonato.")]
        CampeonatoIndisponivel = 4008,
        [Description("Já existe vídeo publicado, apague o vídeo no campeonato atual para publicar um novo.")]
        VideoJaPublicado = 4009,

        [Description("Não Autorizado.")]
        NaoAutorizado = 401,
        [Description("Não Encontrado.")]
        NaoEncontrado = 404,

        #endregion Fim - Validações

        #region Erros

        [Description("Falha ao solicitar cadastro do canal.")]
        CanalFalhaCadastro = 5001,
        [Description("Falha ao cadastrar usuário.")]
        UsuarioFalhaCadastro = 5002,
        [Description("Falha ao solicitar validação do canal.")]
        CanalFalhaValidacao = 5003,
        [Description("Falha ao solicitar validação do canal.")]
        ErroValidarKey = 5003,
        [Description("Erro interno.")]
        ErroInterno = 500,

        #endregion Fim - Erros
    }
}
