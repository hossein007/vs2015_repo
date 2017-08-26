using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevelopersHub.Models
{
    public class DevelopersHubRepository<T> : GenericRepository<DevelopersHubContext,T> 
        where T : class
    {
    }
}
