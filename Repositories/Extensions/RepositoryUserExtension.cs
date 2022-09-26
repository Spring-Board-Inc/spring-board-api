using Entities.Models;

namespace Repositories.Extensions
{
    public static class RepositoryUserExtension
    {
        public static IQueryable<User> Search(this IQueryable<User> users, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return users;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return users.Where(u => u.FirstName.ToLower().Contains(lowerCaseTerm) || u.LastName.ToLower().Contains(lowerCaseTerm));
        }
    }
}
