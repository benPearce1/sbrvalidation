using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.FundValidationService.List
{
    public class MapToCanonical
    {
        public MapToCanonical(string payload)
        {
            this.Payload = payload;
        }
        public string Payload { get; private set; }
    }
}
