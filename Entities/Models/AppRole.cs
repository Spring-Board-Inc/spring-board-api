using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Entities.Models
{
    [CollectionName("Roles")]
    public class AppRole : MongoIdentityRole<Guid>
    {
    }
}
