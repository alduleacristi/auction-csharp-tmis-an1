using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Exceptions
{
    public class EntityDoesNotExistException : AuctionException
    {
        public EntityDoesNotExistException():base()
        {
        }

        public EntityDoesNotExistException(String message):base(message)
        {
        }

        public EntityDoesNotExistException(String message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
