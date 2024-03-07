using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication.External.Exceptions
{
    public class ExternalAuthenticationPreventedException : Exception
    {
        public ExternalAuthenticationPreventedException(Exception innerExcepiton) : base($"Could not succesfully execute authenticaiton check with external provider. Mybe their service are not accessible currntly.", innerExcepiton) { }
    }
}
