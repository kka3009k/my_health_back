namespace MyHealth.Api.Extension
{
    public static class ObjectExtension
    {
        public static void FillEntity<T>(this T pTarget, T pSource)
        {

        }

        public static void FillField<TEntity>(this TEntity pTarget, string pOld, string pNew, Action<string> pAction)
        {
            if (!string.IsNullOrWhiteSpace(pNew) && (!pOld?.Equals(pNew) ?? true))
                pAction(pNew);
        }

        public static void FillField<TEntity>(this TEntity pTarget, DateTime? pOld, DateTime? pNew, Action<DateTime?> pAction)
        {
            if (pNew != null && (!pOld?.Equals(pNew) ?? true))
                pAction(pNew);
        }

        public static void FillField<TEntity, TValue>(this TEntity pTarget, TValue pOld, TValue pNew, Action<TValue> pAction)
        {
            if (pNew != null && !pOld.Equals(pNew))
                pAction(pNew);
        }
    }
}
