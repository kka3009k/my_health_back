using System.Reflection;

namespace MyHealth.Common
{
    public class TypeUtil
    {
        public static Type GetBaseType(Type pType, Type pBaseType)
        {
            if (pType.BaseType == null) return pType;
            return pType.BaseType != null && pType.BaseType == pBaseType
                ? pType.BaseType
                : GetBaseType(pType.BaseType, pBaseType);
        }

        public static Type GetTypeFromAssembly(string pAssembly, string pName)
        {
            if (string.IsNullOrEmpty(pName))
                throw new ArgumentNullException(nameof(pName), @"pName должен содержать наименование типа");

            if (string.IsNullOrEmpty(pAssembly))
                throw new ArgumentNullException(nameof(pAssembly), @"pAssembly должен содержать наименование сборки");

            var types = Assembly.Load(pAssembly).GetTypes();
            var type = types.FirstOrDefault(t => t.Name == pName);

            return type ?? throw new ArgumentException("Тип не найден");
        }

        /// <summary>
        /// Returns all the leaf types (non abstract and non interface) that implements a baseType
        /// </summary>
        /// <param name="baseType"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetLeafTypes(Type baseType)
        {
            var allTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(baseType.IsAssignableFrom).ToArray();
            return GetLeafTypes(allTypes);
        }

        private static IEnumerable<Type> GetLeafTypes(Type[] allTypes)
        {
            foreach (var type in allTypes.ToArray())
            {
                if (type.IsAbstract || type.IsInterface || type.ContainsGenericParameters) continue;
                if (allTypes.All(x => x.BaseType != type))
                    yield return type;
            }
        }
    }
}
