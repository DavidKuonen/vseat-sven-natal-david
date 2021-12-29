using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface IDistrictsDB
    {
        List<Districts> GetAllDistricts();
        Districts GetDistrictsById(int id);
    }
}