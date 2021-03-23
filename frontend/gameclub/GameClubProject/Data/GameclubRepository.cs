using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameClubProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GameClubProject.Data
{
    public class GameclubRepository : IGameClubData
    {
        private readonly GameclubDBContext _dbContext = null;        
        public GameclubRepository (GameclubDBContext context)
        {
            _dbContext = context;
        }

        public async Task<int> AddRegisteredUser (UserAccount user)
        {
            var newUser = new UserAccount()
            {
                UserName = user.UserName,
                MembershipStatusId = user.MembershipStatusId,
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                AddressStreet = user.AddressStreet,
                AddressStreet2= user.AddressStreet2,
                AddressCity = user.AddressCity,
                AddressState = user.AddressState,
                AddressZipCode = user.AddressZipCode,
                CreatedAt = BitConverter.GetBytes(DateTime.UtcNow.Ticks)
            };  
            await _dbContext.UserAccounts.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();
            return newUser.PKey;
        }
        
        public async Task<List<UserAccount>> GetRegisteredUserByIdAsync(string userid)
        {
            return await _dbContext.UserAccounts.Where(o => o.UserId == userid)
                  .Select(user => new UserAccount()
                  {
                      UserName = user.UserName,
                      MembershipStatusId = user.MembershipStatusId,
                      UserId = user.UserId,
                      FirstName = user.FirstName,
                      LastName = user.LastName,
                      Email = user.Email,
                      AddressStreet = user.AddressStreet,
                      AddressStreet2 = user.AddressStreet2,
                      AddressCity = user.AddressCity,
                      AddressState = user.AddressState,
                      AddressZipCode = user.AddressZipCode                      
                  //}).FirstOrDefaultAsync();
                  }).ToListAsync();
        }




    }

}
