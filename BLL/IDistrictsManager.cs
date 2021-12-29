using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface IDistrictsManager
    {
        List<Districts> GetAllDistricts();
        Districts GetDistrictsById(int id);
    }
}