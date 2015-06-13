using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Akka.Actor;
using Messages.FundValidationService.List;

namespace SbrValidation.Worker.FVS
{
    public class FVSListValidationActor : ReceiveActor
    {
        Random r = new Random();
        public FVSListValidationActor()
        {
            Ready();
        }

        private void Ready()
        {
            Receive<TaxonomyValidate>(validate => 
            {
                //Sender.Tell(new TaxonomyValidateResponse(r.Next(100) > 50 ? true : false, new List<string>()));
                Sender.Tell(new TaxonomyValidateResponse(true, null));
            });
        }
    }
}
