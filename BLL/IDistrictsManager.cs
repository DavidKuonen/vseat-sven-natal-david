using DTO;

namespace BLL
{
    public interface IDistrictsManager
    {
        //List<Districts> GetAllDistricts();
        Districts GetDistrictsById(int id);
    }
}