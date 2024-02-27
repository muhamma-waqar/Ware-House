using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class MyEntity : IEntity, ISoftDeletable, IAudited
    {
        public int Id { get; private set; }

        public string? DeleteBy { get; private set; }

        public DateTime? DeleteAt {  get; private set; }

        public string CreatedBy { get; private set; } = null!;

        public DateTime CreatedAt {  get; private set; }

        public string? LastModifiedBy {  get; private set; }

        public DateTime? LastModifiedAt {  get; private set; }
    }
}
