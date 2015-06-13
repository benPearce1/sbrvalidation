using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.FundValidationService.List
{
    public class SingleProcessResponse
    {
        public SingleProcessResponse(TaxonomyValidateResponse validateResult, MapToCanonicalResponse mappingResult)
        {
            this.TaxonomyValidateResult = validateResult;
            this.MappingResult = mappingResult;
        }

        public TaxonomyValidateResponse TaxonomyValidateResult { get; private set; }

        public MapToCanonicalResponse MappingResult { get; set; }

    }
}
