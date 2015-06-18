using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Akka.Actor;
using Messages.FundValidationService.List;

namespace SbrValidation.Worker.FVS
{
    public class FVSListBulkSplitActor : ReceiveActor
    {
        public FVSListBulkSplitActor()
        {
            Ready();
        }

        private void Ready()
        {
            Receive<BulkListProcessRequest>(request => 
            {
                // split data and return each message
                List<string> parts = new List<string>();

                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(request.Data);
                writer.Flush();
                stream.Position = 0;

                var message = MimeKit.MimeMessage.Load(stream);
                string body;
                foreach (var part in message.Attachments)
                {
                    StreamReader reader = new StreamReader(part.ContentObject.Open());
                    body = reader.ReadToEnd();
                    Regex re = new Regex("<Record_Delimiter DocumentID=\"\\d*\" DocumentName=\"FVS\" DocumentType=\"CHILD\" RelatedDocumentID=\"\\d*\"/>");
                    parts = re.Split(body).Skip(1).ToList();
                }

                BulkListSplitResult result = new BulkListSplitResult(parts.ToList());
                Sender.Tell(result);
            });
        }

    }
}
