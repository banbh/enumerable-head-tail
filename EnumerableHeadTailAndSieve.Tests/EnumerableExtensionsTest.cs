using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnumerableHeadTailAndSieve.Tests;

[TestClass]
public class EnumerableExtensionsTest
{
    
    [TestMethod]
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public void TestEnumeratorEnumerable()
    {
        var nn = Enumerable.Range(0, 5);
        using var y = nn.GetEnumerator();
        var x = y.EnumeratorEnumerable();

        // Below we show how the result of a EnumeratorEnumerable() does not behave like
        // a normal enumerable. Since it's really an enumerator it gets "used up".
        int n = 2;
        CollectionAssert.AreEqual(nn.Take(n).ToList(), x.Take(n).ToList()); // as expected
        CollectionAssert.AreEqual(nn.Skip(n).ToList(), x.ToList()); // does not reset
        Assert.AreEqual(0, x.Count()); // still has not reset
    }

    [TestMethod]
    public void TestHeadTailWasteful()
    {
        int n = 5;
        int gec = 0, mnc = 0, d = 0;
        CollectionAssert.AreEqual(new[] { -n, (n + 1) * n, (n + 2) * n },
            n.Nats()
                .Track(() => gec++, () => mnc++, () => d++)
                .HeadTailWasteful(k => -k, (x, yy) => yy.Select(y => y * x))
                .Take(3).ToList());
        Assert.AreEqual(2, gec); // it starts enumerating twice
        Assert.AreEqual(1 + 3, mnc); // moves first once and second three times
        Assert.AreEqual(2, d); // disposes both
    }

    [TestMethod]
    public void TestHeadTail()
    {
        int n = 5;
        int gec = 0, mnc = 0, d = 0;
        CollectionAssert.AreEqual(new[] { -n, (n + 1) * n, (n + 2) * n },
            n.Nats()
                .Track(() => gec++, () => mnc++, () => d++)
                .HeadTail(k => -k, (x, yy) => yy.Select(y => y * x))
                .Take(3).ToList());
        Assert.AreEqual(1, gec);
        Assert.AreEqual(3, mnc);
        Assert.AreEqual(1, d);
    }

}