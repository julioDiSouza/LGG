using LGG.Global.Library.Enumeradores;

namespace LGG.Aplicacao.Library.Dtos
{
    public class UsuarioDto
    {
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UsuarioSexoEnum Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public UsuarioTipoEnum Tipo { get; set; }
        public UsuarioStatusEnum Status { get; set; }
        public bool Ativo { get; set; }
    }
}
