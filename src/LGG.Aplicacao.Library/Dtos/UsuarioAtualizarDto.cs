using LGG.Global.Library.Enumeradores;

namespace LGG.Aplicacao.Library.Dtos
{
    public class UsuarioAtualizarDto
    {
        public string Nome { get; set; } = string.Empty;
        public UsuarioSexoEnum Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
