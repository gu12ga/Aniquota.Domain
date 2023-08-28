using Aniquota.Domain.Models;
//using System.Data.Entity;
using /*EF =*/ Microsoft.EntityFrameworkCore;
//using MySQL.EntityFrameworkCore.Extensions;
//using System.Security.Policy;

namespace Aniquota.Domain
{
    public class Contexto : /*EF.*/DbContext
    {
        public DbSet<Aplica> Aplica { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Telefone> Telefone { get; set; }
        public DbSet<AplicaProdutoClienteModel> aplicaprodutoclientemodel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=aniquota;user=root;password=GustavoGabriel10");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.IdProduto);
                entity.Property(e => e.Nome).IsRequired();
                entity.Property(e => e.Rendimento).IsRequired();
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);
                entity.HasIndex(e => e.CPF).IsUnique();
                entity.Property(e => e.Senha).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Nome).IsRequired();
            });

            modelBuilder.Entity<Telefone>(entity =>
            {
                entity.HasKey(e => new { e.IdCliente, e.Tel });
                entity.HasOne(d => d.Cliente)
                .WithMany(p => p.Telefones);
            });

            modelBuilder.Entity<AplicaProdutoClienteModel>(entity =>
            {
                entity.ToView(nameof(aplicaprodutoclientemodel));
                entity.HasNoKey();

            });

            modelBuilder.Entity<Aplica>(entity =>
            {
                entity.HasKey(e => new { e.IdCliente, e.IdProduto, e.DataAplica });

                entity.Property(e => e.ValorAplicado).IsRequired();

                entity.HasOne(d => d.Cliente)
                .WithMany(p => p.Aplicas);

                entity.HasOne(d => d.Produto)
                .WithMany(p => p.Aplicas);
            });
        }
    }
}
