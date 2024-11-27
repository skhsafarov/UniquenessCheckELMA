using System.ComponentModel.DataAnnotations;

using Entity = UniquenessCheckELMA.Domain.Entities.Application;

namespace UniquenessCheckELMA.Application.DTOs
{
    public class ApplicationDTO
    {
        public long Id { get; set; }
        public string? Status { get; set; }
        public bool IsActive { get; set; }
        [Range(10000000000000, 99999999999999, ErrorMessage = "ПИНФЛ должен состоять и 14 цифр!")]
        public long PhysicalPersonId { get; set; }
        public long ProcessInstanceId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Mapping from Entity to DTO
        /// </summary>
        /// <param name="entity"></param>
        public static implicit operator ApplicationDTO(Entity entity)
        {
            return new ApplicationDTO
            {
                Id = entity.Id,
                Status = entity.Status,
                IsActive = entity.IsActive,
                PhysicalPersonId = entity.PhysicalPersonId,
                ProcessInstanceId = entity.ProcessInstanceId,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
        /// <summary>
        /// Mapping from DTO to Entity
        /// </summary>
        /// <param name="dto"></param>
        public static implicit operator Entity(ApplicationDTO dto)
        {
            return new Entity
            {
                Id = dto.Id,
                Status = dto.Status,
                IsActive = dto.IsActive,
                PhysicalPersonId = dto.PhysicalPersonId,
                ProcessInstanceId = dto.ProcessInstanceId,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt
            };
        }
    }
}
