namespace Aniquota.Domain.Models
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string Nome { get; set; }
        public float Rendimento { get; set; }
        public virtual ICollection<Aplica> Aplicas { get; set; }
    }
}
