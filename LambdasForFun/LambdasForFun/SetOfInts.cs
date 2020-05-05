using System;
using System.Collections.Generic;
using System.Text;

namespace LambdasForFun
{
    public static class SetOfInts
    {
        private static int leftBind = -1000;
        private static int rightBind = 1000;

        public static bool Forall(Predicate<int> set, Predicate<int> predicate)
        {
            for(int i = leftBind; i < rightBind; i++)
            {
                if (set(i) && !predicate(i)) return false;
            }
            return true;
        }

        public static bool Exists(Predicate<int> set, Predicate<int> predicate)
        {
            return !Forall(set, x => !predicate(x));
        }

        public static Predicate<int> Map(Predicate<int> set, Func<int,int> p)
        {
            return y => Exists(set, x => p(x) == y);
        } 
    }
}
