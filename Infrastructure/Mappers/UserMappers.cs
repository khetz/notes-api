using Domain.Entities;
using Infrastructure.Models;

namespace Infrastructure.Mappers
{
    internal static class UserMappers
    {
        internal static User ToDomainUser(this UserDbModel user) => new()
        { 
            Id = user.Id,
            Username = user.Username,
            PasswordHash = user.PasswordHash
        };

        internal static UserDbModel ToDbUserModel(this User user) => new()
        {
            Id = user.Id,
            Username = user.Username,
            PasswordHash = user.PasswordHash
        };
    }
}
