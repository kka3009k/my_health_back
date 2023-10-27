using System.ComponentModel.DataAnnotations;

namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Параметер обновления анализа
    /// </summary>
    public class UpdAnalysisPar : AddAnalysisPar
    {
        [Required]
        public Guid ID { get; set; }
    }
}
