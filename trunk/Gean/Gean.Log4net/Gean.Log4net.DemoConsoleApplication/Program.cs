using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Log4net.DemoConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Log4netLoggingService log = Log4netLoggingService.Initialize(); 
            log.Debug(Guid.NewGuid());
            log.Error(Guid.NewGuid());
            log.Fatal(Guid.NewGuid());
            log.Info(Guid.NewGuid());
            log.Warn(Guid.NewGuid());
            Console.ReadKey();
            log.Debug(Guid.NewGuid());
            log.Error(Guid.NewGuid());
            log.Fatal(Guid.NewGuid());
            log.Info(Guid.NewGuid());
            log.Warn(Guid.NewGuid());
            Console.ReadKey();
            log.Debug(Guid.NewGuid());
            log.Error(Guid.NewGuid());
            log.Fatal(Guid.NewGuid());
            log.Info(Guid.NewGuid());
            log.Warn(Guid.NewGuid());
            Console.ReadKey();
            log.Debug(Guid.NewGuid());
            log.Error(Guid.NewGuid());
            log.Fatal(Guid.NewGuid());
            log.Info(Guid.NewGuid());
            log.Warn(Guid.NewGuid());
            Console.ReadKey();
        }
    }
}
