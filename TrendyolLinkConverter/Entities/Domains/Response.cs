using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Domains
{
    public class Response : IEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
        public string Content { get; set; }
        public Nullable<int> RequestId { get; set; }
        public virtual Request Request { get; set; }
    }
}
