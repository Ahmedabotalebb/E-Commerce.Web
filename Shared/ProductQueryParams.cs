using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParams
    {
        private const int Defaultpagesize = 5;
        private const int maxpageSize = 10;
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSortingOptions sortingOptions { get; set; }
        public string?  SearchValue { get; set; }
        public int PageIndex { get; set; } = 1;
        private int pagesize = Defaultpagesize;

        public int PageSize
        {
            get { return PageSize; }
            set { PageSize = value > maxpageSize ? maxpageSize : PageSize; }
        }

    }
}
