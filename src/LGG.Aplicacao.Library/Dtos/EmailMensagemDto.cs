namespace LGG.Aplicacao.Library.Dtos
{
    public class EmailMensagemDto
    {
        public EmailMensagemDto(string destino, string assunto, string conteudo)
        {
            Destino = destino;
            Assunto = assunto;
            Conteudo = conteudo;
        }

        public string Destino { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }
    }
}
