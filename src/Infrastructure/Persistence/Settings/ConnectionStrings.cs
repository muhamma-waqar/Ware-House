using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Settings
{
    public class ConnectionStrings
    {
        [Required, MinLength(1)]
        public string DefaultConnection { get; init; } = null!;
    }
}
