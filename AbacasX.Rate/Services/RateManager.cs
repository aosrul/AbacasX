﻿using AbacasX.Rate.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Rate.Services
{
    
    public class TokenPairRate
    {

    }


    public class CurrencyPairRate
    {

    }


    [ServiceBehavior(UseSynchronizationContext = false, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    class RateManager : IRateService
    {
        

    }
}
