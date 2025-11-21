using Microsoft.EntityFrameworkCore;
using Cardapio_Inteligente.Modelos;

namespace Cardapio_Inteligente.Servicos
{
    /// <summary>
    /// DbContext para a API interna (SQLite local)
    /// </summary>
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Prato> Pratos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações da tabela Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Senha).IsRequired().HasMaxLength(256);
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Configurações da tabela Prato
            modelBuilder.Entity<Prato>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Descricao).HasMaxLength(500);
                entity.Property(e => e.Preco).HasColumnType("decimal(10,2)");
            });

            // Seed de dados iniciais
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Usuário de teste
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Nome = "Admin",
                    Email = "admin@cardapio.com",
                    Senha = BCrypt.Net.BCrypt.HashPassword("admin123")
                }
            );

            // Pratos de exemplo
            modelBuilder.Entity<Prato>().HasData(
                new Prato { Id = 1, Nome = "Feijoada Completa", Descricao = "Feijoada com acompanhamentos", Preco = 35.90m, Categoria = "Prato Principal" },
                new Prato { Id = 2, Nome = "Lasanha Bolonhesa", Descricao = "Lasanha tradicional", Preco = 28.50m, Categoria = "Prato Principal" },
                new Prato { Id = 3, Nome = "Salada Caesar", Descricao = "Salada com frango grelhado", Preco = 22.00m, Categoria = "Salada" },
                new Prato { Id = 4, Nome = "Pizza Margherita", Descricao = "Pizza com mussarela e tomate", Preco = 42.00m, Categoria = "Pizza" },
                new Prato { Id = 5, Nome = "Hambúrguer Artesanal", Descricao = "Hambúrguer 200g com bacon", Preco = 32.90m, Categoria = "Lanche" }
            );
        }
    }
}
