using System.Collections;

namespace GenericList
{
    public class GenericList<T> : IList<T>, ICollection<T>, IEnumerable<T>
    {
        #region properties

        private T[] items;
        public int Count { get; private set; }
        public bool IsReadOnly => false;
        private const int Capacity = 4;
        IEqualityComparer<T> Comparer;
        Stack<T> stack;

        #endregion

        #region Constructors ans indexer
        public GenericList()
        {
            items = new T[Capacity];
            Count = 0;
            Comparer = EqualityComparer<T>.Default;
            stack = new Stack<T>();
        }
        public GenericList(IEqualityComparer<T> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            items = new T[Capacity];
            Count = 0;
            Comparer = comparer;
            stack = new Stack<T>();
        }
        public GenericList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException("must cpacity non-nagation", nameof(capacity));
            items = new T[capacity];
            Count = 0;
            Comparer = EqualityComparer<T>.Default;
            stack = new Stack<T>();
        }
        public GenericList(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            Comparer = EqualityComparer<T>.Default;
            stack = new Stack<T>();
            items = new T[GetCountOfCollection(collection)];
            AddRange(collection);

        }
        // access spacific index
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException(nameof(index));
                return items[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException(nameof(index));
                items[index] = value;

            }

        }
        //return  array of values for intery indexes
        public T[] this[string s_indexs]
        {
            get
            {
                if (string.IsNullOrEmpty(s_indexs))
                    throw new ArgumentException("not valid indexs string");
                if (!IsValidIndexesString(s_indexs))
                    throw new ArgumentException("not valid indexs  ");

                string[] indexs = s_indexs.Split(",");
                T[] values = new T[indexs.Length];
                int index;
                for (int i = 0; i < indexs.Length; i++)
                {
                    index = Convert.ToInt32(indexs[i]);
                    if (index < 0 || index >= Count)
                        throw new IndexOutOfRangeException("index not valid out of range.");
                    values[i] = items[index];
                }
                return values;
            }

        }
        //return  array of values for intery indexes ( optimized solution )
        public T[] this[params int[] indexs]
        {
            get
            {
                T[] result = new T[indexs.Length];
                int count = 0;
                foreach (int index in indexs)
                {
                    if (index >= Count || index < 0)
                        throw new IndexOutOfRangeException("index out of rang");
                    result[count++] = items[index];
                }
                return result;
            }
        }
        #endregion

        #region Methods 

        #region Add method
        public void Add(T item)
        {
            if (Count == items.Length)
                Resize(items);
            items[Count++] = item;
        }
        public void AddRange(IEnumerable<T> collection)
        {
            //check if empty list element is less than arr.length ---> resize
            if ((items.Length - Count) < GetCountOfCollection(collection))
                Resize(GetCountOfCollection(collection) + Count);
            //loop on list and add element one-by-one
            foreach (var item in collection)
                items[Count++] = item;
        }
        public void Insert(int index, T item)
        {
            //ckeck if index in range 
            if (index > Count || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));
            //ckeck if list count less than or equal array count
            if (Count >= items.Length)
                Resize(Count * 2);

            //shift right of element 
            ShiftRight(index, 1);
            //insert item 
            items[index] = item;
            //increase Count 
            Count++;
        }
        public void InsertRange(int index, IEnumerable<T> collection)
        {
            int nCollectionCount = GetCountOfCollection(collection);
            //ckeck if index in range 
            if (index > Count || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));
            //ckeck if list count less than or equal array count
            if (nCollectionCount > items.Length - Count)
                Resize(Count + nCollectionCount);
            //shift right of element 
            ShiftRight(index, nCollectionCount);
            //insert item 
            int nCount = index;
            foreach (var item in collection)
            {
                items[nCount++] = item;
                Count++;
            }
        }
        #endregion

        #region Remove
        public void RemoveAt(int index)
        {
            //check element in rang array
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            // for undo feature
            stack.Push(items[index]);
            //shift left element 
            ShiftLeft(index, 1);
            //decrease count and currentindex
            Count--;
        }
        public bool Remove(T value)
        {
            int index = IndexOf(value);
            if (index < 0)
                return false;
            //for undo feature
            stack.Push(value);
            //sheft left of element
            ShiftLeft(index, 1);
            //decrease current and count
            Count--;
            return true;
        }
        public void RemoveRange(int index, int count)
        {
            //check if number of removed element inside array range
            if (count > Count || count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (index >= Count || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (count + index > Count)
                throw new ArgumentOutOfRangeException(nameof(count), nameof(index));
            // loop on number of removed elment and set it by default
            ShiftLeft(index, count);
            int tepCount = Count;
            for (int i = tepCount - count; i < tepCount; i++)
            {
                //for undo feature
                stack.Push(items[i]);
                items[i] = default;
                Count--;
            }

        }
        public void RemoveAll()
        {
            for (int i = 0; i < Count; i++)
            {
                stack.Push(items[i]);
                items[i] = default(T);

            }
            Count = 0;
        }
        public void Clear()
        {
            items = new T[4];
            Count = 0;
        }
        #endregion

        #region Helper Method
        //
        public void Sort()
        {
            Array.Sort(items, 0, Count);
        }
        public int IndexOf(T item)
        {

            //loop on array
            for (int i = 0; i < Count; i++)
            {
                //compare item with array[i] is equal or no 
                if (Comparer.Equals(item, items[i]))
                    return i;//if equal return index
            }
            //else return -1 
            return -1;
        }
        public T[] ToArray()
        {
            T[] result = new T[Count];
            Array.Copy(items, result, Count);
            return result;
        }
        private void ShiftLeft(int index, int numOfItems)
        {
            //shift left element 
            for (int i = index; i < Count - numOfItems; i++)
                items[i] = items[i + numOfItems];
            // remove values
            for (int i = Count - 1; i >= Count - numOfItems; i--)
                items[i] = default;
        }
        private void ShiftRight(int index, int numOfItems)
        {
            for (int i = Count - 1; i >= index; i--)
            {
                items[i + numOfItems] = items[i];
            }
            for (int i = index; i < index + numOfItems; i++)
            {
                items[i] = default(T);
            }
        }
        public void Reverse()
        {
            if (Count == 0)
            {
                return;
            }
            T temp;
            for (int i = 0; i < Count / 2; i++)
            {
                temp = items[i];
                items[i] = items[Count - i - 1];
                items[Count - i - 1] = temp;

            }
        }
        // we can use params with array of int
        private bool IsValidIndexesString(string String)
        {

            string[] indexs = String.Split(",");
            foreach (string s in indexs)
            {
                if (!int.TryParse(s, out _))
                    return false;
            }
            return true;
        }
        private void Resize(IEnumerable<T> collection)
        {
            //get count of collection
            int nCountOfCollection = collection.Count();
            //size of new list 
            int newSize = (nCountOfCollection == 0) ? 4 : nCountOfCollection * 2;
            //declare resized array
            T[] resizedArray = new T[newSize];
            Array.Copy(items, resizedArray, Count);
            //loop on collection to copy it values inside list
            int nCount = Count;
            foreach (T item in collection)
            {
                resizedArray[nCount++] = item;
            }
            items = resizedArray;

        }
        private void Resize(int newSize)
        {
            //create new array of new size
            T[] resizedArray = new T[newSize];
            // copy for previous array
            Array.Copy(items, resizedArray, Count);
            //make the main array equal new array
            items = resizedArray;
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return items[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public bool Contains(T item)
        {
            return IndexOf(item) > -1;
        }
        //
        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(items, 0, array, arrayIndex, Count);
        }
        public int GetCountOfCollection(IEnumerable<T> collection)
        {
            int count = 0;
            foreach (T item in collection)
                count++;
            return count;
        }

        public void Undo()
        {
            if (stack.Count > 0)
            {
                T item = stack.Pop();
                Add(item);
            }
        }

        #endregion

        #endregion


    }
}
