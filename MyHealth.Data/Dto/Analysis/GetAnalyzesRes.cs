namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Анализ
    /// </summary>
    public class GetAnalyzesRes : EntityBaseDto
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата анализа
        /// </summary>
        public DateTime Date { get; set; }
    }
}
