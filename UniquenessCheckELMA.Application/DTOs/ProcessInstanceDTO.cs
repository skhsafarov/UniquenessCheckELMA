using System.ComponentModel.DataAnnotations;

using UniquenessCheckELMA.Domain.Entities;

namespace UniquenessCheckELMA.Application.DTOs
{
    public class ProcessInstanceDTO
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Mapping from Entity to DTO
        /// </summary>
        /// <param name="entity"></param>
        public static implicit operator ProcessInstanceDTO(ProcessInstance entity)
        {
            return new ProcessInstanceDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
        /// <summary>
        /// Mapping from DTO to Entity
        /// </summary>
        /// <param name="dto"></param>
        public static implicit operator ProcessInstance(ProcessInstanceDTO dto)
        {
            return new ProcessInstance
            {
                Id = dto.Id,
                Name = dto.Name ?? default!,
                IsActive = dto.IsActive,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt
            };
        }
    }
}
