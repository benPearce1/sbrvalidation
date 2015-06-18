using System;
using System.Collections.Generic;
using System.IO;
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
            var data = File.ReadAllText(@"C:\Users\Ben\Downloads\sean_20150424_1319_pull_receive_payload.dat");

            commander.Tell(new Messages.FundValidationService.List.BulkListProcessRequest(data));
            RouterActorSystem.WaitForShutdown();
        }

    }
}
