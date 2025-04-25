using System.Collections;
using BloomFilter.HashFunctions;

namespace BloomFilter.Core;

/// <comments>
/// We will save _size (size of bitarray) as double,
/// because we will need the value as double later for
/// calculating optimal amount of hash functions.
///
///
/// If user fails to specify n and p or specifies n and p that makes the _size 0, we will use default values;
///
///
/// add hash functions and calculate optimal amount of needed hashfuncs
/// </comments>

public class BloomFilter
{
    private readonly BitArray _arr;
    private readonly int _size;
    private const int DefaultN = 100000;
    private const double DefaultP = 0.01;
    public BloomFilter(int n = DefaultN, double p = DefaultP)
    {
        if (!ValidateInput(n, p))
        {
            Console.WriteLine("Invalid input, using default values");
            n = DefaultN;
            p = DefaultP;
        }
        
        _size = CalculateSize(n, p);

        if (_size == int.MaxValue)
        {
            Console.WriteLine("Warning, using max size of filter");
        }
        
        _arr = new BitArray(_size);
        
        Console.WriteLine($"Optimal size of array: {_size}");
    }

    public void Add(string item)
    {
        //use more hash functions and h3 is bad, just for testing
        item = item.ToLowerInvariant();
        var h1 = Djb2.Hash(item) % _size;
        var h2 = Sdbm.Hash(item) % _size;
        var h3 = (h1 ^ h2) % _size;
        
        Console.WriteLine($"Adding {item} h1: {h1}, h2: {h2}, h3: {h3}");
        
        _arr[(int)h1] = true;
        _arr[(int)h2] = true;
        _arr[(int)h3] = true;
    }
    
    public bool PossiblyContains(string item)
    {
        item = item.ToLowerInvariant();
        var h1 = Djb2.Hash(item) % _size;
        var h2 = Sdbm.Hash(item) % _size;
        var h3 = (h1 ^ h2) % _size;

        
        return _arr[(int)h1] && 
               _arr[(int)h2] && 
               _arr[(int)h3];
    }

    private static bool ValidateInput(int n, double p)
    {
        return (n > 0) && (p is > 0 and < 1);
    }

    private static int CalculateSize(int n, double p)
    {
        return (int)Math.Ceiling(-n * Math.Log(p) / Math.Pow(Math.Log(2), 2));
    }
}