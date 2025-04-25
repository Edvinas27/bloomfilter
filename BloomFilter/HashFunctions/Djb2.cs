namespace BloomFilter.HashFunctions;

public static class Djb2
{
    public static ulong Hash(string input)
    {
        ulong hash = 5381; // prime
        foreach (char c in input)
        {
            hash = (hash << 5) + hash + c; // hash * 33 + c // hash << 5 = hash * 2^5
        }

        return hash;
    }
}