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
/// </comments>

public class BloomFilter
{
    private readonly BitArray _arr;
    private readonly double _size;
    public BloomFilter(double n = 100000, double p = 0.01)
    {
        try
        {
            _size = (int)(-n * Math.Log(p) / Math.Pow(Math.Log(2), 2));

            if (_size <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(n)} or {nameof(p)}",_size,
                    $"{nameof(n)} : {n}, {nameof(p)} : {p} - Size of array must be greater than 0");
            }
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
            
            Console.WriteLine("Using default size of 100000 with 0.01 failure rate");
            n = 100000;
            p = 0.01;
            _size = (int)(-n * Math.Log(p) / Math.Pow(Math.Log(2), 2));
        }
        
        _arr = new BitArray((int)_size);
        Console.WriteLine($"Optimal size of array: {_size}");
    }

    public void Add(string item)
    {
        //use more hash functions and h3 is bad, just for testing
        item = item.ToLowerInvariant();
        var h1 = Djb2.Hash(item) % (ulong)_size;
        var h2 = Sdbm.Hash(item) % (ulong)_size;
        var h3 = (h1 ^ h2) % (ulong)_size;
        
        Console.WriteLine($"Adding {item} h1: {h1}, h2: {h2}, h3: {h3}");
        
        _arr[(int)h1] = true;
        _arr[(int)h2] = true;
        _arr[(int)h3] = true;
    }
    
    public bool Contains(string item)
    {
        item = item.ToLowerInvariant();
        var h1 = Djb2.Hash(item) % (ulong)_size;
        var h2 = Sdbm.Hash(item) % (ulong)_size;
        var h3 = (h1 ^ h2) % (ulong)_size;

        
        return _arr[(int)h1] && 
               _arr[(int)h2] && 
               _arr[(int)h3];
    }
    
    public double OptimalHashAmount(double n, double p = 0.01)
    {
        // Where n is the number of items and p is the desired false positive rate
        
        return Math.Round((_size / n) * Math.Log(2),1);
    }
}