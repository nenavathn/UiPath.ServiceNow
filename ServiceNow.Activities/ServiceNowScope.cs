using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Activities.Statements;
using System.Security;
using System.Net;

namespace ServiceNow
{
    [Designer(typeof(ServiceNowScopeDesigner))]
    [DisplayName("ServiceNow Connector Scope")]
    [Description("Drop ServiceNow related activities inside this scope.")]
    public class ServiceNowScope : NativeActivity
    {
        [Browsable(false)]
        public ActivityAction<ServiceNowProp> Body { get; set; }

        [Category("Input")]
        
        [RequiredArgument]
        [Description("Name of the ServiceNow URI. https://example.service-now.com")]
        [DisplayName("SNOW Base URL")]
        public InArgument<String> SnowBaseURL { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<String> UserName { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<SecureString> Password { get; set; }

        [Browsable(false)]
        public ServiceNowProp snowDetails;

        public ServiceNowScope()
        {
            Body = new ActivityAction<ServiceNowProp>
            {
                Argument = new DelegateInArgument<ServiceNowProp>("snowDetails"),
                Handler = new Sequence { DisplayName = "Execute SNOW activities" }
            };
        }

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);

            //if (SnowInstance == null)
            //{
            //    metadata.AddValidationError("SnowInstance URL is required");
            //}

            //if (UserName == null)
            //{
            //    metadata.AddValidationError("UserName is required");
            //}

            //if (Password == null)
            //{
            //    metadata.AddValidationError("Password is required");
            //}
        }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.


        protected override void Execute(NativeActivityContext context)
        {

            snowDetails = new ServiceNowProp(SnowBaseURL.Get(context), UserName.Get(context), new NetworkCredential(String.Empty, Password.Get(context)).Password);


            if (Body != null)
            {
                //scheduling the execution of the child activities
                // and passing the value of the delegate argument
                context.ScheduleAction<ServiceNowProp>(Body, snowDetails, OnCompleted, OnFaulted);
            }
            //Console.WriteLine("Scope Executed");
        }

        private void OnFaulted(NativeActivityFaultContext faultContext, Exception propagatedException, ActivityInstance propagatedFrom)
        {
            //TODO
        }

        private void OnCompleted(NativeActivityContext context, ActivityInstance completedInstance)
        {
            //TODO
        }
    }
}
