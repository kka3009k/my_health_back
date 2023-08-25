namespace MyHealth.Common.Extensions;

public static class CollectionExtension
{
    public static List<T> ToHierarchicalCollection<T, TKey>(this IEnumerable<T> flatList, Func<T, TKey> getKey,
        Func<T, TKey?> getParentKey, Action<T, T> addChild)
    {
        var rootItems = flatList.Where(x => Equals(getParentKey(x), default(TKey))).ToList();

        foreach (var rootItem in rootItems)
        {
            FillHierarchicalChildren(rootItem, flatList, getKey, getParentKey, addChild);
        }

        return rootItems;
    }

    public static bool IsEmpty<T>(this IEnumerable<T>? collection)
    {
        return collection == null || !collection.Any();
    }
        
    public static bool IsEmpty<T>(this T[]? collection)
    {
        return collection == null || collection.Length == 0;
    }
        
    public static bool IsNotEmpty<T>(this IEnumerable<T>? collection)
    {
        return !IsEmpty(collection);
    }
        
    public static bool IsNotEmpty<T>(this T[]? collection)
    {
        return !IsEmpty(collection);
    }

    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> query, bool cond, Func<T, bool> predicate)
    {
        return cond ? query.Where(predicate) : query;
    }

    private static void FillHierarchicalChildren<T, TKey>(T parent, IEnumerable<T> flatList, Func<T, TKey> getKey,
        Func<T, TKey?> getParentKey, Action<T, T> addChild)
    {
        var id = getKey(parent);
        var children = flatList.Where(x => Equals(getParentKey(x), id));
        foreach (var child in children)
        {
            addChild(parent, child);
            FillHierarchicalChildren(child, flatList, getKey, getParentKey, addChild);
        }
    }
}