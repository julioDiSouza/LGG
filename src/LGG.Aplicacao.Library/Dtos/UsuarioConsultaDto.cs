using LGG.Global.Library.Enumeradores;

namespace LGG.Aplicacao.Library.Dtos
{
    public class UsuarioConsultaDto
    {
        public string Id { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UsuarioSexoEnum Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public UsuarioTipoEnum Tipo { get; set; }
        public UsuarioStatusEnum Status { get; set; }

        public CanalDto Canal { get; set; }
    }
}
