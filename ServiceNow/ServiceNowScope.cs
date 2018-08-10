using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Activities.Statements;

namespace ServiceNow
{
    [Designer(typeof(ServiceNowScopeDesigner))]
    [DisplayName("ServiceNow Connector Scope")]
    [Description("Drop ServiceNow related activities inside this scope.")]
    public class ServiceNowScope : NativeActivity
    {
        [Browsable(false)]
        public ActivityAction Body { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<String> SnowInstance { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<String> UserName { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<String> Password { get; set; }

        public ServiceNowScope()
        {
            Body = new ActivityAction
            {
                //Argument = new DelegateInArgument<SshClient>("SSHClient"),
                Handler = new Sequence { DisplayName = "Execute SNOW activities" }
            };
        }

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);

            if (UserName == null  | Password == null)
            {
                metadata.AddValidationError("UserName and Password required");
            }

        }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.


        protected override void Execute(NativeActivityContext context)
        {
            Console.WriteLine("Scope Executed");
        }
    }
}
