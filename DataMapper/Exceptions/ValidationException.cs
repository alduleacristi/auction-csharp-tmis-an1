using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Exceptions
{
    public class ValidationException:AuctionException
    {
        public ValidationException():base()
        {
        }

        public ValidationException(String message):base(message)
        {
        }

        public ValidationException(String message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
