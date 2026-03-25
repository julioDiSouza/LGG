namespace LGG.Aplicacao.Library.Dtos
{
    public class CanalValidacaoDto
    {
        public string UsuarioId { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;

        public CanalValidacaoDto() { }

        public CanalValidacaoDto(string usuarioId, string nome, string codigo)
        {
            UsuarioId = usuarioId;
            Nome = nome;
        }
    }
}