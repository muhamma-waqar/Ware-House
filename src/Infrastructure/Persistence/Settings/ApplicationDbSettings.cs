using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Settings
{
    class ApplicationDbSettings
    {
        [Required]
        public bool? AutoMigrate { get; init; }

        [Required] 
        public bool? AutoSeed { get; init; }
    }
}
