using MyHashTable;

namespace TestMyHashTable
{
    [TestClass]
    public class UnitTestMyHashTable
    {
        const int n = 10000;

        [TestMethod]
        public void TestCountEqualZeroAfterInit()
        {
            MyHashTable<string, int> myHashTable = new MyHashTable<string, int>();
            Assert.AreEqual(0, myHashTable.Count);
        }

        [TestMethod]
        public void TestCountAfterAddedIncrement()
        {
            MyHashTable<string, int> myHashTable = new MyHashTable<string, int>();
            myHashTable.Add("123", 123);
            Assert.AreEqual(1, myHashTable.Count);
        }

        [TestMethod]
        public void TestCountEqualZeroAfterAddedAndRemove()
        {
            MyHashTable<string, int> myHashTable = new MyHashTable<string, int>();
            myHashTable.Add("123", 123);
            myHashTable.Remove("123");
            Assert.AreEqual(0, myHashTable.Count);
        }

        [TestMethod]
        public void TestAddCountEqualCountAddedElements()
        {
            MyHashTable<string, int> myHashTable = new MyHashTable<string, int>();
            for (int i = 0; i < n; i++)
            {
                myHashTable.Add(i.ToString(), i);
            }
            Assert.AreEqual(n, myHashTable.Count);
        }

        [TestMethod]
        public void TestCountEqualZeroAfterAddedAndRemoveSomeElements()
        {
            MyHashTable<string, int> myHashTable = new MyHashTable<string, int>();
            for (int i = 0; i < n; i++)
            {
                myHashTable.Add(i.ToString(), i);
            }
            for (int i = 0; i < n; i++)
            {
                myHashTable.Remove(i.ToString());
            }
            
            Assert.AreEqual(0, myHashTable.Count);
        }

        [TestMethod]
        public void TestGetElementsValuesEqualsAddedValues()
        {
            MyHashTable<string, int> myHashTable = new MyHashTable<string, int>();
            for (int i = 0; i < n; i++)
            {
                myHashTable.Add(i.ToString(), i);
            }
            int GuessedValues = 0;
            for (int i = 0; i < n; i++)
            {
                int value = myHashTable.GetValue(i.ToString());
                if (value == i)
                    GuessedValues++;
            }

            Assert.AreEqual(n, GuessedValues);
        }

        [TestMethod]
        public void TestGetValueOrDefaultGettedAddedItem()
        {
            MyHashTable<string, int> myHashTable = new MyHashTable<string, int>();
            for (int i = 0; i < n; i++)
            {
                myHashTable.Add(i.ToString(), i);
            }
            int GuessedValues = 0;
            for (int i = 0; i < n; i++)
            {
                int value = myHashTable.GetValueOrDefault(i.ToString(), -1);
                if (value == i)
                    GuessedValues++;
            }

            Assert.AreEqual(n, GuessedValues);
        }

        [TestMethod]
        public void TestGetValueOrDefaultGettedDefault()
        {
            MyHashTable<string, int> myHashTable = new MyHashTable<string, int>();
            int GuessedValues = 0;
            for (int i = 0; i < n; i++)
            {
                int value = myHashTable.GetValueOrDefault(i.ToString(), -1);
                if (value == -1)
                    GuessedValues++;
            }

            Assert.AreEqual(n, GuessedValues);
        }

        [TestMethod]
        public void TestSetValueByKeySettedRightValue()
        {
            MyHashTable<string, int> myHashTable = new MyHashTable<string, int>();
            myHashTable.SetValueByKey("123", 123);
            Assert.AreEqual(123, myHashTable["123"]);
        }

        [TestMethod]
        public void TestSetValueByKeySettedUpdateOldItem()
        {
            MyHashTable<string, int> myHashTable = new MyHashTable<string, int>();
            myHashTable.SetValueByKey("123", 123);
            myHashTable.SetValueByKey("123", 456);
            Assert.AreEqual(456, myHashTable["123"]);
        }
    }
}