using LGG.Dominio.Library.Interfaces.IRepositorios.IVideo;
using LGG.Dominio.Library.Interfaces.IRepositorios.IUsuario;
using LGG.Dominio.Library.Interfaces.IRepositorios.IVideoCiclo;
using LGG.Dominio.Library.Interfaces.IRepositorios.ICampeonatoMensal;
using LGG.Dominio.Library.Interfaces.IRepositorios.IVideoBalde;
using LGG.Dominio.Library.Interfaces.IRepositorios.IVideoEstatistica;

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