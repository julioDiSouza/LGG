using LGG.Global.Library.Enumeradores;

namespace LGG.Dominio.Library.Entidades
{
    public class CampeonatoMensal
    {
        public int Id { get; set; }
        public MesEnum Mes { get; set; }
        public int Ano { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public CampeonatoMensalStatusEnum Status { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
