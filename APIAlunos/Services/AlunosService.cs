using APIAlunos.Context;
using APIAlunos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace APIAlunos.Services
{
    public class AlunosService : IAlunoService
    {
        private readonly AppDbContext _context;

        public AlunosService(AppDbContext context)
        {
            _context = context;  //construtor do context
        }

        public async Task CreateAluno(Aluno aluno)
        {
            _context.Alunos.Add(aluno); //primeiro cria no contexto
            await _context.SaveChangesAsync(); //depois adiciona no banco de dados
        }

        public async Task DeleteAluno(Aluno aluno)
        {
            _context.Alunos.Remove(aluno); //primeiro remove no contexto
            await _context.SaveChangesAsync(); //depois remove no banco de dados
        }

        public async Task EditAluno(Aluno aluno)
        {
            _context.Entry(aluno).State = EntityState.Modified; //informa que o estado dela foi modificado
            await _context.SaveChangesAsync(); //depois adiciona no banco de dados
        }

        public async Task<Aluno> GetAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id); //busca primeiro na memória
            //permite ganhar em desempenho
            return aluno;
        }
        
        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            try
            {
                return await _context.Alunos.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Aluno>> GetAlunosByNome(string nome)
        {
            IEnumerable<Aluno> alunos;

            if (!string.IsNullOrWhiteSpace(nome))
            {
                alunos = await _context.Alunos.Where(n => n.Nome.Contains(nome)).ToListAsync();
            }
            else {
                alunos = await GetAlunos();
            }
            return alunos;
        }
    }
}
