﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Routing;
using Messages.FundValidationService.List;

namespace SbrValidation.Worker.FVS
{
    public class FVSListCommanderActor : ReceiveActor, IWithUnboundedStash
    {
        IActorRef splitActor;
        IActorRef coordinator;

        private List<string> results;
        private int resultCount;

        public IStash Stash { get; set; }

        public FVSListCommanderActor()
        {
            results = new List<string>();
            Ready();
        }

        protected override void PreStart()
        {
            splitActor = Context.ActorOf(Props.Create(() => new FVSListBulkSplitActor()),"split");
            coordinator = Context.ActorOf(Props.Create(() => new FVSListCoordinatorActor(Self)).WithRouter(new RoundRobinPool(1)), "coordinator");
        }

        private void Ready()
        {
            Receive<BulkListProcessRequest>(request => 
            {
                splitActor.Tell(request);
            });

            Receive<BulkListSplitResult>(result =>
            {
                Console.WriteLine("Found {0} parts in message", result.Parts.Count);
                foreach (var r in result.Parts)
                {
                    coordinator.Tell(new SingleProcessRequest(r));
                }
            });

            Receive<SingleProcessResponse>(processResponse =>
            {
                Console.WriteLine(processResponse.MappingResult != null ? processResponse.MappingResult.CanonicalModel.Root.Value : "empty mapping result");
            });
        }
    }
}
