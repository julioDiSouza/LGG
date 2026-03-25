using LGG.Global.Library.Enumeradores;

namespace LGG.Dominio.Library.Entidades
{
    public class Canal
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
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