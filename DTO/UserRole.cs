using System;

namespace DTO
{
    public class UserRole
    {
        public int IdUserRole { get; set; }

        public string Type { get; set; }

        public override string ToString()
        {
            return "UserRole ID: " + IdUserRole +
                   " Type: " + Type;
        }
    }
}
