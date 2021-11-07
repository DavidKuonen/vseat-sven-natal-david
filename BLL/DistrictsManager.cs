using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BLL
{
    public class DistrictsManager : IDistrictsManager
    {
        private IDistrictsDB DistrictsDb { get; }

        public DistrictsManager(IConfiguration conf)
        {
            DistrictsDb = new DistrictsDB(conf);
        }

        //SQL Befehle der DAL Klasse
        public List<Districts> GetAllDistricts()
        {
            return DistrictsDb.GetAllDistricts();
        }
        //SQL Befehle bis hier
    }
}
