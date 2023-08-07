using Aniquota.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aniquota.Domain.Controllers
{
    public class ProdutoController : Controller
    {
        public List<Produto> ListaProduto()
        {

            using (var contexto = new Contexto())
            {
                return contexto.Produto
                    .Where(a => a.IdProduto == a.IdProduto)
                    .ToList();

            }

        }

        public Produto BuscaProduto(int idProduto)
        {

            using (var contexto = new Contexto())
            {
                return contexto.Produto.Find(idProduto);

            }

        }
    }
}
