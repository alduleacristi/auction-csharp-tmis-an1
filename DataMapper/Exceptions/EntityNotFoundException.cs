using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Exceptions
{
    public class EntityNotFoundException:AuctionException
    {
        public EntityNotFoundException():base()
        {
        }

        public EntityNotFoundException(String message):base(message)
        {
        }

        public EntityNotFoundException(String message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
