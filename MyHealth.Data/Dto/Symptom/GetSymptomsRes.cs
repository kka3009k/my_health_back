namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Результат получения симптомов
    /// </summary>
    public class GetSymptomsRes : EntityBaseDto
    {
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }
    }
}
