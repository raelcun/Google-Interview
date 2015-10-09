using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google_Interview.sorting
{
    public interface ISort<T> where T : IComparable
    {
        void Sort(IList<T> list);
    }
}
