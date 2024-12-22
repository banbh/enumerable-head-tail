namespace EnumerableHeadTailAndSieve;

public static class EnumerableExtensions
{

    public static IEnumerable<TOut> HeadTailWasteful<TIn, TOut>(
        this IEnumerable<TIn> enumerable,
        Func<TIn, TOut> headFunc,
        Func<TIn, IEnumerable<TIn>, IEnumerable<TOut>> tailFunc)
    {
        // ReSharper disable once PossibleMultipleEnumeration
        var first = enumerable.First();
        // ReSharper disable once PossibleMultipleEnumeration
        return new[] { headFunc(first) }.Concat(tailFunc(first, enumerable.Skip(1)));
    }

    public static IEnumerable<T> EnumeratorEnumerable<T>(this IEnumerator<T> tt)
    {
        while (tt.MoveNext()) yield return tt.Current; // NB: does not dispose tt
    }
    
    public static IEnumerable<TOut> HeadTail<TIn, TOut>(
        this IEnumerable<TIn> enumerable,
        Func<TIn, TOut> headFunc,
        Func<TIn, IEnumerable<TIn>, IEnumerable<TOut>> tailFunc)
    {
        using var enumerator = enumerable.GetEnumerator();
        if (enumerator.MoveNext())
        {
            var head = enumerator.Current;
            yield return headFunc(head);
            foreach (var x in tailFunc(head, enumerator.EnumeratorEnumerable())) yield return x;
        }
    }
}