namespace BloomFilter;

class Program
{
    static void Main(string[] args)
    {
        Core.BloomFilter b = new(10000);
        string[] items = { "world", "hello", "foo", "bar", "baz" };
        
        foreach (var item in items)
        {
            b.Add(item);
        }
        
        foreach (var item in items)
        {
            Console.WriteLine($"Contains {item}: {b.Contains(item)}");
        }
        
        Console.WriteLine($"Contains WORLD: {b.Contains("WORLD")}");
        Console.WriteLine($"Contains baR: {b.Contains("baR")}");
        Console.WriteLine($"Contains ____(): {b.Contains("____()")}");
    }
}