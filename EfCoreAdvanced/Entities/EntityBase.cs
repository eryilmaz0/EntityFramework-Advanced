using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCoreAdvanced.Entities
{
    public class EntityBase<T>
    {
        public T Id { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
    }
}
