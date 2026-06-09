using LGG.Dominio.Library.Interfaces.IRepositorios;

namespace LGG.Dominio.Library.Interfaces.IDatabase
{
    public interface IUnidTrabalho
    {
        IUsuarioRepositorio UsuarioRepositorio { get; }
        ICampeonatoMensalRepositorio CampeonatoMensalRepositorio { get; }
        IVideoRepositorio VideoRepositorio { get; }
        IVideoCicloRepositorio VideoCicloRepositorio { get; }
        IVideoBaldeRepositorio VideoBaldeRepositorio { get; }
        IVideoEstatisticaRepositorio VideoEstatisticaRepositorio { get; }
    }
}