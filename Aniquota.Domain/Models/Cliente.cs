using System.ComponentModel.DataAnnotations;

namespace Aniquota.Domain.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }

        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter exatamente 11 caracteres.")]
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Telefone> Telefones { get; set; }
        public virtual ICollection<Aplica> Aplicas { get; set; }
    }
}
