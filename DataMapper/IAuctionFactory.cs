﻿using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public interface IAuctionFactory
    {
        void AddNewAuction(Auction auction,User user);
    }
}
