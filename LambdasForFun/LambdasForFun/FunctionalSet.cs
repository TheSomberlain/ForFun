using System;
using System.Collections.Generic;
using System.Text;

namespace LambdasForFun
{
    public static class FunctionalSet
    {
        public static Predicate<T> Empty<T>() where T : class 
        {
            return x => x == default(T);
        } 

        public static Predicate<T> SingletonSet<T>(T value)
        {
            return x => value.Equals(x);
        }

        public static Predicate<T> Union<T>(Predicate<T> e, Predicate<T> f)
        {
            return x => e(x) || f(x);
        }

        public static Predicate<T> Intersect<T>(Predicate<T> e, Predicate<T> f)
        {
            return x => e(x) && f(x);
        }

        public static Predicate<T> Diff<T>(Predicate<T> e, Predicate<T> f)
        {
            return x => e(x) && !f(x);
        }

        public static Predicate<T> Filter<T>(Predicate<T> e, Predicate<T> filter)
        {
            return Intersect(e, filter);
        }
    }
}
