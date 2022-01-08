using DAL;
using DTO;

namespace BLL
{
    public class DistrictsManager : IDistrictsManager
    {
        private IDistrictsDB DistrictsDb { get; }

        public DistrictsManager(IDistrictsDB DistrictsDb)
        {
            this.DistrictsDb = DistrictsDb;
        }

        //SQL queries
        public Districts GetDistrictsById(int id)
        {
            return DistrictsDb.GetDistrictsById(id);
        }
    }
}
