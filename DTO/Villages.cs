

namespace DTO
{
    public class Villages
    {
        public int idVillage { get; set; }
        public int postalCode { get; set; }
        public string name { get; set; }
        public int idDistrict { get; set; }


        public override string ToString()
        {
            return "idVillage: " + idVillage +
                   " postalCode: " + postalCode +
                   " name: " + name +
                   " idDistrict: " + idDistrict;
        }
    }
}

