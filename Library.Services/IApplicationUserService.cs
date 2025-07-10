using Library.Utilities;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IApplicationUserService
    {
        PagedResult<ApplicationUserViewModel> GetAll(int pageNumber, int pageSize);
        PagedResult<ApplicationUserViewModel> GetAllMember(int pageNumber, int pageSize);
        PagedResult<ApplicationUserViewModel> GetAllStaff(int pageNumber, int pageSize);
        PagedResult<ApplicationUserViewModel> SearchMember(int pageNumber, int pageSize, string name);

        PagedResult<ApplicationUserViewModel> GetUserByUserCode(string usercode, int pageNumber, int pageSize);
        ApplicationUserViewModel GetUserById(int userId);

        void UpdateApplicationUser(ApplicationUserViewModel user);
        void InsertApplicationUser(ApplicationUserViewModel user);
        void DeleteApplicationUser(int id);
    }
}
