namespace LGG.Aplicacao.Library.Dtos
{
    public class UsuarioTokenRespostaDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string TokenType { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public int ExpiresIn { get; set; }
    }
}
