using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModel
{
    public class ValidationError
    {
        public string filed { get; set; } = default!;
        public IEnumerable<string> Errors { get; set; } = [];
    }
}
