using LGG.Aplicacao.Library.Dtos;
using LGG.Aplicacao.Library.Dtos.Youtube;
using LGG.Aplicacao.Library.Interfaces.IRabbitMq;
using LGG.Aplicacao.Library.Interfaces.IServices;
using LGG.Global.Library.Enumeradores;
using LGG.Global.Library.Extensoes;

namespace LGG.Canal.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IRabbitMqConsumer _consumer;
        private readonly IYoutubeService _youTubeService;
        private readonly string _filaCanal;
        private readonly string _filaCryptoKey;
        private readonly string _tag;
        private readonly int _limiteInscritos;

        public Worker(IConfiguration configuration, ILogger<Worker> logger, 
            IRabbitMqConsumer consumer, IYoutubeService youtubeService)
        {
            _logger = logger;
            _consumer = consumer;
            _youTubeService = youtubeService;
            _filaCanal = configuration["RabbitMqFilas:ValidarCanal"]!.ToString();
            _filaCryptoKey = configuration["Criptografia:Fila:ChaveFila"]!.ToString();
            _tag = configuration["Youtube:TagLgg"]!.ToString();
            _limiteInscritos = Convert.ToInt32(configuration["Youtube:LimiteInscritos"]!.ToString());
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            await _consumer.StartAsync(
                _filaCanal,
                ProcessarAsync,
                stoppingToken);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        private async Task<MsgResultadoEnum> ProcessarAsync(string mensagem)
        {
            _logger.LogInformation($"Recebido mensagem: {mensagem}");

            try
            {
                var canalStatus = CanalStatusEnum.NaoLocalizado;
                var usuarioId = string.Empty;
                var codigo = string.Empty;

                if (!JsonExtensao.SafeDeserialize(mensagem, _logger, out CanalValidacaoDtoMsg? msgCanal))
                    return MsgResultadoEnum.Discard;

                if (!msgCanal!.ValidarMensagem(_filaCryptoKey))
                    return MsgResultadoEnum.Discard;

                usuarioId = msgCanal.UsuarioId;
                codigo = _tag.Replace("codigo", msgCanal.Codigo);

                var canaisPorNome = await _youTubeService
                    .ObterCanalPorNomeAsync(msgCanal.Nome);

                if (canaisPorNome!.Items != null && canaisPorNome!.Items!.Any())
                {
                    ItemChannelDto itemCanal = null;
                    foreach (var item in canaisPorNome!.Items!)
                    {
                        var canalPorId = await _youTubeService
                                .ObterCanalPorIdAsync(item.Snippet!.ChannelId);

                        if (canalPorId!.Items != null && canalPorId!.Items!.Any())
                            foreach (var itemId in canalPorId!.Items!)
                            {
                                if (itemId.Snippet!.Description.Contains(codigo))
                                {
                                    canalStatus = CanalStatusEnum.Ativo;

                                    var estatisticas = itemId.Statistics!;
                                    if (Convert.ToInt32(estatisticas.SubscriberCount) > _limiteInscritos)
                                        canalStatus = CanalStatusEnum.BloqueadoPorQuantidadeInscritos;
                                    else
                                        itemCanal = itemId;

                                    break;
                                }
                            }

                        if (canalStatus == CanalStatusEnum.BloqueadoPorQuantidadeInscritos)
                            break;
                    }
                }
                                
                if (!string.IsNullOrEmpty(usuarioId))
                {
                    //await _youTubeService.AtualizarStatusAsync(usuarioId, canalStatus);

                    return MsgResultadoEnum.Ack;
                }

                return MsgResultadoEnum.Discard;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, mensagem);

                return MsgResultadoEnum.Retry;
            }
        }
    }
}