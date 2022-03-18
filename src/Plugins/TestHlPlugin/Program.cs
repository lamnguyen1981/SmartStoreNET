using SmartStore.Core.Infrastructure;
using System;

namespace TestHlPlugin
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = EngineContext.Initialize(false);
            Console.WriteLine("Hello World!");
        }
    }
}
