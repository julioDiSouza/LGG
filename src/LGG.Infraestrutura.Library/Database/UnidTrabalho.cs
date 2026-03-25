using Microsoft.Extensions.Configuration;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Infraestrutura.Library.Repositorios.Canal_;
using LGG.Infraestrutura.Library.Repositorios.Usuario_;
using LGG.Dominio.Library.Interfaces.IRepositorios.ICanal;
using LGG.Dominio.Library.Interfaces.IRepositorios.IUsuario;

namespace LGG.Infraestrutura.Library.Database
{
    public class UnidTrabalho : IUnidTrabalho
    {
        private IUsuarioRepositorio? _usuarioRepositorio;
        private ICanalRepositorio? _canalRepositorio;

        private readonly string _dapperString;

        public UnidTrabalho(IConfiguration configuration)
        {
            _dapperString = configuration.GetConnectionString("Postgres")!;
        }

        public IUsuarioRepositorio UsuarioRepositorio => _usuarioRepositorio ??= new UsuarioRepositorio(_dapperString);
        public ICanalRepositorio CanalRepositorio => _canalRepositorio ??= new CanalRepositorio(_dapperString);
    }
}
