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
    public class ResponseService : IResponseService
    {
        private readonly TrendyolDbContext _context;
        public ResponseService(TrendyolDbContext context)
        {
            _context = context;
        }

        public void Add(Response response)
        {
            _context.Responses.Add(response);
            _context.SaveChanges();
        }
    }
}
