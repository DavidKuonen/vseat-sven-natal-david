using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface IDistrictsDB
    {
        Districts GetDistrictsById(int id);
    }
}