using LGG.Global.Library.Enumeradores;

namespace LGG.Dominio.Library.Entidades
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public UsuarioSexoEnum Sexo { get; set; }
        public UsuarioTipoEnum Tipo { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Senha { get; set; } = string.Empty;
        public UsuarioStatusEnum Status { get; set; }
        public DateTime DataStatus { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenDataExpiracao { get; set; }
        public string ResetPasswordToken { get; set; } = string.Empty;
        public DateTime ResetPasswordDataExpiracao { get; set; }
        public string CodigoVideo { get; set; } = string.Empty;

        public Usuario() { }

        public Usuario Cadastrar(string nome, string email, UsuarioSexoEnum sexo, DateTime dataNascimento, 
            UsuarioTipoEnum tipo, UsuarioStatusEnum status, string senha, string codigoVideo)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            Sexo = sexo;
            DataNascimento = dataNascimento;
            Tipo = tipo;
            Status = status;
            Senha = BCrypt.Net.BCrypt.HashPassword(senha);
            CodigoVideo = codigoVideo;

            return this;
        }
    }
}