using LGG.Dominio.Library.Interfaces.IRepositorios.ICanal;
using LGG.Dominio.Library.Interfaces.IRepositorios.IUsuario;
using LGG.Dominio.Library.Interfaces.IRepositorios.ICampeonatoMensal;
using LGG.Dominio.Library.Interfaces.IRepositorios.IVideo;

namespace LGG.Dominio.Library.Interfaces.IDatabase
{
    public interface IUnidTrabalho
    {
        IUsuarioRepositorio UsuarioRepositorio { get; }
        ICanalRepositorio CanalRepositorio { get; }
        ICampeonatoMensalRepositorio CampeonatoMensalRepositorio { get; }
        IVideoRepositorio VideoRepositorio { get; }
    }
}