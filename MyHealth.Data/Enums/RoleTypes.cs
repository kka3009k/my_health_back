using System.ComponentModel;

namespace MyHealth.Data.Entities
{
    /// <summary>
    /// Роли системы
    /// </summary>
    public enum RoleTypes
    {
        [Description("Админ")]
        Admin = 1,

        [Description("Пациент")]
        Client,

        [Description("Врач")]
        Doctor,
    }
}
