using Aniquota.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Policy;

namespace Aniquota.Domain.Controllers
{
    public class AplicaController : Controller
    {
        public const string SessionKey = "_idCliente";

        
        public List<AplicaProdutoClienteModel> ListaCliente(ISession session)
        {
            var idCliente = session.GetString(SessionKey);

            using (var contexto = new Contexto())
            {
                return contexto.aplicaprodutoclientemodel
                    .Where(a => a.IdCliente == 1 /*Convert.ToInt32(idCliente)*/)
                    .ToList();

            }

        }
    }
}
