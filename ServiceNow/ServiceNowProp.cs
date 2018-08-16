using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceNow
{
    public class ServiceNowProp
    {
        public ServiceNowProp(string v1, string v2, string v3)
        {
            this.SnowInstance = v1;
            this.UserName = v2;
            this.Password = v3;
        }

        public String SnowInstance { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
    }
}
