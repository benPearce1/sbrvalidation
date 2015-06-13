using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Messages.FundValidationService.List;

namespace SbrValidation.Worker.FVS
{
    public class FVSListCoordinatorActor : ReceiveActor, IWithUnboundedStash
    {
        private string currentPayload;
        //private IActorRef validationActor;
        private IActorRef mappingActor;
        //private TaxonomyValidateResponse validateResult = null;
        private MapToCanonicalResponse mappingResult = null;
        private IActorRef commander;

        protected override void PreStart()
        {
            //validationActor = Context.ActorOf(Props.Create(() => new FVSListValidationActor()));
            mappingActor = Context.ActorOf(Props.Create(() => new FVSListMappingActor()));
        }

        public FVSListCoordinatorActor(IActorRef commander)
        {
            this.commander = commander;
            Ready();
            Console.WriteLine("Creating new coordinator");
        }

        private void Ready()
        {
            // entry point for validation and mapping
            Receive<SingleProcessRequest>(validation =>
            {
                //BecomeProcessing();
                currentPayload = validation.Payload;
                Console.WriteLine("Received payload: {0}", currentPayload);
                //validationActor.Tell(new TaxonomyValidate(validation.Payload));
                mappingActor.Tell(new MapToCanonical(validation.Payload));
            });

            // receive validation result
            //Receive<TaxonomyValidateResponse>(validationResult =>
            //{
            //    this.validateResult = validationResult;
            //    ProcessResult();
            //});

            // receive mapping result
            Receive<MapToCanonicalResponse>(mappingResult =>
            {
                this.mappingResult = mappingResult;
                //ProcessResult();
            });
        }

        private void BecomeProcessing()
        {
            Become(Processing);
        }

        private void Processing()
        {
            Receive<SingleProcessRequest>(spr => Stash.Stash());

            // receive validation result
            //Receive<TaxonomyValidateResponse>(validationResult =>
            //{
            //    this.validateResult = validationResult;
            //    ProcessResult();
            //});

            // receive mapping result
            Receive<MapToCanonicalResponse>(mappingResult =>
            {
                this.mappingResult = mappingResult;
                //ProcessResult();
                commander.Tell(new SingleProcessResponse(null, mappingResult));
            });
        }

        //private void ProcessResult()
        //{
        //    if (validateResult != null && validateResult.Result == false)
        //    {
        //        var response = new SingleProcessResponse(validateResult, null);
        //        Console.WriteLine("Sending response: Validation result {0}", validateResult.Result);
        //        mappingActor.Tell(PoisonPill.Instance);
        //        commander.Tell(response);
        //        Become(Ready);
        //        //Stash.UnstashAll();
        //    }
        //    else if (validateResult != null && mappingResult != null)
        //    {
        //        var response = new SingleProcessResponse(validateResult, mappingResult);
        //        Console.WriteLine("Sending response: Validation result {0}, Mapping Result: {1}", validateResult.Result, mappingResult.CanonicalModel.Root.Value);
        //        commander.Tell(response);
        //        Become(Ready);
        //        //Stash.UnstashAll();
        //    }

        //}

        public IStash Stash { get; set; }

    }
}
