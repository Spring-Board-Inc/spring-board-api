using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories
{
    public class Entity<T> where T : class
    {
        [Table(nameof(T))]
        public class EntityTable
        {
            public string Name { get; set; }
        }
    }
}
