using LGG.Global.Library.Enumeradores;

namespace LGG.Dominio.Library.Entidades
{
    public class VideoCiclo
    {
        public long Id { get; set; }
        public int CampeonatoMensalId { get; set; }
        public DateTime DataCadastro { get; set; }
        public VideoCicloStatusEnum Status { get; set; }
        public DateTime DataStatus { get; set; }
    }
}
