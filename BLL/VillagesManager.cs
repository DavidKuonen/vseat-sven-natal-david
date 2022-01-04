using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BLL
{
    public class VillagesManager : IVillagesManager
    {
        private IVillagesDB VillagesDb { get; }

        public VillagesManager(IVillagesDB VillagesDb)
        {
            this.VillagesDb = VillagesDb;
        }

        //SQL Befehle der DAL Klasse
        public List<Villages> GetAllVillages()
        {
            return VillagesDb.GetAllVillages();
        }

        public Villages GetVillagesById(int id)
        {
            return VillagesDb.GetVillageById(id);
        }

        public Villages GetVillageByName(string Village)
        {
            return VillagesDb.GetVillageByName(Village);
        }

        public List<Villages> GetVillagesByDistrict(int idDistrict)
        {
            return VillagesDb.GetVillagesByDistrict(idDistrict);
        }
        //SQL Befehle bis hier
    }
}
