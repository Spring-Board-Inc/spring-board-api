using Entities.Models;

namespace Repositories.Extensions
{
    public static class RepositoryExtensions
    {
        public static IQueryable<State> Search(this IQueryable<State> states, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return states;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return states.Where(s => s.AdminArea.ToLower().Contains(lowerCaseTerm)
                         || s.Country.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Skill> Search(this IQueryable<Skill> skills, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return skills;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return skills.Where(s => s.Description.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Country> Search(this IQueryable<Country> countries, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return countries;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return countries.Where(c => c.Name.ToLower().Contains(lowerCaseTerm));
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
            return jobs.Where(j => j.Title.ToLower().Contains(lowerCaseTerm)
                        || j.Descriptions.ToLower().Contains(lowerCaseTerm));
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

        public static IQueryable<Faq> Search(this IQueryable<Faq> faqs, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return faqs;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return faqs.Where(u => u.Question.ToLower().Contains(lowerCaseTerm) || u.Answer.ToLower().Contains(lowerCaseTerm));
        }
    }
}
