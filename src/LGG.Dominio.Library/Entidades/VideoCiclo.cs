using LGG.Global.Library.Enumeradores;

namespace LGG.Dominio.Library.Entidades
{
    public class VideoCiclo
    {
        public long Id { get; set; }
        public int CampeonatoMensalId { get; set; }
        public long CicloId { get; set; }
        public long VideoIdInicial { get; set; }
        public long VideoIdFinal { get; set; }
        public VideoCicloStatusEnum Status { get; set; }
        public DateTime DataInicioProcessamento { get; set; }
        public DateTime DataInicioFim { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
