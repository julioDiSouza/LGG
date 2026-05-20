using LGG.Dominio.Library.Entidades;

namespace LGG.Aplicacao.Library.Interfaces.IVideoCiclo
{
    public interface IVideoCicloService
    {
        Task<CampeonatoMensal?> ObterCampeonatoVigenteAsync();
        Task IniciarCicloAsync(CampeonatoMensal campeonatoMensal);
        Task PopularBaldeAsync();
        Task ProcessarTopDezAsync();
    }
}
