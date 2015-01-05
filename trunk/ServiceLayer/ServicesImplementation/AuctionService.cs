using DataMapper;
using DataMapper.Exceptions;
using DomainModel;
using ServiceLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServicesImplementation
{
    public class AuctionService:IAuctionService
    {
        private AuctionLogger logger;
        public AuctionService()
        {
            logger = AuctionLogger.GetInstance();
        }
        private void CheckUser(User user)
        {
            IRoleService roleService = new RoleService();
            IConfiguration configuration = ConfigurationService.GetInstance();
            IUserService userService = new UserService();

            ICollection<Role> roles = roleService.GetRolesFromAnUser(user);
            ICollection<Rating> ratings = userService.GetAllRatingsOfAnUser(user);
            
            bool ok = false;
            foreach (Role role in roles)
                if (role.Name.Equals(Constants.OWNER))
                    ok = true;

            if (!ok)
                throw new AuctionException("User " + user.Email + " does not have the rights to add an auction");

            int nrOfActiveAuctions = this.GetNumberOfActiveAuctionsStartedByUser(user);

            int sum = 0;
            int nr = 0;
            foreach (Rating rating in ratings)
            {
                if ((DateTime.Now - rating.Date).TotalDays < configuration.GetValue(Constants.NR_OF_DAYS_USED_TO_DETERMINE_RATING))
                {
                    sum += rating.Grade;
                    nr++;
                }
            }

            double ratingCalc, maxNrOfAuctions;
            if(nr > 0)
            { 
                ratingCalc = sum / nr;
                maxNrOfAuctions = ratingCalc / 10 * configuration.GetValue(Constants.MAX_NR_OF_STARTED_AUCTION);
            }
            else
            {
                ratingCalc = 10;
                maxNrOfAuctions = configuration.GetValue(Constants.MAX_NR_OF_STARTED_AUCTION);
            }
            int intMaxNrOfAuctions = (int)maxNrOfAuctions;
            
            if(ratingCalc < configuration.GetValue(Constants.RATING_THRESH_HOLD_FOR_AUCTION))
                throw new AuctionException("Auction can not be added because the rating of the user is small than " + configuration.GetValue(Constants.RATING_THRESH_HOLD_FOR_AUCTION));

            if (nrOfActiveAuctions > intMaxNrOfAuctions)
                throw new AuctionException("Auction can not be added because max number of auctions per user is "+configuration.GetValue(Constants.MAX_NR_OF_STARTED_AUCTION));
        }

        private void CheckProduct(Product product,User user)
        {
            ICategoryService categoryService = new CategoryService();
            IProductService productService = new ProductService();
            IConfiguration configuration = ConfigurationService.GetInstance();
            ICollection<Category> categorys = categoryService.GetCategorysForAProduct(product);

            int nrOfAuctions = 0;
            foreach(Category category in categorys)
            {
                ICollection<Category> parentCAtegorys = categoryService.getParents(category.IdCategory);
                ICollection<Product> products;
                foreach(Category parentCategory in parentCAtegorys)
                {
                    products = productService.GetAllProductsOfACategory(category);
                    foreach(Product productCat in products)
                    {
                        Auction auction = productService.GetAuctionOfAProduct(productCat);
                        if (auction.User.Equals(user))
                            nrOfAuctions++;
                    }
                }
            }

            if(nrOfAuctions > configuration.GetValue(Constants.MAX_NR_OF_AUCTION_ASSOCIATE_WITH_CATEGORY))
                throw new AuctionException("The auction can not be added because the number of auctions per category was exceeded");
        }
        public void AddNewAuction(User user,Product product,Currency currency,double startPrice,DateTime startDate,DateTime endDate)
        {
            try 
            {
                logger.logInfo("User "+user.Email+" try to add a new auction.");

                CheckUser(user);
                CheckProduct(product,user);

                Auction auction = new Auction();
                auction.User = user;
                auction.Product = product;
                auction.Currency = currency;
                auction.StartPrice = startPrice;
                auction.BeginDate = startDate;
                auction.EndDate = endDate;

                DataMapperFactoryMethod.GetCurrentFactory().AuctionFactory.AddNewAuction(auction);
            }
            catch (EntityDoesNotExistException exc)
            {
                logger.logError(exc);
                throw exc;
            }
            catch (ValidationException validationException)
            {
                logger.logError(validationException);
                throw validationException;
            }
            catch (AuctionException auctioException)
            {
                logger.logError(auctioException);
                throw auctioException;
            }
        }

        public Currency GetCurrencyByName(String currencyName)
        {
            return DataMapperFactoryMethod.GetCurrentFactory().AuctionFactory.GetCurrencyByName(currencyName);
        }

        public int GetNumberOfActiveAuctionsStartedByUser(User user)
        {
            return DataMapperFactoryMethod.GetCurrentFactory().AuctionFactory.GetNumberOfActiveAuctionsStartedByUser(user);
        }
    }
}
