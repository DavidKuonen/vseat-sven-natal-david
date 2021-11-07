using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DishesManager : IDishesManager
    {
        private IDishesDB DishesDb { get; }

        public DishesManager(IConfiguration conf)
        {
            DishesDb = new DishesDB(conf);
        }

        //SQL Befehle der DAL Klasse werden untenstehend geholt
        public List<Dishes> GetAllDishes()
        {
            return DishesDb.GetAllDishes();
        }

        public Dishes GetDishesByName(string name)
        {
            return DishesDb.GetDishesByName(name);
        }
        //SQL Befehle bis hier

    }
}
