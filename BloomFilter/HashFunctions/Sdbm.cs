namespace BloomFilter.HashFunctions;

public class Sdbm
{
    public static ulong Hash(string input)
    {
        ulong hash = 65599; //prime
        
        foreach (char c in input)
        {
            hash = c + (hash << 6) + (hash << 16) - hash; // hash * 2^6 + hash * 2^16 - hash
        }

        return hash;
    }
}