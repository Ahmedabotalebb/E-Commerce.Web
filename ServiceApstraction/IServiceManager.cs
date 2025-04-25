using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApstraction
{
    public interface IServiceManager
    {
        public IProductService productService { get; }
    }
}
