using LGG.Global.Library.Enumeradores;

namespace LGG.Aplicacao.Library.Dtos
{
    public class CanalDto
    {
        public string Id { get; set; } = string.Empty;
        public string UsuarioId { get; set; } = string.Empty;
        public string CanalId { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string UrlPersonalizada { get; set; } = string.Empty;
        public string UrlImagem { get; set; } = string.Empty;
        public CanalStatusEnum Status { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
