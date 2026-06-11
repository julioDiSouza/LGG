using Microsoft.Extensions.Configuration;
using LGG.Infraestrutura.Library.Repositorios;
using LGG.Dominio.Library.Interfaces.IDatabase;
using LGG.Dominio.Library.Interfaces.IRepositorios;

namespace LGG.Infraestrutura.Library.Database
{
    public class UnidTrabalho : IUnidTrabalho
    {
        private IUsuarioRepositorio? _usuarioRepositorio;
        private ICampeonatoMensalRepositorio? _campeonatoMensalRepositorio;
        private IVideoRepositorio? _videoRepositorio;
        private IVideoCicloRepositorio? _videoCicloRepositorio;
        private IVideoBaldeRepositorio? _videoBaldeRepositorio;
        private IVideoEstatisticaRepositorio? _videoEstatisticaRepositorio;
        private IVideoTopDezRepositorio? _videoTopDezRepositorio;
        private IEmailBaldeRepositorio? _emailBaldeRepositorio;

        private readonly string _dapperString;

        public UnidTrabalho(IConfiguration configuration)
        {
            _dapperString = configuration.GetConnectionString("Postgres")!;
        }

        public IUsuarioRepositorio UsuarioRepositorio => _usuarioRepositorio ??= new UsuarioRepositorio(_dapperString);
        public ICampeonatoMensalRepositorio CampeonatoMensalRepositorio => _campeonatoMensalRepositorio ??= new CampeonatoMensalRepositorio(_dapperString);
        public IVideoRepositorio VideoRepositorio => _videoRepositorio ??= new VideoRepositorio(_dapperString);
        public IVideoCicloRepositorio VideoCicloRepositorio => _videoCicloRepositorio ??= new VideoCicloRepositorio(_dapperString);
        public IVideoBaldeRepositorio VideoBaldeRepositorio => _videoBaldeRepositorio ??= new VideoBaldeRepositorio(_dapperString);
        public IVideoEstatisticaRepositorio VideoEstatisticaRepositorio => _videoEstatisticaRepositorio ??= new VideoEstatisticaRepositorio(_dapperString);
        public IVideoTopDezRepositorio VideoTopDezRepositorio => _videoTopDezRepositorio ??= new VideoTopDezRepositorio(_dapperString);
        public IEmailBaldeRepositorio EmailBaldeRepositorio => _emailBaldeRepositorio ??= new EmailBaldeRepositorio (_dapperString);
    }
}
