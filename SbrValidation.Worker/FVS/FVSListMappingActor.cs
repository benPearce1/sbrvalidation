using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Akka.Actor;
using Messages.FundValidationService;
using Messages.FundValidationService.List;

namespace SbrValidation.Worker.FVS
{
    public class FVSListMappingActor : ReceiveActor
    {
        public FVSListMappingActor()
        {
            Ready();
        }

        private void Ready()
        {
            Receive<MapToCanonical>(map =>
            {
                var value = new XElement("value");
                value.SetValue(map.Payload);
                Sender.Tell(new MapToCanonicalResponse(new XDocument(new XElement("root", value))));
            });
        }
    }
}
