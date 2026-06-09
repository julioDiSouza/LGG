using LGG.Dominio.Library.Entidades;

namespace LGG.Aplicacao.Library.Interfaces.IServices
{
    public interface IVideoCicloService
    {
        Task<CampeonatoMensal?> ObterCampeonatoVigenteAsync();
        Task IniciarCicloAsync(CampeonatoMensal campeonatoMensal);
        Task PopularBaldeAsync();
        Task FinalizarMetricasAsync();
        Task ProcessarTopDezAsync();
    }
}
