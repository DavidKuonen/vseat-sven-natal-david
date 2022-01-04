using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BLL
{
    public class DistrictsManager : IDistrictsManager
    {
        private IDistrictsDB DistrictsDb { get; }

        public DistrictsManager(IDistrictsDB DistrictsDb)
        {
            this.DistrictsDb = DistrictsDb;
        }

        //SQL Befehle der DAL Klasse
        public List<Districts> GetAllDistricts()
        {
            return DistrictsDb.GetAllDistricts();
        }

        public Districts GetDistrictsById(int id)
        {
            return DistrictsDb.GetDistrictsById(id);
        }
        //SQL Befehle bis hier
    }
}
