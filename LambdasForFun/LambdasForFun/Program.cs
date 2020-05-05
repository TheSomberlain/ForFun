using System;

namespace LambdasForFun
{
    class Program
    {
        static void Main(string[] args)
        {
            Predicate<int> set1 = x => x < -5 && x > -8;
            Predicate<int> set2 = x => x > 5 && x < 8;
            for(int i = -8; i != 9; i++)
            {
                Console.WriteLine(FunctionalSet.Union(set1,set2)(i));
            }
            Console.WriteLine("----------------");
            Console.WriteLine(SetOfInts.Forall(set1, x => x < 0));
            Console.WriteLine(SetOfInts.Exists(set2, x => x % 2 == 0));
            Console.WriteLine("----------------");
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"{i} : {SetOfInts.Map(set2, x=> x-2)(i)}");
            }
            Console.ReadKey();
        }
    }
}
