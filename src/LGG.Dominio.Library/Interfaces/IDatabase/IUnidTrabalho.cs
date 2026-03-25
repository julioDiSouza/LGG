using LGG.Dominio.Library.Interfaces.IRepositorios.ICanal;
using LGG.Dominio.Library.Interfaces.IRepositorios.IUsuario;

namespace LGG.Dominio.Library.Interfaces.IDatabase
{
    public interface IUnidTrabalho
    {
        IUsuarioRepositorio UsuarioRepositorio { get; }
        ICanalRepositorio CanalRepositorio { get; }
    }
}