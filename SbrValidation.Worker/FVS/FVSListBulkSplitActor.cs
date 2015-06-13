using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

                parts = request.Data.Split(new string[] {"<DocumentSeperator>"}, StringSplitOptions.RemoveEmptyEntries).ToList();

                BulkListSplitResult result = new BulkListSplitResult(parts.ToList());
                Sender.Tell(result);
            });
        }

    }
}
