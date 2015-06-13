using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Messages.FundValidationService.List
{
    public class MapToCanonicalResponse
    {
        public MapToCanonicalResponse(XDocument doc)
        {
            CanonicalModel = doc;
        }

        public XDocument CanonicalModel { get; private set; }
    }
}
