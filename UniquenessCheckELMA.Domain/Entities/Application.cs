using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

using UniquenessCheckELMA.Domain.SeedWork;

namespace UniquenessCheckELMA.Domain.Entities
{
    public class Application : Entity<long>
    {
        public long? ClaimId { get; set; }
        public bool IsActive { get; set; }
        public string? Status { get; set; }
        public long PhysicalPersonId { get; set; }
        public long ProcessInstanceId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [JsonIgnore, InverseProperty(nameof(PhysicalPerson.Applications))] public virtual PhysicalPerson PhysicalPerson { get; set; } = null!;
    }
}