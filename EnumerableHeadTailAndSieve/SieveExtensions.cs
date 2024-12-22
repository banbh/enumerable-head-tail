namespace EnumerableHeadTailAndSieve;

public static class SieveExtensions
{
    public static IEnumerable<int> Nats(this int start)
    {
        var n = start;
        while (true) yield return checked(n++);
        // ReSharper disable once IteratorNeverReturns
    }

    public static IEnumerable<T> ReplaceEveryNth<T>(this IEnumerable<T> tt, int n, T replacement)
    {
        foreach (var (i, t) in tt.Index())
        {
            yield return n != 0 && i % n == n - 1 ? replacement : t;
        }
    }

    public static IEnumerable<int> Sieve(this IEnumerable<int> numbers) =>
        numbers.HeadTail(n => n, (p, nn) => nn.ReplaceEveryNth(p, 0).Sieve());

    public static IEnumerable<int> Primes(this int n) => 2.Nats().Sieve().Where(x => x > 0).Take(n);

}