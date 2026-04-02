using QuantityMeasurement.Models;
namespace QuantityMeasurement.Repository
{
    // UC18: EF Core user repository implementation.
    public class UserRepository : IUserRepository
    {
        private readonly QuantityMeasurementDbContext context;

        public UserRepository(QuantityMeasurementDbContext context)
        {
            this.context = context;
        }
        // Save a new user to the database
        public UserEntity Save(UserEntity user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }
        // Find user by username, returns null if not found
        public UserEntity FindByUsername(string username)
        {
            var query = from u in context.Users
                        where u.Username == username
                        select u;
            return query.FirstOrDefault()!;
        }
        // Check if username already exists
        public bool ExistsByUsername(string username)
        {
            var query = from u in context.Users
                        where u.Username == username
                        select u;
            return query.Any();
        }
        // Check if email already exists
        public bool ExistsByEmail(string email)
        {
            var query = from u in context.Users
                        where u.Email == email
                        select u;
            return query.Any();
        }
    }
}
