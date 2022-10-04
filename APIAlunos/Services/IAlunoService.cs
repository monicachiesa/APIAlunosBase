using APIAlunos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIAlunos.Services
{
    public interface IAlunoService
    {
        Task<IEnumerable<Aluno>> GetAlunos(); //a classe Task é assíncrona 
        Task<Aluno> GetAluno(int id);
        Task<IEnumerable<Aluno>> GetAlunosByNome(string nome);
        Task CreateAluno(Aluno aluno);
        Task EditAluno(Aluno aluno);
        Task DeleteAluno(Aluno aluno);
    }
}
