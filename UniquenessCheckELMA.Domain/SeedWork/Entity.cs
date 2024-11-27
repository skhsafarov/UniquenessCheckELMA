using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UniquenessCheckELMA.Domain.SeedWork
{
    public abstract class Entity<T> where T : struct
    {
        [Key, Column(Order = 0), JsonIgnore] public virtual T Id { get; set; }
        public override int GetHashCode() => Id.GetHashCode();
        public override bool Equals(object? obj) => obj is Entity<T> other && Id.GetHashCode() == other.Id.GetHashCode();
    }
}