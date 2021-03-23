using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameClubProject.Models;

namespace GameClubProject.Data
{
    public interface IGameClubData
    {
        Task<int> AddRegisteredUser(UserAccount user);
        Task<List<UserAccount>> GetRegisteredUserByIdAsync(string userid);
    }
}
