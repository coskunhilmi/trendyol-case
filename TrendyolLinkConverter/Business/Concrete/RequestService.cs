using Business.Abstract;
using DataAccess.EntityFramework.Contexts;
using Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RequestService : IRequestService
    {
        private readonly TrendyolDbContext _context;
        public RequestService(TrendyolDbContext context)
        {
            _context = context;
        }

        public void Add(Request request)
        {
            _context.Requests.Add(request);
            _context.SaveChanges();
        }
    }
}
