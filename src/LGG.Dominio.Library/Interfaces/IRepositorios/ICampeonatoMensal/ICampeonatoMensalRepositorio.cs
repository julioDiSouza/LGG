using LGG.Dominio.Library.Entidades;
using LGG.Global.Library.Enumeradores;

namespace LGG.Dominio.Library.Interfaces.IRepositorios.ICampeonatoMensal
{
    public interface ICampeonatoMensalRepositorio
    {
        Task<CampeonatoMensal?> ObterPorIdAsync(int id);
        Task<CampeonatoMensal?> ObterVigenteAsync();
        Task<List<CampeonatoMensal>> ObterTodosAsync();
        Task<List<CampeonatoMensal>> ObterPorAnoAsync(int ano);
        Task<CampeonatoMensal?> ObterPorMesEAnoAsync(MesEnum mes, int ano);
        Task<CampeonatoMensal> AdicionarAsync(CampeonatoMensal campeonatoMensal);
        Task AtualizarAsync(CampeonatoMensal campeonatoMensal);
        Task AtualizarStatusAsync(int id, CampeonatoMensalStatusEnum status);
        Task RemoverAsync(int id);
    }
}
