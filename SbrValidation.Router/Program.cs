using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka;
using Akka.Actor;

namespace SbrValidation.Router
{
    class Program
    {
        public static ActorSystem RouterActorSystem;

        static void Main(string[] args)
        {
            RouterActorSystem = ActorSystem.Create("router");
        }

    }
}
