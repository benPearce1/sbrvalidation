using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.FundValidationService.List
{
    public class BulkListSplitResult
    {
        public BulkListSplitResult(IList<string> parts)
        {
            Parts = parts;
        }

        public IList<string> Parts { get; private set; }
    }
}
