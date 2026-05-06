using LGG.Dominio.Library.Interfaces.IRepositorios.IVideo;
using LGG.Dominio.Library.Interfaces.IRepositorios.IUsuario;
using LGG.Dominio.Library.Interfaces.IRepositorios.ICampeonatoMensal;

namespace LGG.Dominio.Library.Interfaces.IDatabase
{
    public interface IUnidTrabalho
    {
        IUsuarioRepositorio UsuarioRepositorio { get; }
        ICampeonatoMensalRepositorio CampeonatoMensalRepositorio { get; }
        IVideoRepositorio VideoRepositorio { get; }
    }
}