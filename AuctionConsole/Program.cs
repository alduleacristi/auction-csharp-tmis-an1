using DataMapper.EFDataMapper;
using DomainModel;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world");
            RoleService roleService = new RoleService();
            roleService.AddRole(new Role() { Name="ABCDE"});
            Console.ReadKey();
        }
    }
}
