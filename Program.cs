using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using Topshelf;
namespace mywinodwservice
{
     class Program
    {
       
            static void Main(String[] args)
            {
            var exitcode = HostFactory.Run(x => 
             {
                x.Service<heartbeat>(s =>
                {
                    s.ConstructUsing(heartbeat => new heartbeat());
                    s.WhenStarted(heartbeat=>heartbeat.OnStart());
                    s.WhenStopped(heartbeat=>heartbeat.OnStop());
                });

                x.RunAsLocalSystem();
                x.SetServiceName("inlandservice");
                x.SetDisplayName("inland Service");
                x.SetDescription("This service automatically send data to file");
            });

            int exitcodevalue = (int)Convert.ChangeType(exitcode, exitcode.GetTypeCode());
            Environment.ExitCode = exitcodevalue; 
            }
        
    }
}  
