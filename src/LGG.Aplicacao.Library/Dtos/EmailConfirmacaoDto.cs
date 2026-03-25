namespace LGG.Aplicacao.Library.Dtos
{
    public class EmailConfirmacaoDto
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Frase { get; set; } = string.Empty;

        public EmailConfirmacaoDto(string Id, string email, string frase)
        {
            this.Id = Id;
            Email = email;
            Frase = frase;
        }
    }
}
