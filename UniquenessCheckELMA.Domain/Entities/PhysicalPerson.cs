using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

using UniquenessCheckELMA.Domain.SeedWork;

namespace UniquenessCheckELMA.Domain.Entities
{
    public class PhysicalPerson: Entity<long>
    {
        [JsonIgnore, InverseProperty(nameof(Application.PhysicalPerson))] public virtual List<Application> Applications { get; set; } = new();
    }
}