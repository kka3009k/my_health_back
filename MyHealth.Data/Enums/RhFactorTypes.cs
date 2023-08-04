using System.ComponentModel;

namespace MyHealth.Data.Entities
{
    /// <summary>
    /// Резус фактор
    /// </summary>
    public enum RhFactorTypes
    {
        [Description("Положительная")]
        Positive = 1,

        [Description("Отрицательная")]
        Negative
    }
}
