using LGG.Global.Library.Enumeradores;

namespace LGG.Aplicacao.Library.Dtos
{
    public class UsuarioCadastroDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UsuarioSexoEnum Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Senha { get; set; } = string.Empty;
        public string Resenha { get; set; } = string.Empty;
    }
}
