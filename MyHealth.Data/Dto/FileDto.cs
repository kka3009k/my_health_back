namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Файл
    /// </summary>
    public class FileDto : EntityBaseDto
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Расширение
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Путь
        /// </summary>
        public string Path { get; set; }
    }
}
