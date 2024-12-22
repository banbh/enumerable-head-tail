// See https://aka.ms/new-console-template for more information
using EnumerableHeadTailAndSieve;

int n = 100;
Console.WriteLine($"The first {n} primes are {string.Join(", ", n.Primes())}.");
