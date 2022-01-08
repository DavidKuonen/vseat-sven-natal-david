using System.Collections.Generic;

namespace WebApp.Models
{
    public class CollectionDataModel
    {
        public PersonalDetails PersonalDetails { get; set; }
        public List<ShoppingCartVM> ShoppingCartVMs { get; set; }
    }
}