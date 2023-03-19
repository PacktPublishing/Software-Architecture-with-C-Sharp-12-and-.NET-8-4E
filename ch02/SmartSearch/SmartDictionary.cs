using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartSearch
{
    public class SmartDictionary<T>
    {
        private List<T> allItems;
        private Func<T, string> keyAccessor;
        protected class Rater
        {
            public T Item;
            public double Penalty=0;
            public int FoundChars=0;
        }
        public SmartDictionary(Func<T, string> keyAccessor, IEnumerable<T> allItems)
        {
            if (allItems == null) throw new ArgumentNullException(nameof(allItems));
            this.keyAccessor = keyAccessor ?? throw new ArgumentNullException(nameof(keyAccessor));
            this.allItems = allItems.ToList();
        }
        public void Replace(IEnumerable<T> newItems)
        {
            if (newItems == null) throw new ArgumentNullException(nameof(newItems));
            allItems = newItems.ToList();
        }
        public void Add(T newItem)
        {
            if (newItem == null) throw new ArgumentNullException(nameof(newItem));
            allItems.Add(newItem);
        }
        protected Rater[] Rate(string s)
        {
            Rater[] result = new Rater[allItems.Count];
            for(int i=0; i < result.Length; i++)
            {
                result[i] = RateItem(s, new Rater
                {
                    Item = allItems[i]
                });
            }
            return result;
        }
        protected Rater RateItem(string search, Rater x)
        {
            var toSearch = search.ToLower();
            var destination = keyAccessor(x.Item).ToLower();
            bool firstMatch = true;
            for (var j = 0; j < toSearch.Length; j++)
            {
                if (destination == string.Empty) return x;
                var currChar = toSearch[j];
                var index = destination.IndexOf(currChar);
                if (index == -1) continue;
                x.FoundChars++;
                if (firstMatch)
                {
                    x.Penalty += index;
                    firstMatch = false;
                
                }
                else x.Penalty += index*1000;

                if (index + 1 < destination.Length)
                    destination = destination.Substring(index + 1);
                else
                    destination = string.Empty;
            }
            return x;
        }
        public IEnumerable<T> Search(string search, int maxItems)
        {
            if (search == null) throw new ArgumentNullException(nameof(search));
            if (maxItems <=0) throw new ArgumentException($"{nameof(maxItems)} must be greater than zero", nameof(maxItems));
            var pres = Rate(search);
            Array.Sort(pres, new Comparison<Rater>((x, y) => 
            {
                if (x.FoundChars > y.FoundChars) return -1;
                if (x.FoundChars < y.FoundChars) return 1;
                return x.Penalty.CompareTo(y.Penalty);
            }));
            return pres.Take(maxItems).Select(m => m.Item);
        }
    }
}
