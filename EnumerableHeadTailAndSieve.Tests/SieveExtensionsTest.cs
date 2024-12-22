using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnumerableHeadTailAndSieve.Tests;

[TestClass]
public class SieveExtensionsTest
{
    [TestMethod]
    public void TestNats()
    {
        const int start = -42, count = 100;
        CollectionAssert.AreEqual(Enumerable.Range(start, count).ToList(), 
            start.Nats().Take(count).ToList());

        CollectionAssert.AreEqual(new[] { int.MaxValue }, int.MaxValue.Nats().Take(1).ToList());
        Assert.ThrowsException<OverflowException>(() => int.MaxValue.Nats().Take(2).Count());
        Assert.ThrowsException<OverflowException>(() => (int.MaxValue - 100).Nats().Count());
    }

    [TestMethod]
    public void TestReplaceEveryNth()
    {
        CollectionAssert.AreEqual(new[]
            {
                4, 5, 0,
                7, 8, 0,
                10, 11, 0,
                13
            },
            4.Nats().ReplaceEveryNth(3, 0).Take(10).ToList());
    }

    [TestMethod]
    public void TestPrimes()
    {
        int gec2 = 0, mnc2 = 0;
        CollectionAssert.AreEqual(
            new[]
            {
                // See https://oeis.org/A000040/list
                2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59,
                61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127,
                131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191,
                193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257,
                263, 269, 271
            },
            2.Nats()
                .Sieve()
                .Track(() => gec2++, () => mnc2++)
                .Where(x => x > 0)
                .Take(58).ToList());
        Assert.AreEqual(1, gec2);
        Assert.AreEqual(270, mnc2);
    }
}