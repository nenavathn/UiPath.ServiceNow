using System;
using System.Activities;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ActivityXamlServicesSettings settings = new ActivityXamlServicesSettings
            {
                CompileExpressions = true
            };

            Activity workflow = ActivityXamlServices.Load("Activity1.xaml", settings);
            WorkflowInvoker.Invoke(workflow);

            Console.WriteLine("Press <enter> to exit" + System.Environment.CurrentDirectory);
            Console.ReadLine();
        }
    }
}
