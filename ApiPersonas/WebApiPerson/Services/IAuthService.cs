using WebApiPerson.Models;

namespace WebApiPerson.Services
{
    public interface IAuthService
    {
        string Authenticate(UserLogin user);
    }
}
