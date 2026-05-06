using LGG.Global.Library.Enumeradores;

namespace LGG.Aplicacao.Library.Dtos
{
    public class CampeonatoMensalCadastroDto
    {
        public MesEnum Mes { get; set; }
        public int Ano { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
