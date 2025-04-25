namespace BloomFilter.HashFunctions;

public static class Djb2
{
    public static uint Hash(string input)
    {
        uint hash = 5381; // prime
        foreach (char c in input)
        {
            hash = (hash << 5) + hash + c;
        }

        return hash;
    }
}