using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UniquenessCheckELMA.Application.DTOs
{
    /// <summary>
    /// Учетные данные пользователя для аутентификации в системе.
    /// </summary>
    public class Credentials
    {
        /// <summary>
        /// Логин пользователя.
        /// </summary>
        [Required, DefaultValue("admin")]
        public string Login { get; set; } = null!;
        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        [Required, DefaultValue("admin")]
        public string Password { get; set; } = null!;
    }
}
