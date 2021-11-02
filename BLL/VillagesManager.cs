using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BLL
{
    class VillagesManager
    {
        private IVillagesDB VillagesDb { get; }

        public VillagesManager(IConfiguration conf)
        {
           VillagesDb = new VillagesDB(conf);
        }

        public List<Villages> GetAllVillages()
        {
            return VillagesDb.GetAllVillages();
        }

        public List<Villages> GetVillagesByDistrict(int idDistrict)
        {
            return VillagesDb.GetVillagesByDistrict(idDistrict);
        }
    }
}
