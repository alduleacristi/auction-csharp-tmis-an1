using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Exceptions
{
    public class DependencyException : AuctionException
    {
       

        public DependencyException(String message):base(message)
        {
        }

    }
}
