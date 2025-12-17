using UserService.DTO;
using UserService.Models;
using UserService.Repository;

namespace UserService.Service
{
    public class UserAppService : IService<UserReceive, UserSend>
    {
        private readonly IRepository<User> repository;

        public UserAppService(IRepository<User> repository)
        {
            this.repository = repository;
        }

        private User DtoToEntity(UserReceive receive, int? id)
        {
            User user = new User()
            {
                Nom = receive.Nom,
                Email = receive.Email
            };

            if (id != null)
            {
                user.Id = (int)id;
            }

            return user;
        }
        private UserSend EntityToDto(User user)
        {
            return new UserSend()
            {
                Id = user.Id,
                Nom = user.Nom,
                Email = user.Email
            };
        }

        // Create a new user
        public UserSend Create(UserReceive receive)
        {
            return EntityToDto(repository.Create(DtoToEntity(receive, null)));
        }

        // Update an existing user
        public UserSend Update(UserReceive receive, int id)
        {
            return EntityToDto(repository.Update(DtoToEntity(receive, id)));
        }

        // Delete a user by id
        public bool Delete(int id)
        {
            return repository.Delete(id);
        }

        // Get user by id
        public UserSend GetById(int id)
        {
            User user = repository.GetById(id);
            if (user == null)
            {
                return null;
            }

            return EntityToDto(user);
        }

        // Get all users
        public List<UserSend> GetAll()
        {
            List<User> users = repository.GetAll();
            List<UserSend> usersDtoSends = new List<UserSend>();
            foreach (var user in users)
            {
                usersDtoSends.Add(EntityToDto(user));
            }

            return usersDtoSends;
        }

    }
}
