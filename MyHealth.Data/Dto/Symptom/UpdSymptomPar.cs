using System.ComponentModel.DataAnnotations;

namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Параметер обновления симптома
    /// </summary>
    public class UpdSymptomPar : AddSymptomPar
    {
        [Required]
        public Guid ID { get; set; }
    }
}
