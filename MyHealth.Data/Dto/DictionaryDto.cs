namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Справочник
    /// </summary>
    public class DictionaryDto<TKey, TValue>
    {
        /// <summary>
        /// Ключ
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public TValue Value { get; set; }
    }
}
