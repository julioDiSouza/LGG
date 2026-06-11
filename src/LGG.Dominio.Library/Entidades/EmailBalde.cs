using LGG.Global.Library.Enumeradores;

namespace LGG.Dominio.Library.Entidades
{
    public class EmailBalde
    {
        public long Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string Email { get; set; } = string.Empty;
        public EmailEnum Tipo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataInicioProcessamento { get; set; }
    }
}
