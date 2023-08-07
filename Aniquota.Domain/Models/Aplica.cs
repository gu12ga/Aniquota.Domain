namespace Aniquota.Domain.Models
{
    public class Aplica
    {
        public int IdProduto { get; set; }

        public int IdCliente { get; set; }

        public float ValorAplicado { get; set; }

        public DateTime DataAplica { get; set; }

        public DateTime? DataResgate { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual Produto Produto { get; set; }
    }
}
