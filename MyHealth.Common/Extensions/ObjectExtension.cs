namespace MyHealth.Common.Extensions
{
    public static class ObjectExtension
    {
        public static bool IsNotNull<T>(this T? pSelf) where T : class
        {
            return pSelf != null;
        }

        public static bool OneOf<T>(this T? obj, params T[] items)
        {
            return OneOf(obj, (IEnumerable<T>)items);
        }

        public static bool OneOf<T>(this T? obj, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                if (obj == null && item == null) return true;
                if (item == null) continue;
                if (obj == null) continue;
                if (obj.Equals(item)) return true;
            }

            return false;
        }

        public static bool IsNumeric(this object? o, bool checkUnderlying = true)
        {
            return o != null && IsNumeric(o.GetType(), checkUnderlying);
        }

        public static bool IsNumeric(this Type? type, bool checkUnderlying = true)
        {
            if (type == null) return false;
            if (checkUnderlying)
            {
                var underType = Nullable.GetUnderlyingType(type);
                if (underType != null) type = underType;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsBoolean(this object? o, bool checkUnderlying = true)
        {
            return o != null && IsBoolean(o.GetType(), checkUnderlying);
        }

        public static bool IsBoolean(this Type? type, bool checkUnderlying = true)
        {
            if (type == null) return false;
            if (checkUnderlying)
            {
                var underType = Nullable.GetUnderlyingType(type);
                if (underType != null) type = underType;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsDateTime(this object? o, bool checkUnderlying = true)
        {
            return o != null && IsDateTime(o.GetType(), checkUnderlying);
        }

        public static bool IsDateTime(this Type? type, bool checkUnderlying = true)
        {
            if (type == null) return false;
            if (checkUnderlying)
            {
                var underType = Nullable.GetUnderlyingType(type);
                if (underType != null) type = underType;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.DateTime:
                    return true;
                default:
                    return false;
            }
        }
    }
}
