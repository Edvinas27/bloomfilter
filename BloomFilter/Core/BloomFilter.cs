using System.Collections;
using BloomFilter.HashFunctions;

namespace BloomFilter.Core;

public class BloomFilter
{
    private readonly BitArray _arr;
    private readonly int _size;
    public BloomFilter(int size)
    {
        // the size should be calculated based on the number of items and the desired false positive rate
        _size = size;
        _arr = new BitArray(_size);
    }

    public void Add(string item)
    {
        //use more hash functions and h3 is bad, just for testing
        item = item.ToLowerInvariant();
        var h1 = Djb2.Hash(item) % (ulong)_size;
        var h2 = Sdbm.Hash(item) % (ulong)_size;
        var h3 = (h1 ^ h2) % (ulong)_size;
        
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
}