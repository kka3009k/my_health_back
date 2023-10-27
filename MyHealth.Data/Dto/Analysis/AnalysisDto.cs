namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Анализ
    /// </summary>
    public class AnalysisDto : UpdAnalysisPar
    {
        /// <summary>
        /// Путь к файлу анализа
        /// </summary>
        public new string? File { get; set; }
    }
}
