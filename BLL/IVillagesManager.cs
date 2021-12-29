using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface IVillagesManager
    {
        List<Villages> GetAllVillages();
        Villages GetVillagesById(int idVillage);
        List<Villages> GetVillagesByDistrict(int idDistrict);
    }
}