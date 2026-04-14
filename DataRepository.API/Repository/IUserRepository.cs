using DataRepository.API.Models;
namespace DataRepository.API
{
    // UC18: User repository interface for authentication.
    public interface IUserRepository
    {
        UserEntity Save(UserEntity user);
        UserEntity FindByUsername(string username);
        bool ExistsByUsername(string username);
        bool ExistsByEmail(string email);
    }
}
