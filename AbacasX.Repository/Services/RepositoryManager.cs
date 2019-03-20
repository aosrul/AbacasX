using AbacasX.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Repository.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    class RepositoryManager : IRepositoryService
    {

    }
}
