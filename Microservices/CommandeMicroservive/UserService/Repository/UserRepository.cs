using UserService.Data;
using UserService.Models;

namespace UserService.Repository
{
    public class UserRepository : IRepository<User>
    {

        private AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get user by id
        public User GetById(int id)
        {
            return _dbContext.Users.Find(id);
        }

        // Get all users
        public List<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        // Create a new user
        public User Create(User entity)
        {
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        // Update an existing user
        public User Update(User entity)
        {
            User user = GetById(entity.Id); 
            if (user == null)
            {
                return null;
            }

            _dbContext.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        // Delete a user by id
        public bool Delete(int id)
        {
            User user = GetById(id);  
            if (user == null) return false;

            _dbContext.Remove(user);
            _dbContext.SaveChanges();
            return true;
        }


    }
}
