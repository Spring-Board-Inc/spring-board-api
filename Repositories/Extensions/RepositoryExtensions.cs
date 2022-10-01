using Entities.Models;

namespace Repositories.Extensions
{
    public static class RepositoryExtensions
    {
        public static IQueryable<Location> Search(this IQueryable<Location> locations, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return locations;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return locations.Where(l => l.State.ToLower().Contains(lowerCaseTerm) || l.Town.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Company> Search(this IQueryable<Company> companies, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return companies;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return companies.Where(c => c.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Job> Search(this IQueryable<Job> jobs, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return jobs;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return jobs.Where(j => j.Title.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Industry> Search(this IQueryable<Industry> industries, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return industries;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return industries.Where(i => i.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<AppUser> Search(this IQueryable<AppUser> users, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return users;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return users.Where(u => u.FirstName.ToLower().Contains(lowerCaseTerm) || u.LastName.ToLower().Contains(lowerCaseTerm));
        }
    }
}
