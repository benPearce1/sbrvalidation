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
        private IActorRef validationActor;
        private IActorRef mappingActor;
        private IActorRef commander;

        public FVSListCoordinatorActor(IActorRef commander)
        {
            this.commander = commander;
            Console.WriteLine("Creating new coordinator");
            Ready();
        }

        protected override void PreStart()
        {
            validationActor = Context.ActorOf(Props.Create(() => new FVSListValidationActor()));
            mappingActor = Context.ActorOf(Props.Create(() => new FVSListMappingActor()));
        }

        private void Ready()
        {
            // entry point for validation and mapping
            Receive<SingleProcessRequest>(request =>
            {
                //BecomeProcessing();
                Console.WriteLine("Received payload: {0}", request.Payload);
                var t = Task.Run(async () =>
                {
                    var validationTask = validationActor.Ask(new TaxonomyValidate(request.Payload));
                    var mappingTask = mappingActor.Ask(new MapToCanonical(request.Payload));

                    await Task.WhenAll(validationTask, mappingTask);

                    var response = new SingleProcessResponse(validationTask.Result as TaxonomyValidateResponse, mappingTask.Result as MapToCanonicalResponse);
                    commander.Tell(response);
                });
            });
        }

        public IStash Stash { get; set; }

    }
}
