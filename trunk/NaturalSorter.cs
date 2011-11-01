//
// NaturalSorter.cs
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CNaturalSorter
    {
        private class CEnumerableComparer<T> 
            : IComparer<IEnumerable<T>>
        {
            private IComparer<T> Comparer { get; set; }

            public CEnumerableComparer()
            {
                Comparer = Comparer<T>.Default;
            }

            public CEnumerableComparer(IComparer<T> comparer)
            {
                Comparer = comparer;
            }

            public int Compare(IEnumerable<T> x, IEnumerable<T> y)
            {
                using (IEnumerator<T> lhs = x.GetEnumerator())
                using (IEnumerator<T> rhs = y.GetEnumerator())
                {
                    while (true)
                    {
                        bool l = lhs.MoveNext();
                        bool r = rhs.MoveNext();

                        if (!l && !r)
                            return 0;

                        if (!l)
                            return -1;

                        if (!r)
                            return 1;

                        int result = Comparer.Compare(lhs.Current, rhs.Current);
                        if (result != 0)
                            return result;
                    }
                }
            }
        }

        public static List<string> NaturalSort(List<string> strings)
        {
            List<string> result = new List<string>(strings);
            result.Sort();
            return result;

            Func<string, object> convert = str => {
                try
                {
                    return int.Parse(str);
                }
                catch
                {
                    return str;
                }
            };

            var sorted = strings.OrderBy(
                str => Regex.Split(str.Replace(" ", ""), "([0-9]+)").Select(convert),
                new CEnumerableComparer<object>());

            return sorted.ToList();
        }
    }
}
