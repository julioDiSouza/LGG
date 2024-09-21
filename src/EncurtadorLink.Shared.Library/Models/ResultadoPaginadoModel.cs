namespace EncurtadorLink.Shared.Library.Models
{
    public class ResultadoPaginadoModel<T>
    {
        public List<T> Colecao { get; set; }
        public int Total { get; set; }
        public int Pagina { get; set; }
        public int Quantidade { get; set; }
    }
}
