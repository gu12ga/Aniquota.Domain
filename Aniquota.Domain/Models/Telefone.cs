using System.Security.Policy;
using System.ComponentModel.DataAnnotations;

namespace Aniquota.Domain.Models
{
    public class Telefone
    {
        public int IdCliente { get; set; }

        [StringLength(9, MinimumLength = 9, ErrorMessage = "O Telefone deve ter exatamente 9 caracteres.")]
        public string Tel { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}
