using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface IVillagesDB
    {
        List<Villages> GetAllVillages();
        List<Villages> GetVillagesByDistrict(int idDistrict);
    }
}