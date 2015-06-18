using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Akka.Actor;
using JeffFerguson.Gepsio;
using Messages.FundValidationService;
using Messages.FundValidationService.List;
using Newtonsoft.Json.Linq;

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
                XbrlDocument doc = new XbrlDocument();
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(map.Payload);
                writer.Flush();
                stream.Position = 0;
                doc.Load(stream);

                //JObject jsonJObject = new JObject();
                //jsonJObject.
            });
        }
    }
}
