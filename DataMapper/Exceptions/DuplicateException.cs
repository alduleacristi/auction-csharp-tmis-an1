using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Exceptions
{
    public class DuplicateException:AuctionException
    {
        public DuplicateException():base()
        {
        }

        public DuplicateException(String message):base(message)
        {
        }

        public DuplicateException(String message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
