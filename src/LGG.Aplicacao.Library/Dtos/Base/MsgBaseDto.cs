using LGG.Aplicacao.Library.Criptografia;

namespace LGG.Aplicacao.Library.Dtos.Base
{
    public class MsgBaseDto
    {
        public DateTime MsgDataCriacao { get; set; }
        public string MsgFilaToken { get; set; } = string.Empty;

        public MsgBaseDto() { }

        public MsgBaseDto(string cryptoKey) 
        {
            if (!string.IsNullOrEmpty(cryptoKey))
            {
                MsgDataCriacao = DateTime.Now;
                MsgFilaToken = Criptografador.EncryptString(MsgDataCriacao.ToString("yyyyMMddHHmmss"), cryptoKey);
            }
        }

        public bool ValidarMensagem(string cryptoKey)
        {
            if (string.IsNullOrEmpty(cryptoKey))
                return false;

            var msgFilaToken = Criptografador.DecryptString(MsgFilaToken, cryptoKey);

            return MsgDataCriacao.ToString("yyyyMMddHHmmss") == msgFilaToken;
        }
    }
}
