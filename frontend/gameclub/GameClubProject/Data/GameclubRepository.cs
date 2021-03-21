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
        private readonly GameclubDBContext _context = null;        
        public GameclubRepository (GameclubDBContext context)
        {
            _context = context;
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
            await _context.UserAccounts.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return newUser.PKey;
        }
        
        public async Task<List<UserAccount>> GetRegisteredUserByIdAsync(string userid)
        {
            return await _context.UserAccounts.Where(o => o.UserId == userid)
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
