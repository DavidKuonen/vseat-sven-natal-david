﻿using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BLL
{
    public class CategoryRestaurantsManager : ICategoryRestaurantsManager
    {
        private ICategoryRestaurantsDB CategoryRestaurantsDb { get; }

        public CategoryRestaurantsManager(IConfiguration conf)
        {
            CategoryRestaurantsDb = new CategoryRestaurantsDB(conf);
        }

        //SQL Befehle der DAL Klasse
        public List<CategoryRestaurants> GetAllCateegoryRestautants()
        {
            return CategoryRestaurantsDb.GetAllCategoryRestaurants();
        }
        //SQL Befehle bis hier
    }
}
