using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface IVillagesManager
    {
        List<Villages> GetAllVillages();
        List<Villages> GetVillagesByDistrict(int idDistrict);
    }
}