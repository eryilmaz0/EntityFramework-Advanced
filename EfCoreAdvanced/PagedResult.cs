using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCoreAdvanced
{
    public class PagedResult<T>
    {
        public int TotalCount { get; init; }
        public List<T> Result { get; init; }
    }
}
