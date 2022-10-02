using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazniva_krizovatka.Tree
{
    public interface ICustomComparable<T>
    {
        int Compare(ICustomComparable<T> obj);
        T GetData();

    }
}
