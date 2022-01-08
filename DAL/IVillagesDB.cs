using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface IVillagesDB
    {
        List<Villages> GetAllVillages();
        Villages GetVillageById(int idVillage);
        Villages GetVillageByName(string Village);
        //List<Villages> GetVillagesByDistrict(int idDistrict);
    }
}