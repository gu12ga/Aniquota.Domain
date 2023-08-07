namespace Aniquota.Domain.Models
{
    public class AplicaProdutoClienteModel
    {
        //C.IdCliente, P.IdProduto, P.Nome, P.Rendimento, A.ValorAplicado, A.ValorAplicado* (1+P.Rendimento) as ValorAtual, A.DataAplica
        public int IdCliente { get; set; }
        public int IdProduto { get; set; }
        public int Nome { get; set; }
        public float Rendimento { get; set; }
        public float ValorAplicado { get; set; }
        public float ValorAtual { get; set; }
        public DateTime DataAplica { get; set; }
    }
}
