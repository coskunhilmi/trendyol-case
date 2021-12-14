using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Domains
{
    public class Request : IEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
        public string Content { get; set; }
        public virtual Response Response { get; set; }

    }
}
