namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Симптом
    /// </summary>
    public class SymptomDto : UpdSymptomPar
    {
        public new List<FileDto> Files { get; set; }
    }
}
