using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.FundValidationService.List
{
    public class BulkListProcessComplete
    {
        public IList<SingleProcessResponse> Results { get; private set; }

        public BulkListProcessComplete(IList<SingleProcessResponse> results )
        {
            Results = results;
        }
    }
}
