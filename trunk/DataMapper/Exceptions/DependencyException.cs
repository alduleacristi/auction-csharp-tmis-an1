﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Exceptions
{
    public class DependencyException : AuctionException
    {
        public DependencyException():base()
        {
        }

        public DependencyException(String message):base(message)
        {
        }

        public DependencyException(String message, Exception inner)
            : base(message, inner)
        {
        }
    }
}