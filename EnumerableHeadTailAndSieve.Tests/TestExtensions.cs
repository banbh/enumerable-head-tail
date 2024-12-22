using System;
using System.Collections;
using System.Collections.Generic;

namespace EnumerableHeadTailAndSieve.Tests;

public static class TestExtensions
{
    public static IEnumerable<T> Track<T>(this IEnumerable<T> tt,
        Action onGetEnumerator,
        Action onMoveNext,
        Action? onDispose = null)
    {
        return new InstrumentedEnumerable<T>(tt, onGetEnumerator, onMoveNext, onDispose ?? (() => { }));
    }

}

public class InstrumentedEnumerable<T>(
    IEnumerable<T> enumerable,
    Action onGetEnumerator,
    Action onMoveNext,
    Action onDispose) : IEnumerable<T>
{
    public IEnumerator<T> GetEnumerator()
    {
        onGetEnumerator();
        return new InstrumentedEnumerator<T>(enumerable.GetEnumerator(), onMoveNext, onDispose);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class InstrumentedEnumerator<T>(IEnumerator<T> enumerator, Action onMoveNext, Action onDispose) : IEnumerator<T>
{
    public bool MoveNext()
    {
        onMoveNext();
        return enumerator.MoveNext();
    }

    public void Reset()
    {
        enumerator.Reset();
    }

    T IEnumerator<T>.Current => enumerator.Current;

    object? IEnumerator.Current => enumerator.Current;

    public void Dispose()
    {
        onDispose();
        enumerator.Dispose();
    }
}
