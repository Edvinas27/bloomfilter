namespace BloomFilter.HashFunctions;

public class Sdbm
{
    public static uint Hash(string input)
    {
        uint hash = 65599; //prime
        
        foreach (char c in input)
        {
            hash = c + (hash << 6) + (hash << 16) - hash;
            // each << is equal to multiplying by 2, so basically hash * 64;
        }

        return hash;
    }
}

