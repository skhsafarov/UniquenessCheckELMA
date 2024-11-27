using UniquenessCheckELMA.Domain.Entities;

namespace UniquenessCheckELMA.Application.DTOs
{
    public class PhysicalPersonDTO
    {
        /// <summary>
        /// ПИНФЛ
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Mapping from Entity to DTO
        /// </summary>
        /// <param name="entity"></param>
        public static implicit operator PhysicalPersonDTO(PhysicalPerson entity)
        {
            return new PhysicalPersonDTO
            {
                Id = entity.Id
            };
        }
        /// <summary>
        /// Mapping from DTO to Entity
        /// </summary>
        /// <param name="dto"></param>
        public static implicit operator PhysicalPerson(PhysicalPersonDTO dto)
        {
            return new PhysicalPerson
            {
                Id = dto.Id
            };
        }
    }
}
