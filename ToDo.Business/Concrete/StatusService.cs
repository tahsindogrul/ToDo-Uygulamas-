using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Business.Abstract;
using ToDo.Business.Shared.Concrete;
using ToDo.Data.Repository.Shared.Absract;
using ToDo.Models;

namespace ToDo.Business.Concrete
{
    public class StatusService : Service<Status>, IStatusService
    {
        public StatusService(IRepository<Status> repo) : base(repo)
        {
        }
    }
}
