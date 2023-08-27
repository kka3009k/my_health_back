using System.ComponentModel;

namespace MyHealth.Data.Entities
{
    /// <summary>
    /// Резус фактор
    /// </summary>
    public enum RhFactorTypes
    {
        [Description("Положительный")]
        Positive = 1,

        [Description("Отрицательный")]
        Negative
    }
}
