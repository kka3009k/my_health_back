namespace MyHealth.Common.Extensions
{
    public static class ValueExtension
    {
        public static bool IsNullOrZero(this int? pValue)
        {
            return pValue == null || pValue == 0;
        }

        public static bool IsNullOrZero(this int pValue)
        {
            return pValue == 0;
        }
    }
}
