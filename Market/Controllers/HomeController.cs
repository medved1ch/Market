using Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace Market.Controllers
{
    public class HomeController : Controller

    {
        de08Context db = new de08Context();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();
        }
        public IActionResult LogIn()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult SiteMap()
        {
            return View();
        }
        public IActionResult Feed(string Disadvantages, string Advantages, string Other,int id, int raten)
        {
            //Product pr = db.Products.SingleOrDefault(c => c.Id == id);
            //Feedback fb = new Feedback
            //{
            //    Advantages = Advantages,
            //    Disadvantages = Disadvantages,
            //    Other = Other,
            //    Product = pr.Id,
            //    Date = DateTime.Now,
            //    Rate = raten,
            //};
            //db.Feedbacks.Add(fb);
            //db.SaveChanges();

            return View();
        }
        public IActionResult ProductSingle(int id)
        {

            Product pr = db.Products.SingleOrDefault(c => c.Id == id);
            if (pr == null)
                return NotFound();
            List<Feedback> fb = db.Feedbacks.Where(c => c.Product == pr.Id).ToList();
            List<UserAccounts> userAccounts = new List<UserAccounts>();
            ProductForSingle pfs = new ProductForSingle
            {
                Brand = db.Brands.SingleOrDefault(c => c.Id == pr.Brand).Name,
                Image = pr.Image,
                Price = pr.Price,
                Title = pr.Title,
                Description = pr.Description,
                
                Type = db.TypeProducts.SingleOrDefault(c=> c.Id == pr.Type).Name,





            };
            if (fb.Count != 0)
            {
                foreach (var item in fb)
                {
                    UserAccounts ua = new UserAccounts
                    {
                        Advantages = item.Advantages,
                        Disadvantages = item.Disadvantages,
                        Other = item.Other,
                        Rate = item.Rate,
                        Id = item.Id,
                        Date = item.Date,
                    };
                    Models.User us = db.Users.SingleOrDefault(c => c.Id == item.Author);
                    if (us is null)
                    {
                        ua.Name = "Гость";
                    }
                    else
                    {
                        ua.Name = us.Login;
                    }
                    userAccounts.Add(ua);
                }
            }
            
            pfs.UserAccounts = userAccounts;
            List<Feedback> feed = db.Feedbacks.Where(c => c.Product == pr.Id).ToList();
            double amount = 0;
            foreach (var item in feed)
            {
                amount += item.Rate;
            }
            if (amount > 0)
                pfs.Rate = Math.Round(amount / feed.Count);
                
            else
                pfs.Rate = 0;
            List<ListCharacteristic> llc = db.ListCharacteristics.Where(c => c.Product == pr.Id).ToList();
            List<ProductCharacteristic> lpc = new List<ProductCharacteristic>();
            foreach (var item in llc)
            {
                ProductCharacteristic pc = new ProductCharacteristic
                {
                    Name = db.Characteristics.SingleOrDefault(c=> c.Id == item.Characteristic).Name,
                    Description = item.Text
                };
                lpc.Add(pc);
            }
            pfs.Character = lpc;
            return View(pfs);
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult RegisterAcc(string LastName, string FirstName, string MiddleName, string Login, string Password)
        {
            User usr = new User

            {
                Login = Login,
                LastName = LastName,
                FirstName = FirstName,
                MiddleName = MiddleName,
                Password = Password,
                Type = 2,
            };
            try
            {
                db.Users.Add(usr);
                db.SaveChanges();
                
            }
            catch (Exception e)
            {
                return BadRequest(e.StackTrace);
                
            }
            return RedirectToAction("Index");
        }


        public IActionResult ListProduct()
        {
            // var primes = Tuple.Create(lvm, 3, 5, 7, 11, 13, 17, 19);
            List<TypeProduct> lProd = db.TypeProducts.ToList();
            List<Product> Prod = db.Products.ToList();
            List<ViewModel> lvm = new List<ViewModel>();
            lProd.OrderByDescending(c => c.Id);
            if (lProd.Count > 5)
            {
                lProd = lProd.GetRange(0, 5);
            }
            foreach (var item in Prod)
            {
                List<Feedback> lf = db.Feedbacks.Where(c => c.Id == item.Id).ToList();
                double amount = 0;
                foreach (var item2 in lf)
                {
                    amount += item2.Rate;
                }

                ViewModel vm = new ViewModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    Type = db.TypeProducts.SingleOrDefault(c => c.Id == item.Type).Name,
                    Price = item.Price,
                    Brand = db.Brands.SingleOrDefault(c =>c.Id == item.Brand).Name,
                    Description = item.Description,
                    Image = item.Image,
                    Rate = amount,
                };
                if (lf.Count == 0)
                    vm.Rate = 0;
                else
                    vm.Rate = Math.Round(amount / lf.Count);
                lvm.Add(vm);
            }
            return View(lvm);
            //return View(Tuple.Create(Prod, lProd, brands));

        }


        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
