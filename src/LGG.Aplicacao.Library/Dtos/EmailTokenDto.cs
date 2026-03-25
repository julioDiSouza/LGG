namespace LGG.Aplicacao.Library.Dtos
{
    public class EmailTokenDto
    {
        public string UsuarioId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mensagem { get; set; } = string.Empty;

        public EmailTokenDto() { }
        public EmailTokenDto(string usuarioId, string email, string mensagem)
        {
            UsuarioId = usuarioId;
            Email = email;
            Mensagem = mensagem;
        }
    }
}