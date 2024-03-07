using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication.Core.Model
{
    public enum MySingInResult
    {
        Failed,
        Success,
        LockedOut,
        NotAllowed
    }
}
