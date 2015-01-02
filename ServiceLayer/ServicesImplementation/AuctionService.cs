using DataMapper;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServicesImplementation
{
    public class AuctionService:IAuctionService
    {
        private void CheckUser(User user)
        {
            //tb verificat rolul userului
            //tb verificat nr de licitatii active ale userului
        }
        public void AddNewAuction(Auction auction,User user)
        {
            DataMapperFactoryMethod.GetCurrentFactory().AuctionFactory.AddNewAuction(auction, user);
        }
    }
}
