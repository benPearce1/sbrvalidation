using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka;
using Akka.Actor;
using SbrValidation.Worker.FVS;

namespace SbrValidation.Router
{
    class Program
    {
        public static ActorSystem RouterActorSystem;
        static IActorRef commander;

        static void Main(string[] args)
        {
            RouterActorSystem = ActorSystem.Create("router");
            commander = RouterActorSystem.ActorOf(Props.Create(() => new FVSListCommanderActor()),"commander");
            var data = new List<string>();
            Enumerable.Range(0, 20).ToList().ForEach(x => data.Add(string.Format("document data {0}", x)));
            commander.Tell(new Messages.FundValidationService.List.BulkListProcessRequest(string.Join("<DocumentSeperator>", data.ToArray())));
            RouterActorSystem.WaitForShutdown();
        }

    }
}
