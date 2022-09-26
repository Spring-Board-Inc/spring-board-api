using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Address : BaseEntity
    {
        [ForeignKey(nameof(UserInformation))]
        public Guid? UserInformationId { get; set; }
        public UserInformation? UserInformation { get; set; }
        
    }
}
