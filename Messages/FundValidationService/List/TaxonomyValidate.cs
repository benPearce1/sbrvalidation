using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.FundValidationService.List
{
    public class TaxonomyValidate
    {
        public TaxonomyValidate(string payload)
        {
            Payload = payload;
        }

        public string Payload { get; private set; }
    }
}
