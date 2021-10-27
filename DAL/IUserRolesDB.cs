using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface IUserRolesDB
    {
        List<UserRole> GetAllUserRoles();
        UserRole GetEmployeeById(int idUserRole);
    }
}