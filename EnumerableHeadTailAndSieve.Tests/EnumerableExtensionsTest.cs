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

}