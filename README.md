# Enumerables, Heads, Tails, and a Prime Sieve
Explore matching potentially infinite enumerables on head and tail, and apply it to the [Sieve of Eratosthenes](https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes).

This is discussed in [an answer on SO](https://stackoverflow.com/a/5246000) 
where the shortcomings of this approach are discussed.
One problem in particular is that the tail should be 
another enumerable, but the natural way to do see is 
wasteful (see `HeadTailWasteful`) in this repo.
It would be more efficient to make the tail an 
*enumerator* (rather than an enumerable) but this makes 
it hard to process using LINQ-type methods. A half-way
house is to turn the enumerator into an enumerable
which makes processing it easier but results in an
enumerable which doesn't really behave the way one would
expect.
