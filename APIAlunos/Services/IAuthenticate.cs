using System.Threading.Tasks;

namespace APIAlunos.Services
{
   public interface IAuthenticate
    {
        Task<bool> Authenticate(string email, string password);  //login

        Task<bool> RegisterUser(string email, string password);  //login
        Task Logout();  //logout
    }
}
