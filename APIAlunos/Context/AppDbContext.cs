using APIAlunos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIAlunos.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Aluno> Alunos { get; set; } //cria uma tabela de nome Aluno

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>().HasData(  //popula a tabela
                new Aluno
                {
                    Id = 1,
                    Nome = "Monica Chiesa",
                    Email = "monica@monica.com",
                    Idade = 27
                },
                new Aluno
                {
                    Id = 2,
                    Nome = "Marcio Kussler",
                    Email = "marcio@marcio.com",
                    Idade = 25
                }
                );

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Aluno>()
            .HasKey(a => a.Id);
        }
    }
}
