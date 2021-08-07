using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdekaDestek.DataAccess.Abstract;
using AdekaDestek.HelpDeskAPI.DataAccess.Abstract;

namespace AdekaDestek.HelpDeskAPI.Concrete
{
    public class ManagerBase
    {
        protected IUnitOfWork UnitOfWork { get; }
      

        public ManagerBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
           
        }
    }
}
