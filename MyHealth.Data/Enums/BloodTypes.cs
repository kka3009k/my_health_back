using System.ComponentModel;

namespace MyHealth.Data.Entities
{
    /// <summary>
    /// Тип крови
    /// </summary>
    public enum BloodTypes
    {
        [Description("Первая")]
        I = 1,

        [Description("Вторая")]
        II,

        [Description("Третья")]
        III,

        [Description("Четвертая")]
        IV,
    }
}
