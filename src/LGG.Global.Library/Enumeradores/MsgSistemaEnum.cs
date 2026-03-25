using System.ComponentModel;

namespace LGG.Global.Library.Enumeradores
{
    public enum MsgSistemaEnum
    {
        #region Sucesso

        [Description("Api funcionando.")]
        ApiFuncionando = 2001,
        [Description("Solicitação enviada com sucesso.")]
        SolicitacaoEnviadaSucesso = 2200,

        #endregion Fim - Sucesso

        #region Validações

        [Description("Email já cadastrado.")]
        EmailJaCadastrado = 4002,
        [Description("Email indisponível para validação.")]
        EmailIndisponivelValidacao = 4003,
        [Description("Parâmetro id é obrigatório.")]
        ParametroIdObrigatorio = 4004,
        [Description("Não Autorizado.")]
        NaoAutorizado = 4401,
        [Description("Não Encontrado.")]
        NaoEncontrado = 4404,

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
        ErroInterno = 5500,

        #endregion Fim - Erros
    }
}
