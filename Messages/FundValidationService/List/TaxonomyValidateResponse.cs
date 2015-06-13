using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.FundValidationService.List
{
    public class TaxonomyValidateResponse
    {
        public TaxonomyValidateResponse(bool result, IList<string> validations)
        {
            Result = result;
            Validations = validations;
        }

        public bool Result { get; private set; }
        public IList<string> Validations { get; private set; }
    }
}
