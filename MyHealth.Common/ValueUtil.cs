using System.Reflection;
using MyHealth.Common.Extensions;

namespace MyHealth.Common
{
    public static class ValueUtil
    {
        /// <summary>
        /// Изменение значения в объекте
        /// </summary>
        public static void SetValue(this object pObject, string pProperty, object pValue)
        {
            var property = GetProperty(pObject, pProperty);

            if (property == null || property.GetSetMethod() == null) return;

            var path = pProperty.Split(".");

            if (path.Length > 1)
            {
                for (int i = 0; i < path.Length - 1; i++)
                {
                    var propertyToGet = pObject.GetType().GetProperty(path[i]);
                    pObject = propertyToGet.GetValue(pObject, null);
                }
            }

            if (pValue == null || pObject == null)
            {
                property.SetValue(pObject, pValue); return;
            }

            var type = GetType(property);

            if (type == typeof(string))
            {
                property.SetValue(pObject, pValue.ToString());
            }
            else if (type == typeof(int))
            {
                property.SetValue(pObject, int.Parse(pValue.ToString()));
            }
            else if (type == typeof(short))
            {
                property.SetValue(pObject, short.Parse(pValue.ToString()));
            }
            else if (type == typeof(bool))
            {
                property.SetValue(pObject, bool.Parse(pValue.ToString()));
            }
            else if (type.IsEnum)
            {
                property.SetValue(pObject, Enum.Parse(type, pValue.ToString()));
            }
            else if (type == typeof(decimal))
            {
                property.SetValue(pObject, decimal.Parse(pValue.ToString()));
            }
            else
            {
                property.SetValue(pObject, pValue);
            }
        }

        /// <summary>
        /// Получение значения из объекта
        /// </summary>
        public static object? GetValue(this object? obj, string prop)
        {
            if (obj == null || string.IsNullOrWhiteSpace(prop)) return null;

            var path = prop.Split(".");
            var property = GetProperty(obj, path[0]);

            if (property == null) return null;

            if (path.Length > 1)
            {
                var value = property.GetValue(obj);
                return GetValue(value, prop.Replace($"{path[0]}.", ""));
            }

            return property.GetValue(obj);
        }

        public static T? GetValue<T>(this object? obj, string prop, T? def = default)
        {
            return GetValue(obj, prop) is T res ? res : def;
        }

        public static T? GetValue<T>(this object? obj, PropertyInfo pProperty, T? def = default)
        {
            return GetValue(obj, pProperty.Name, def);
        }

        /// <summary>
        /// Возварщает инфорамацию о свойстве объекта
        /// </summary>
        public static PropertyInfo? GetProperty(this object? pObject, string pProperty)
        {
            if (pObject == null || string.IsNullOrWhiteSpace(pProperty)) return null;

            return GetProperty(pObject.GetType(), pProperty);
        }

        public static PropertyInfo? GetProperty<T>(string pProperty)
        {
            if (string.IsNullOrWhiteSpace(pProperty)) return null;

            return GetProperty(typeof(T), pProperty);
        }

        public static PropertyInfo? GetProperty(Type pType, string pProperty)
        {
            if (pType == null || string.IsNullOrWhiteSpace(pProperty)) return null;

            var path = pProperty.Split(".");
            var property = pType.GetProperty(path[0],
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

            if (property == null) 
                return null;

            if (path.Length > 1)
            {
                var propertyType = property.PropertyType;
                return GetProperty(propertyType, pProperty.Replace($"{path[0]}.", ""));
            }

            return property;
        }

        /// <summary>
        /// Получение типа из свойства
        /// </summary>
        /// <param name="pProperty"></param>
        /// <returns></returns>
        public static Type GetType(PropertyInfo pProperty)
        {
            var type = Nullable.GetUnderlyingType(pProperty.PropertyType);
            return type ?? pProperty.PropertyType;
        }

        /// <summary>
        /// Получение типа из свойства
        /// </summary>
        public static Type? GetType(Type? initType)
        {
            if (initType == null) return null;
            var type = Nullable.GetUnderlyingType(initType);
            return type ?? initType;
        }

        public static void InitObjectFromDictionary(object pObject, IDictionary<string, object>? pDictionary)
        {
            pDictionary?.ToList().ForEach(f => SetValue(pObject, f.Key, f.Value));
        }

        public static void InitObjectFromObject(object pOldObject, object pNewObject)
        {
            if (pOldObject == null || pNewObject == null) return;
            pOldObject.GetType()
                .GetProperties()
                .Where(w => w.PropertyType.IsPublic)
                .ToList()
                .ForEach(f => SetValue(pOldObject, f.Name, GetValue(pNewObject, f.Name)));
        }

        public static bool IsNullOrZero(this int? pValue)
        {
            return pValue is null or 0;
        }

        /// <summary>
        /// Invokes nonstatic method
        /// </summary>
        public static object? Invoke(this object obj, string methodName, Type[] genericArguments,
            params object?[] parameters)
        {
            return Invoke(obj, obj.GetType(), methodName, genericArguments, parameters);
        }

        /// <summary>
        /// Invokes static method
        /// </summary>
        public static object? Invoke(this Type type, string methodName, Type[] genericArguments, params object?[] parameters)
        {
            return Invoke(null, type, methodName, genericArguments, parameters);
        }

        /// <summary>
        /// Invokes static or nonstatic method, depending on obj is null or not
        /// </summary>
        public static object? Invoke(object? obj, Type type, string methodName, Type[] genericArguments,
            params object?[] parameters)
        {
            var method = type
                .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                            BindingFlags.NonPublic)
                .Where(x => x.Name == methodName)
                .WhereIf(!genericArguments.IsEmpty(), x => x.GetGenericArguments().Length == genericArguments.Length)
                .First(x => x.GetParameters().Length == parameters.Length);
            if (!genericArguments.IsEmpty())
                method = method.MakeGenericMethod(genericArguments);

            return method.Invoke(obj, parameters);
        }

        public static object? CreateInstance(this Type type, Type[] arguments, params object?[] parameters)
        {
            return type.GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
                    null, arguments, null)?
                .Invoke(parameters);
        }

        public static T? CreateInstance<T>(Type[] arguments, params object?[] parameters)
        {
            return (T?)CreateInstance(typeof(T), arguments, parameters);
        }
    }
}
