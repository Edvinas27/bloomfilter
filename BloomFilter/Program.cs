namespace BloomFilter;

class Program
{
    static void Main(string[] args)
    {
        /* Automatically finds optimal size of array for holding 100000 elements
         with 0.01 failure rate.
         
         --CONSTRUCTOR--
         n -> expected amount of saved values in array (100000)
         p -> desired false positive rate              (0.01)                
         
         --EXAMPLE FOR FAILING--
         if we take n = 100, and p = 0.99, we get optimal size of array of 2;
         a and c with used hash functions will always be 0,0,0 - collide;
         b and d will be 1,1,0 - collide;
         
          */
        
        
        Core.BloomFilter b = new(100,0.99);
        
        b.Add("a");
        b.Add("b");
        
        Console.WriteLine(b.Contains("c")); // true because fails
        Console.WriteLine(b.Contains("d")); // true because fails

    }
}