using NUnit.Framework;
using System;
using LambdasForFun;

namespace LambdaTests
{
    [TestFixture]
    public class Tests
    {
        private Predicate<int> setForTesting;

        [SetUp]
        public void Setup()
        {
            setForTesting = x => x > 0 && x < 10;
        }

        [Test]
        public void EmptySetTest()
        {
            Predicate<string> set = FunctionalSet.Empty<string>();
            Assert.IsTrue(set(null));
        }

        [Test]
        public void SingletonSetTest()
        {
            Predicate<string> set = FunctionalSet.SingletonSet("string");
            Assert.IsTrue(set("string"));
            Assert.IsFalse(set(null));
        }

        [Test]
        public void UnionSetOfTwoSingletones()
        {
            Predicate<float> set = FunctionalSet.Union(
                FunctionalSet.SingletonSet(6.66f),
                FunctionalSet.SingletonSet(3.14f)
                );
            Assert.IsTrue(set(6.66f) && set(3.14f));
            Assert.IsFalse(set(5653f));
        }

        [Test]
        public void IntersectSingletons()
        {
            Predicate<int> set = FunctionalSet.Intersect(
                FunctionalSet.SingletonSet(5),
                FunctionalSet.SingletonSet(6)
                );
            Assert.IsFalse(set(5));
            Assert.IsFalse(set(6));
        }

        [Test]
        public void IntersectSets()
        {
            Predicate<int> set = FunctionalSet.Intersect(setForTesting, x => x > 5 && x < 12);
            Assert.IsTrue(set(6) && set(9));
            Assert.IsFalse(set(12));
        }

        [Test]
        public void DiffSets()
        {
            Predicate<int> set = FunctionalSet.Diff( x => x > -5 && x < 5, setForTesting);
            Assert.IsTrue(set(-4));
            Assert.IsFalse(set(1));
            Assert.IsFalse(set(7));
        }

        [Test]
        public void FilterSet()
        {
            Predicate<int> set = FunctionalSet.Filter(setForTesting, x => x > 5);
            Assert.IsTrue(set(6));
            Assert.IsFalse(set(5));
        }

        [Test]
        public void ForallTrue()
        {
            Assert.IsTrue(SetOfInts.Forall(setForTesting,x=> x > 0));
        }

        [Test]
        public void ForallFalse()
        {
            Assert.IsFalse(SetOfInts.Forall(setForTesting, x => x > 2 && x < 5));
        }

        [Test]

        public void ExistTrue()
        {
            Assert.IsTrue(SetOfInts.Exists(setForTesting, x => x > 0 && x < 3));
        }

        public void ExistFalse()
        {
            Assert.IsFalse(SetOfInts.Exists(setForTesting, x => x > 10 && x < 0));
        }

        public void MapTest()
        {
            int[] arr = {1, 4, 9, 16, 25, 36, 49, 64, 81 };
            Predicate<int> set = SetOfInts.Map(setForTesting, x => x * x);
            foreach (int item in arr)
            {
                Assert.IsTrue(set(item));
            }
        }
    }
}