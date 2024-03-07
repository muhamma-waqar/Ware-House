using Infrastructure.Authentication.External.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication.External.Exceptions
{
    public class ExternalAuthenticaitonInfoException : Exception
    {
        public readonly ExternalUserData? ReceivedData;
        public readonly IEnumerable<string>? MissingFields;

        public ExternalAuthenticaitonInfoException(IEnumerable<string>? missingFields = null, ExternalUserData? receivedData = null):base($"External authentication yielded insufficient information to allow local login.")
        {
            this.ReceivedData = receivedData;
            this.MissingFields = missingFields;
        }
    }
}
