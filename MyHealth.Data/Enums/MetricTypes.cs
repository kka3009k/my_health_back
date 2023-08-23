using System.ComponentModel;

namespace MyHealth.Data.Entities
{
    /// <summary>
    /// Тип метрики
    /// </summary>
    public enum MetricTypes
    {
        [Description("Все")]
        All = 1,

        [Description("Рост")]
        Height,

        [Description("Вес")]
        Weight,

        [Description("Сатурация")]
        Saturation,

        [Description("Пульс")]
        Pulse,

        [Description("Глазное давление")]
        IntraocularPressure,

        [Description("Обхват живота")]
        AbdominalGirth,

        [Description("Артериальное давление")]
        ArterialPressure,
    }
}
