using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.FundValidationService.List
{
    public class BulkListProcessRequest
    {
        public BulkListProcessRequest(string data)
        {
            this.Data = data;
        }

        public string Data { get; private set; }
    }
}
