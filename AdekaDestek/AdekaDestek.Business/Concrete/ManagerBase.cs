using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgrammersBlog.DataAccess.Abstract;

namespace AdekaDestek.Business.Concrete
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
