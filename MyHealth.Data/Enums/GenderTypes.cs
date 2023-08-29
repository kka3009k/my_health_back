using System.ComponentModel;

namespace MyHealth.Data.Entities
{
    /// <summary>
    /// Пол
    /// </summary>
    public enum GenderTypes
    {
        [Description("Мужской")]
        Male = 1,

        [Description("Женский")]
        Female,
    }
}
