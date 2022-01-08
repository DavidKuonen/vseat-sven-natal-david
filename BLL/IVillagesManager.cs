using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface IVillagesManager
    {
        List<Villages> GetAllVillages();
        Villages GetVillagesById(int idVillage);
        Villages GetVillageByName(string Village);
        //List<Villages> GetVillagesByDistrict(int idDistrict);
    }
}