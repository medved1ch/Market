using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Models
{
    public class ViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Title { get; set; }
        public string Image { get; set;}
        public double Price { get; set; }
        public double Rate { get; set;}
        public string Description { get; set; }

    }
    public class ProductForSingle
    {
        public string Title { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public List<ProductCharacteristic> Character { get; set; }
        public double Rate { get; set;}
        public string Description { get; set; }
        public List<UserAccounts> UserAccounts { get; set; }
        
    }

    public class ProductCharacteristic
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }

    public class Characteristics
    {
        
        public string Product { get; set; }

        public string Text { get; set; }
    }

    public class UserAccounts
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Advantages { get; set; }
        public string Disadvantages { get; set; }
        public string Other { get; set; }
        public DateTime Date { get; set; }
        public double Rate { get; set; }
        

    }
}

