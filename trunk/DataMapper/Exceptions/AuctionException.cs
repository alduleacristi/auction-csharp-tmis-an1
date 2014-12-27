using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Exceptions
{
    public class AuctionException:ApplicationException
    {
        public AuctionException():base()
        {
        }

        public AuctionException(String message):base(message)
        {
        }

        public AuctionException(String message, Exception inner):base(message,inner)
        {
        }
    }
}
