using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerJS.DataAccess
{
    public interface IContext : IDisposable
    {
        int SaveChanges();
    }
}
