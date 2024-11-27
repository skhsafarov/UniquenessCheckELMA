using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

using UniquenessCheckELMA.Domain.SeedWork;

namespace UniquenessCheckELMA.Domain.Entities
{
    public class ProcessInstance : Entity<long>
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [JsonIgnore, InverseProperty(nameof(Application.ProcessInstance))] public virtual List<Application> Applications { get; set; } = new();
    }
}