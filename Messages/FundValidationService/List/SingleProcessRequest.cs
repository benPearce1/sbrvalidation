using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.FundValidationService.List
{
    public class SingleProcessRequest
    {
        public SingleProcessRequest(string data)
        {
            Payload = data;
        }

        public string Payload { get; private set; }
    }
}
