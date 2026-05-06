using Microsoft.Extensions.Configuration;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Infraestrutura.Library.Repositorios.Video_;
using LGG.Infraestrutura.Library.Repositorios.Usuario_;
using LGG.Dominio.Library.Interfaces.IRepositorios.IVideo;
using LGG.Dominio.Library.Interfaces.IRepositorios.IUsuario;
using LGG.Infraestrutura.Library.Repositorios.CampeonatoMensal_;
using LGG.Dominio.Library.Interfaces.IRepositorios.ICampeonatoMensal;

namespace LGG.Infraestrutura.Library.Database
{
    public class UnidTrabalho : IUnidTrabalho
    {
        private IUsuarioRepositorio? _usuarioRepositorio;
        private ICampeonatoMensalRepositorio? _campeonatoMensalRepositorio;
        private IVideoRepositorio? _videoRepositorio;

        private readonly string _dapperString;

        public UnidTrabalho(IConfiguration configuration)
        {
            _dapperString = configuration.GetConnectionString("Postgres")!;
        }

        public IUsuarioRepositorio UsuarioRepositorio => _usuarioRepositorio ??= new UsuarioRepositorio(_dapperString);
        public ICampeonatoMensalRepositorio CampeonatoMensalRepositorio => _campeonatoMensalRepositorio ??= new CampeonatoMensalRepositorio(_dapperString);
        public IVideoRepositorio VideoRepositorio => _videoRepositorio ??= new VideoRepositorio(_dapperString);
    }
}
